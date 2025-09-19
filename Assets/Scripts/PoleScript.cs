using UnityEngine;

public class PoleScript : MonoBehaviour
{
    private Vector3 velocity = Vector3.zero;
    private Rigidbody2D rb;
    public int pole;
    public LayerMask layerMask;
    float checkRad =0.55f;
    public bool canMove;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();   
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
    
}
