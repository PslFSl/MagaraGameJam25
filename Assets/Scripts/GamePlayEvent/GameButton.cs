using UnityEngine;

public class GameButton : MonoBehaviour
{
    public bool physicDoor;
    public GameObject gate;
    private DoorScript doorScript;
    public Sprite noPress;
    public Sprite Press;
    private void Start()
    {
        doorScript=gate.GetComponent<DoorScript>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision != null)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = Press;
            doorScript.doorIsOpen = "open";
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision&&physicDoor)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = noPress;
            doorScript.doorIsOpen = "close";
        }
    }
}
