using UnityEngine;

public class PlayerFollower:MonoBehaviour{
    public Transform Player;
    public float followSpeed=5.0f;
    private Vector3 offset=new Vector3(0,1,-10);
    void Start(){
        
    }
    void LateUpdate(){
        Vector3 playerPosition=Player.position+offset;
        transform.position=Vector3.Lerp(transform.position,playerPosition,followSpeed*Time.deltaTime);
    }
}
