using UnityEngine;

public class PlayerController : MonoBehaviour
{
[SerializeField]
private float MoveSpeed;
[SerializeField]
private float jumpForce;
[SerializeField]
private float jumpHeight;
// private bool IsGrounded;
[SerializeField]
private Transform groundCheck;
[SerializeField]
private LayerMask groundLayer;
[SerializeField]
private Vector3 range;
private float verticalInput;
private float horizontalInput;
private Rigidbody2D rigidbody2D;
private Animator animator;
private void Awake()
{
    rigidbody2D=GetComponent<Rigidbody2D>();
    animator=GetComponent<Animator>();
}
private void FixedUpdate()
{
    PlayerMovement(); 
    CheckCollitionForJump();
    
}
private void Update()
{
  
}
private void PlayerMovement()
{
    horizontalInput=Input.GetAxisRaw("Horizontal");
    animator.SetFloat("speed",Mathf.Abs(horizontalInput));
    rigidbody2D.velocity=new Vector2(horizontalInput*MoveSpeed,rigidbody2D.velocity.y);
    Vector2 scale=transform.localScale;
    if(horizontalInput>0)
    {
        scale.x =1f*Mathf.Abs(scale.x);
        transform.localScale=scale;
    }
    else if(horizontalInput<0)
    {
        scale.x =-1f*Mathf.Abs(scale.x);
        transform.localScale=scale;
    }
    else
    {
        transform.localScale=scale;
    }
    if(rigidbody2D.velocity.y<0)
    {
        animator.SetBool("IsFall",true);
    }
    else
        animator.SetBool("IsFall",false);
}
private void PlayerJump()
{
    bool jumpInput=Input.GetKeyDown(KeyCode.Space);
    if(jumpInput)
    {
        if(rigidbody2D.velocity.y>0)
        {
            rigidbody2D.velocity=new Vector2(rigidbody2D.velocity.x,rigidbody2D.velocity.y*jumpHeight);
        }
    }
}
private void CheckCollitionForJump()
{
    bool jumKey=Input.GetKeyDown(KeyCode.Space);
    Collider2D collider2D=Physics2D.OverlapBox(groundCheck.position,range,0,groundLayer);
    if(collider2D!=null)
    {
        if(collider2D.gameObject.tag=="Ground" && jumKey)
        {
            rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x,jumpForce);
            animator.SetBool("IsJump",true);
            // PlayerJump();
        }
        else
        {
            animator.SetBool("IsJump",false);
        }
    }
}
}

