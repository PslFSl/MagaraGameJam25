using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class PlayerEvent : MonoBehaviour
{
    [Header("Player Move")]
    public float moveSpeed;
    public float jumpSpeed;
    public float fallSpeed;
    float hor;
    private Rigidbody2D rb;
    public bool canJump;

    [Header("Ground Check")]
    LayerMask groundLayer=(1<<7)|(1<<6);
    public Transform feetPos;
    float checkRad=0.8f;
    public bool isGrounded;
    [Header("Rotate")]
    float firstRot;
    float lastRot;
    bool turnRight;
    bool turnLeft;
    [Header("Attraction")]
    public LayerMask poleLayer;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        lastRot = transform.rotation.z;
    }
    void Update()
    {
        RaycastHit2D hitNorth = Physics2D.Raycast(transform.position,transform.TransformDirection(Vector2.right),5,poleLayer);
        RaycastHit2D hitSouth = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.left), 5, poleLayer);
        if (hitNorth!)
        {
            PoleScript poleScript = hitNorth.collider.gameObject.GetComponent<PoleScript>();
            poleScript.PoleAttraction(transform.position, 1);
        }
        if (hitSouth!)
        {
            PoleScript poleScript = hitSouth.collider.gameObject.GetComponent<PoleScript>();
            poleScript.PoleAttraction(transform.position,0);

        }

        
        isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRad, groundLayer);


        hor = Input.GetAxisRaw("Horizontal");
        transform.Translate(hor * Time.deltaTime*moveSpeed,0,0,Space.World);

        if (Input.GetKeyDown(KeyCode.Space)&&isGrounded&&canJump)
        {
            rb.gravityScale = 1;
            rb.AddForce(jumpSpeed * Vector2.up *5,ForceMode2D.Impulse);
            canJump = false;
            StartCoroutine(waitForjump());
        }
        else if (rb.linearVelocity.y < 0)
        {
            rb.gravityScale += 1 * Time.deltaTime*fallSpeed;
        }
        if (isGrounded) { rb.gravityScale = 1; }

        transform.rotation = Quaternion.Euler(0, 0, firstRot);


        if (Input.GetKeyDown(KeyCode.E)&&isGrounded)
        {
             lastRot -= 90;
            turnRight = true;
            rb.AddForce(jumpSpeed * Vector3.up * 4, ForceMode2D.Impulse);

        }
        if (Input.GetKeyDown(KeyCode.Q)&&isGrounded)
        {
            lastRot += 90;
            turnLeft = true;
            rb.AddForce(jumpSpeed*Vector3.up*4, ForceMode2D.Impulse);

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
    
    IEnumerator waitForjump()
    {
        yield return new WaitForSeconds(0.5f);
        canJump = true;
    }
}
