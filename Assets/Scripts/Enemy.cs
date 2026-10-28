using UnityEngine;
using System.Collections;

public class Enemy:MonoBehaviour{
    private Transform Player;
    public float chaseSpeed=2f;
    public float jumpForce=10f;
    public LayerMask groundLayer;
    private Rigidbody2D rb;
    private bool isGrounded;
    private bool shouldJump;
    public int damage=1;
    public int maxHP=3;
    private int currentHP;
    private SpriteRenderer spriteRenderer;
    private Color color;
    void Start(){
        rb=GetComponent<Rigidbody2D>();
        Player=GameObject.FindWithTag("Player").GetComponent<Transform>();
        currentHP=maxHP;
        spriteRenderer=GetComponent<SpriteRenderer>();
        color=spriteRenderer.color;
    }

    // Update is called once per frame
    void Update(){
        isGrounded=Physics2D.Raycast(transform.position,Vector2.down,1f,groundLayer);
        float direction=Mathf.Sign(Player.position.x-transform.position.x);
        bool isAbove=Physics2D.Raycast(transform.position,Vector2.up,3f,1<<Player.gameObject.layer);
        if(isGrounded){
            rb.linearVelocity=new Vector2(direction*chaseSpeed,rb.linearVelocityY);
            RaycastHit2D groundInFront=Physics2D.Raycast(transform.position,new Vector2(direction,0),2f,groundLayer);
            RaycastHit2D gapAhead=Physics2D.Raycast(transform.position+new Vector3(direction,0,0),Vector2.down,2f,groundLayer);
            RaycastHit2D platformAbove=Physics2D.Raycast(transform.position,Vector2.up,10f,groundLayer);
            if(!groundInFront.collider&&!gapAhead.collider){
                shouldJump=true;
            }
            else if(isAbove&&platformAbove.collider){
                shouldJump=true;
            }
        }
    }
    private void FixedUpdate(){
        if(isGrounded&&shouldJump){
            shouldJump=false;
            Vector2 derection=(Player.transform.position-transform.position).normalized;
            Vector2 jumpDerection=derection*jumpForce;
            rb.AddForce(new Vector2(jumpDerection.x,jumpForce),ForceMode2D.Force);
        }
        
    }
    public void TakeDamage(int damage){
        StartCoroutine(FlashWhite());
        currentHP-=damage;
        if(currentHP<=0){
            Die();
        }
    }
    private IEnumerator FlashWhite(){
        spriteRenderer.color=Color.white;
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.color=color;
    }
    private void Die(){
        Destroy(gameObject);
    }
}
