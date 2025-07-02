using UnityEngine;

public class PlayerMoverment : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    public float moveSpeed = 5f;
    public float jump = 50f;
    public LayerMask groundLayer;
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    
    private Rigidbody2D rb2d;
    private Vector2 movement;
    private bool isGrounded;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        float input = Input.GetAxisRaw("Horizontal");
        movement.Normalize();
        MoveAnimation(input);
    }
    
    void FixedUpdate()
    {
        rb2d.linearVelocity = moveSpeed * movement;

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        
        if ((Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) && isGrounded)
        {
            rb2d.linearVelocity = new Vector2(rb2d.linearVelocity.x, jump);
        }
    }

    void MoveAnimation(float input)
    {
        if (input > 0)
        {
            animator.SetBool("IsRunning", true);
            spriteRenderer.flipX = false;
        }
        else if (input < 0)
        {
            animator.SetBool("IsRunning", true);
            spriteRenderer.flipX = true;
        }
        else
        {
            animator.SetBool("IsRunning", false);
        }
    }
}
