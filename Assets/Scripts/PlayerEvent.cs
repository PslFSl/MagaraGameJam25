using System.Collections;
using UnityEngine;

public class PlayerEvent : MonoBehaviour
{
    [Header("Player Move")]
    public float moveSpeed;
    public float jumpSpeed;
    public float fallSpeed;
    float hor;
    private Rigidbody2D rb;

    [Header("Ground Check")]
    public LayerMask groundLayer;
    public Transform feetPos;
    float checkRad=0.1f;
    public bool isGrounded;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRad, groundLayer);
        hor = Input.GetAxisRaw("Horizontal");
        transform.Translate(hor * Time.deltaTime*moveSpeed,0,0,Space.World);

        if (Input.GetKeyDown(KeyCode.Space)&&isGrounded)
        {
            rb.gravityScale = 1;
            rb.AddForce(jumpSpeed * Vector2.up *2,ForceMode2D.Impulse);
        }
        else if (rb.linearVelocity.y < 0)
        {
            rb.gravityScale += 1 * Time.deltaTime*fallSpeed;
        }
        if (isGrounded) { rb.gravityScale = 1; }
        
    }
    
    
}
