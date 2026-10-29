using UnityEngine;

public class MovingPlatform : MonoBehaviour{
    public Transform pointA;
    public Transform pointB;
    public float moveSpeed=2f;
    private Vector3 nextPosition;
    void Start(){
        nextPosition=pointB.position;
    }

    // Update is called once per frame
    void Update(){
        transform.position=Vector3.MoveTowards(transform.position,nextPosition,moveSpeed*Time.deltaTime);
        if(transform.position==pointA.position){
            nextPosition=pointB.position;
        }
        if(transform.position==pointB.position){
            nextPosition=pointA.position;
        }
    }
    void OnCollisionEnter2D(Collision2D other){
        if(other.gameObject.tag=="Player"){
            other.transform.SetParent(transform);
        }
    }
    void OnCollisionExit2D(Collision2D other){
        if(other.gameObject.tag=="Player"){
            other.transform.SetParent(null);
        }
    }
}
