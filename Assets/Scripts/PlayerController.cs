using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerController:MonoBehaviour{
    public Rigidbody2D rb;
    public Animator ani;
    private bool isFacingRight=true;
    [Header("Movement")]
    public float moveSpeed=5f;
    public float horizontalMovement;
    [Header("GroundCheck")]
    public Transform groundCheck;
    public Vector2 groundCheckSize=new Vector2(0.5f,0.05f);
    public bool lastIsGround,isGround;
    public LayerMask groundLayer;
    [Header("WallCheck")]
    public Transform wallCheck;
    public Vector2 wallCheckSize=new Vector2(0.5f,0.05f);
    public LayerMask wallLayer;
    [Header("Jump")]
    public float jumpPower=10f;
    public int maxJumps=2;
    private int remainingJumps;
    [Header("Gravity")]
    public float baseGravity=2f;
    public float maxFallSpeed=18f;
    public float fallSpeedMultiplier=2f;
    [Header("WallMovement")]
    public int wallSlideSpeed=2;
    private bool isWallSliding,isWallJumping;
    private float wallJumpDirection;
    private float wallJumpTimer,wallJumpTime=0.5f;
    public Vector2 wallJumpPower=new Vector2(5f,10f);


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
    private void WallSlide(){
        if(!isGround&&WallChecker()&&horizontalMovement!=0){
            isWallSliding=true;
            rb.linearVelocity=new Vector2(rb.linearVelocityX,Mathf.Max(rb.linearVelocityY,-wallSlideSpeed));
        }
        else{
            isWallSliding=false;
        }
    }
    private void ProcessWallJump(){
        if(isWallSliding){
            isWallJumping=false;
            wallJumpDirection=-transform.localScale.x;
            wallJumpTimer=wallJumpTime;
            CancelInvoke(nameof(CancelWallJump));
        }
        else if(wallJumpTimer>0f){
            wallJumpTimer-=Time.deltaTime;
        }
    }
    private void CancelWallJump(){
        isWallJumping=false;
    }
    void Update(){
        GroundChecker();
        Gravity();
        WallSlide();
        ProcessWallJump();
        if(!isWallJumping){
            Flip();
        }
        ani.SetFloat("Yvelocity",rb.linearVelocityY);
        ani.SetFloat("Magnitude",rb.linearVelocity.magnitude);
        ani.SetBool("IsWallSliding",isWallSliding);
    }
    void FixedUpdate(){
        if(!isWallJumping){
            rb.linearVelocity=new Vector2(horizontalMovement*moveSpeed,rb.linearVelocityY);
        }
    }
    public void Move(InputAction.CallbackContext context){
        horizontalMovement=context.ReadValue<Vector2>().x;
    }
    public void Jump(InputAction.CallbackContext context){
        if(context.canceled&&rb.linearVelocityY>0){
            rb.linearVelocity=new Vector2(rb.linearVelocityX,rb.linearVelocityY*0.5f);
            ani.SetTrigger("Jump");
        }
        if(remainingJumps>0&&context.performed){
            rb.linearVelocity=new Vector2(rb.linearVelocityX,jumpPower);
            --remainingJumps;
            ani.SetTrigger("Jump");
        }
        //wall jump
        if(context.performed&&wallJumpTimer>0f){
            isWallJumping=true;
            rb.linearVelocity=new Vector2(wallJumpDirection*wallJumpPower.x,wallJumpPower.y);
            wallJumpTimer=0;
            if(transform.localScale.x!=wallJumpDirection){
                isFacingRight=!isFacingRight;
                Vector3 ls=transform.localScale;
                ls.x*=-1f;
                transform.localScale=ls;
                Debug.Log("翻转，现在朝"+(isFacingRight?"右":"左"));
            }
            Invoke(nameof(CancelWallJump),wallJumpTime+0.1f);
            ani.SetTrigger("Jump");
        }
    }
    private void Flip(){
        if((isFacingRight&&horizontalMovement<0)||(!isFacingRight&&horizontalMovement>0)){
            isFacingRight=!isFacingRight;
            Vector3 ls=transform.localScale;
            ls.x*=-1f;
            transform.localScale=ls;
            Debug.Log("翻转，现在朝"+(isFacingRight?"右":"左"));
        }
    }
    private void GroundChecker(){
        isGround=Physics2D.OverlapBox(groundCheck.position,groundCheckSize,0,groundLayer);
        if(isGround&&isGround!=lastIsGround){
            remainingJumps=maxJumps;
        }
        lastIsGround=isGround;
    }
    private bool WallChecker(){
        return Physics2D.OverlapBox(wallCheck.position,wallCheckSize,0,wallLayer);
    }
    private void OnDrawGizmosSelected(){
        Gizmos.color=Color.white;
        Gizmos.DrawWireCube(groundCheck.position,groundCheckSize);
        Gizmos.color=Color.blue;
        Gizmos.DrawWireCube(wallCheck.position,wallCheckSize);
    }
}
