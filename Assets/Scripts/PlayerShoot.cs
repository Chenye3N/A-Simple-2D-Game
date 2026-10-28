using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShoot : MonoBehaviour{
    public GameObject bulletPrefab;
    public float bulletSpeed=50f;
    void Start(){
        
    }

    // Update is called once per frame
    void Update(){
        if(Mouse.current!=null&&Mouse.current.leftButton.wasPressedThisFrame){
            Shoot();
            Debug.Log("Shoot");
        }
    }
    void Shoot(){
        Vector3 mousePosition=Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        mousePosition.z=0f;
        Vector3 Direction=mousePosition-transform.position;
        GameObject bullet=Instantiate(bulletPrefab,transform.position,Quaternion.identity);
        Rigidbody2D rb=bullet.GetComponent<Rigidbody2D>();
        rb.linearVelocity=new Vector2(Direction.normalized.x,Direction.normalized.y)*bulletSpeed;
        Destroy(bullet,5f);
    }
}
