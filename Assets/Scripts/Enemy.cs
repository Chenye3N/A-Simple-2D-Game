using UnityEngine;

public class Enemy:MonoBehaviour{
    public Transform Player;
    public float chaseSpeed=2f;
    public float jumpForce=2f;
    public LayerMask groundLayer;
    private Rigidbody2D rb;
    private bool isGrounded;
    private bool shouldJump;
    void Start(){
        rb=GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
