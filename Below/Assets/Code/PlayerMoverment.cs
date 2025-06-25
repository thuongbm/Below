using UnityEngine;

public class PlayerMoverment : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    public float moveSpeed = 5f;
    public float jump = 10f;
    
    private Rigidbody2D rb2d;
    private Vector2 movement;
    
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        movement.Normalize();
    }
    
    void FixedUpdate()
    {
        rb2d.linearVelocity = moveSpeed * movement;
    }
}
