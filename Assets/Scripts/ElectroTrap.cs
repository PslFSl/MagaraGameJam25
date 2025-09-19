using UnityEngine;

public class ElectroTrap : MonoBehaviour
{
    public float speed;
    public float distance;


    private void Update()
    {
        float x = Mathf.PingPong(Time.deltaTime*speed,distance*2)-distance;
        transform.position= new Vector3(transform.position.x + x ,transform.position.y,transform.position.z);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerEvent myChar = collision.gameObject.GetComponent<PlayerEvent>();
            myChar.ForDeath();
        }
    }
}
