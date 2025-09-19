using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
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
    private Animator anim;

    [Header("Ground Check")]
    LayerMask groundLayer=(1<<7)|(1<<6);
    LayerMask ceilingLayer = (1 << 10)|(1<<9);
    public Transform feetPos;
    float checkRad=0.8f;
    bool isGrounded;
    bool isCeiling;
    [Header("Rotate")]
    float firstRot;
    float lastRot;
    bool turnRight;
    bool turnLeft;
    bool turn;
    [Header("Attraction")]
    public LayerMask poleLayer;
    public LayerMask pullLayerN;
    public LayerMask pullLayerS;
    private Vector3 velocity= Vector3.zero;

    private SceneControl sceneControl;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        lastRot = transform.rotation.z;
        anim = GetComponent<Animator>();
        sceneControl = GetComponent<SceneControl>();
    }
    void Update()
    {
        anim.SetFloat("speed", Mathf.Abs(hor));
        RaycastHit2D hitNorth = Physics2D.Raycast(transform.position,transform.TransformDirection(Vector2.right),5,poleLayer);
        RaycastHit2D hitSouth = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.left), 5, poleLayer);
        if (hitNorth!&&!turn)
        {
            PoleScript poleScript = hitNorth.collider.gameObject.GetComponent<PoleScript>();
            poleScript.PoleAttraction(transform.position, 1);
        }
        if (hitSouth!&&!turn)
        {
            PoleScript poleScript = hitSouth.collider.gameObject.GetComponent<PoleScript>();
            poleScript.PoleAttraction(transform.position,0);

        }
        RaycastHit2D hitNorthPull = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.right), 6, pullLayerS);
        if (hitNorthPull! && !turn)
        {
            transform.position = Vector3.SmoothDamp(transform.position,new Vector3(transform.position.x, hitNorthPull.collider.gameObject.GetComponent<Transform>().position.y-1f, 0),ref velocity,0.2f);
            rb.gravityScale=0;
        }
        RaycastHit2D hitSouthPull = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.left), 6, pullLayerN);
        if (hitSouthPull! && !turn)
        {
            transform.position = Vector3.SmoothDamp(transform.position, new Vector3(transform.position.x, hitSouthPull.collider.gameObject.GetComponent<Transform>().position.y-1f, 0), ref velocity, 0.2f);
            rb.gravityScale = 0;
        }


        isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRad, groundLayer);
        isCeiling = Physics2D.OverlapCircle(transform.position, 1, ceilingLayer);


        hor = Input.GetAxisRaw("Horizontal");
        transform.Translate(hor * Time.deltaTime*moveSpeed,0,0,Space.World);

        if (Input.GetKeyDown(KeyCode.Space)&&isGrounded&&canJump)
        {
            rb.gravityScale = 1;
            rb.AddForce(jumpSpeed * Vector2.up *5,ForceMode2D.Impulse);
            canJump = false;
            StartCoroutine(waitForjump());
        }
        else if (rb.linearVelocity.y < 0&&!hitNorthPull&&!hitSouthPull)
        {
            rb.gravityScale += 1 * Time.deltaTime*fallSpeed;
        }
        if (isGrounded) { rb.gravityScale = 1; }

        transform.rotation = Quaternion.Euler(0, 0, firstRot);


        if (Input.GetKeyDown(KeyCode.E)&&(isGrounded||isCeiling))
        {
             lastRot -= 90;
            turnRight = true;
            rb.AddForce(jumpSpeed * Vector3.up * 5, ForceMode2D.Impulse);

        }
        if (Input.GetKeyDown(KeyCode.Q)&&(isGrounded||isCeiling))
        {
            lastRot += 90;
            turnLeft = true;
            rb.AddForce(jumpSpeed*Vector3.up*5, ForceMode2D.Impulse);

        }


        if (firstRot > lastRot&&turnRight==true)
        {
            firstRot -= Time.deltaTime * 150f;
            turn = true;
        }
        else
        {
            turnRight = false;
            turn = false;
        }
        if (firstRot < lastRot && turnLeft == true)
        {
            firstRot += Time.deltaTime * 150f;
            turn = true;
        }
        else
        {
            turnLeft = false;
            turn = false;
        }

    }
    
    IEnumerator waitForjump()
    {
        yield return new WaitForSeconds(0.5f);
        canJump = true;
    }
    public void ForDeath()
    {
        anim.SetTrigger("death");
        sceneControl.waitford(3, gameObject.GetComponent<PlayerEvent>());
        
    }
}
