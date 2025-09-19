using UnityEngine;

public class ElectroTrap : MonoBehaviour
{
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerEvent myChar = collision.gameObject.GetComponent<PlayerEvent>();
            myChar.ForDeath();
        }
    }
}
