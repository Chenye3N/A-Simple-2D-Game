using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
public class ObjectSpawner : MonoBehaviour
{
    public enum ObjectType{Gem,BigGem,Enemy};
    public Tilemap tilemap;
    public GameObject[] objectPrefabs;
    public float bigGemProbability=0.2f,EnemyProbability=0.1f;
    public int maxObjects=5;
    public float gemLifeTime=10f;
    public float spwanInterval=0.5f;
    private List<Vector3>validPosition=new List<Vector3>();
    private List<GameObject>spawnObject=new List<GameObject>();
    private bool isSpawning=false;
    void Start(){
        GetValidPositions();
        StartCoroutine(SpawnObjectsIfNeeded());
        GameController.GameOverEvent+=ResetSpawner;
    }
    void ResetSpawner(){
        StopAllCoroutines();
        DestroyAllObjects();
        GetValidPositions();
        StartCoroutine(SpawnObjectsIfNeeded());
    }
    void Update(){
        if(!tilemap.gameObject.activeInHierarchy){
            LevelChange();
        }
        if(!isSpawning&&ObjectCounts()<maxObjects){
            StartCoroutine(SpawnObjectsIfNeeded());
        }
    }
    private void LevelChange(){
        tilemap=GameObject.Find("Ground").GetComponent<Tilemap>();
        GetValidPositions();
        DestroyAllObjects();
    }
    private void DestroyAllObjects(){
        foreach(GameObject x in spawnObject){
            if(x!=null) Destroy(x);
        }
        spawnObject.Clear();
    }
    private bool PositionHasObject(Vector3 position){
        return spawnObject.Any(checkobj=>checkobj&&Vector3.Distance(checkobj.transform.position,position)<1f);
    }
    private ObjectType RandomObjectType(){
        float value=Random.value;
        if(value<EnemyProbability) return ObjectType.Enemy;
        else if(value<EnemyProbability+bigGemProbability) return ObjectType.BigGem;
        return ObjectType.Gem;
    }
    private void SpawnObject(){
        if(validPosition.Count==0) return;
        Vector3 spawnPosition=Vector3.zero;
        bool validPositionFound=false;
        for(;!validPositionFound&&validPosition.Count>0;){
            int index=Random.Range(0,validPosition.Count);
            Vector3 potentialPosition=validPosition[index];
            Vector3 leftPosition=potentialPosition+Vector3.left;
            Vector3 rightPosition=potentialPosition+Vector3.right;
            if(!PositionHasObject(leftPosition)&&!PositionHasObject(rightPosition)){
                spawnPosition=potentialPosition;
                validPositionFound=true;
            }
            validPosition.RemoveAt(index);
        }
        if(validPositionFound){
            ObjectType objType=RandomObjectType();
            GameObject gameobj=Instantiate(objectPrefabs[(int)(objType)],spawnPosition,Quaternion.identity);
            spawnObject.Add(gameobj);
            if(objType!=ObjectType.Enemy){
                StartCoroutine(DestroyGems(gameobj));
            }
        }
    }
    private IEnumerator DestroyGems(GameObject Gem){
        yield return new WaitForSeconds(gemLifeTime);
        if(Gem){
            spawnObject.Remove(Gem);
            validPosition.Add(Gem.transform.position);
            Destroy(Gem);
        }
    }
    private int ObjectCounts(){
        spawnObject.RemoveAll(item=>item==null);
        return spawnObject.Count;
    }
    private IEnumerator SpawnObjectsIfNeeded(){
        isSpawning=true;
        for(;ObjectCounts()<maxObjects;){
            yield return new WaitForSeconds(spwanInterval);
            SpawnObject();
        }
        isSpawning=false;
    }
    private void GetValidPositions(){
        validPosition.Clear();
        BoundsInt boundsInt=tilemap.cellBounds;
        TileBase[] allTiles=tilemap.GetTilesBlock(boundsInt);
        Vector3 start=tilemap.CellToWorld(new Vector3Int(boundsInt.xMin,boundsInt.yMin,0));
        for(int x=0;x<boundsInt.size.x;++x){
            for(int y=0;y<boundsInt.size.y;++y){
                TileBase tile=allTiles[x+y*boundsInt.size.x];
                if(tile==null) continue;
                Vector3 place=start+new Vector3(x+0.5f,y+2f,0);
                validPosition.Add(place);
            }
        }
    }
    void OnDestroy(){
        GameController.GameOverEvent-=ResetSpawner;
    }
}
