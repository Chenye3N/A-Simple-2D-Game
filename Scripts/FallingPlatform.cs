using UnityEngine;
using System.Collections;

public class FallingPlatform : MonoBehaviour{
    Rigidbody2D rb;
    public float fallSpeed=5f;
    public float fallTime=2f;
    public float destroyTime=1f;
    bool Falling=false;
    void Start(){
        rb=GetComponent<Rigidbody2D>();
    }
    void OnCollisionEnter2D(Collision2D collision){
        if(!Falling&&collision.gameObject.tag == "Player"){
            Falling=true;
            StartCoroutine(Fall());
        }
    }
    public IEnumerator Fall(){
        yield return new WaitForSeconds(fallTime);
        rb.bodyType=RigidbodyType2D.Dynamic;
        rb.linearVelocity=new Vector2(0,-fallSpeed);
        yield return new WaitForSeconds(destroyTime);
        Destroy(gameObject);
    }
}
