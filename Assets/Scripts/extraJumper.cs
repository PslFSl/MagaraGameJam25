using UnityEngine;

public class extraJumper : MonoBehaviour
{
    LayerMask layer = (1<<8)|(1<<7);
    void Update()
    {
        RaycastHit2D hitUp = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.up), 1, layer);
        if (hitUp!)
        {
            hitUp.collider.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up*0.1f,ForceMode2D.Impulse);
        }
    }
}
