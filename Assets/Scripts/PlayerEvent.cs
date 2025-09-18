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
    float checkRad=1f;
    public bool isGrounded;
    [Header("Rotate")]
    public float firstRot;
    public float lastRot;
    bool turnRight;
    bool turnLeft;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        lastRot = transform.rotation.z;
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

        transform.rotation = Quaternion.Euler(0, 0, firstRot);


        if (Input.GetKeyDown(KeyCode.E)&&rb.linearVelocity.y>0)
        {
             lastRot -= 90;
            turnRight = true;
            
        }
        if (Input.GetKeyDown(KeyCode.Q) && rb.linearVelocity.y > 0)
        {
            lastRot += 90;
            turnLeft = true;

        }


        if (firstRot > lastRot&&turnRight==true)
        {
            firstRot -= Time.deltaTime * 150f;
        }
        else
        {
            turnRight = false;
        }
        if (firstRot < lastRot && turnLeft == true)
        {
            firstRot += Time.deltaTime * 150f;
        }
        else
        {
            turnLeft = false;
        }

    }
    
    
}
