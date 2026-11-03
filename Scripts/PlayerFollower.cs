using UnityEngine;

public class PlayerFollower:MonoBehaviour{
    public Transform Player;
    public float followSpeed=5.0f;
    private Vector3 offset=new Vector3(0,1,-10); 
    [Header("Bounds")]
    public BoxCollider2D bound;
    private Camera cam;
    private float halfHeight,halfWidth;
    void Start(){
        cam=GetComponent<Camera>();
        halfHeight=cam.orthographicSize;
        halfWidth=halfHeight*cam.aspect;
    }
    void LateUpdate(){
        Vector3 targetPosition=Player.position+offset;
        targetPosition.x=Mathf.Clamp(targetPosition.x,bound.bounds.min.x+halfWidth,bound.bounds.max.x-halfWidth);
        targetPosition.y=Mathf.Clamp(targetPosition.y,bound.bounds.min.y+halfHeight,bound.bounds.max.y-halfHeight);
        transform.position=Vector3.Lerp(transform.position,targetPosition,followSpeed*Time.deltaTime);
    }
}
