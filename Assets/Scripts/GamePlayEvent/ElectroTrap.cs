using UnityEngine;

public class ElectroTrap : MonoBehaviour
{
    public float speed;
    public float distance;
    public bool move;
    private Vector3 vec;

    private void Start()
    {
        vec = transform.position;
    }
    private void Update()
    {
        if (move)
        {
            float x = Mathf.PingPong(Time.time*speed,distance*2)-distance;
            transform.position= new Vector3(vec.x + x ,vec.y,vec.z);
        }
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerEvent myChar = collision.gameObject.GetComponent<PlayerEvent>();
            myChar.ForDeath();
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            PlayerEvent myChar = collision.gameObject.GetComponent<PlayerEvent>();
            myChar.ForDeath();
        }

    }
}
