using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerController:MonoBehaviour{
    public Rigidbody2D rb;
    public float moveSpeed=5f;
    public float horizontalMovement;
    public float jumpPower=10f;
    public Transform groundCheck;
    public Vector2 groundCheckSize=new Vector2(0.5f,0.05f);
    public LayerMask groundLayer;
    public int maxJumps=2;
    private int remainingJumps;
    public bool lastIsGround,isGround;
    public float baseGravity=2f;
    public float maxFallSpeed=18f;
    public float fallSpeedMultiplier=2f;

    void Start(){
        remainingJumps=maxJumps;
    }
    private void Gravity(){
        if(rb.linearVelocityY<0){
            rb.gravityScale=baseGravity*fallSpeedMultiplier;
            rb.linearVelocity=new Vector2(rb.linearVelocityX,Mathf.Max(rb.linearVelocityY,-maxFallSpeed));
        }
        else{
            rb.gravityScale=baseGravity;
        }
    }
    void Update(){
        
        GroundChecker();
        Gravity();
    }
    void FixedUpdate(){
        rb.linearVelocity=new Vector2(horizontalMovement*moveSpeed,rb.linearVelocityY);
    }
    public void Move(InputAction.CallbackContext context){
        horizontalMovement=context.ReadValue<Vector2>().x;
    }
    public void Jump(InputAction.CallbackContext context){
        if(context.canceled&&rb.linearVelocityY>0){
            rb.linearVelocity=new Vector2(rb.linearVelocityX,rb.linearVelocityY*0.5f);
        }
        if(remainingJumps>0&&context.performed){
            rb.linearVelocity=new Vector2(rb.linearVelocityX,jumpPower);
            --remainingJumps;
        }
    }
    private void OnDrawGizmosSelected(){
        Gizmos.color=Color.white;
        Gizmos.DrawWireCube(groundCheck.position,groundCheckSize);
    }
    private void GroundChecker(){
        isGround=Physics2D.OverlapBox(groundCheck.position,groundCheckSize,0,groundLayer);
        if(isGround&&isGround!=lastIsGround){
            remainingJumps=maxJumps;
        }
        lastIsGround=isGround;
    }
}
