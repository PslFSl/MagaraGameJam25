using UnityEngine;

public class PoleScript : MonoBehaviour
{
    private Vector3 velocity = Vector3.zero;
    private Rigidbody2D rb;
    public int pole;
    public LayerMask layerMask;
    float checkRad =0.5f;
    bool canMove;
    [Header("Between Them Atrraction")]
    float checkGameObjectRad=2;
    public LayerMask layer;
    public float distance;
    private Vector3 pos;
    public bool canAttraction;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();   
    }
    private void Update()
    {
        Collider2D isAtrraction = Physics2D.OverlapCircle(transform.position, checkGameObjectRad, layer);
        if (isAtrraction!=null&&isAtrraction.gameObject.GetComponent<PoleScript>().pole ==pole&&canAttraction )
        {
            pos=isAtrraction.gameObject.transform.position;
            distance = Vector3.Distance(transform.position, pos);
            if (distance < 3) { transform.Translate((transform.position - pos).normalized * Time.deltaTime * 10, Space.World); ; }
        }
        else if (isAtrraction != null&&canAttraction)
        {
            pos = isAtrraction.gameObject.transform.position;
            distance = Vector3.Distance(transform.position, pos);
            if (distance < 3) { transform.position = Vector3.SmoothDamp(transform.position, pos, ref velocity, 0.3f); }
        }
        
    }
    public void PoleAttraction(Vector3 pos,int charPole)
    { 
        canMove = Physics2D.OverlapCircle(transform.position, checkRad, layerMask);
        if (pole != charPole&&!canMove)
        {
            transform.position = Vector3.SmoothDamp(transform.position, pos,ref velocity , 0.3f);
        }
        else if (pole==charPole&&!canMove)
        {
            if (transform.position.x > pos.x)
            {
                rb.AddForce(Vector2.right*0.5f, ForceMode2D.Force);
            }
            else
            {
                rb.AddForce(Vector2.left*0.5f, ForceMode2D.Force);
            }
            
        }
       

    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("pole"))
        {
            canAttraction = false;
        }
        

    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("pole"))
        {
            canAttraction = true;
        }

    }

}
