using UnityEngine;

public class Trap:MonoBehaviour{
    public float bounceForce=10f;
    public int damage=1;
    void OnTriggerEnter2D(Collider2D other){
        Debug.Log("Trap triggered by: "+other.name);
        if(other.CompareTag("Player")){
            HandlePlayerBounce(other.gameObject);
        }
    }
    void HandlePlayerBounce(GameObject player){
        Rigidbody2D rb=player.GetComponent<Rigidbody2D>();
        if(rb){
            rb.linearVelocity=new Vector2(rb.linearVelocity.x,0);
            rb.AddForce(Vector2.up*bounceForce,ForceMode2D.Impulse);
        }
    }
}
