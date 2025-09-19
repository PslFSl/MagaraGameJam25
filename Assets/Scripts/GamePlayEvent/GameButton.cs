using UnityEngine;

public class GameButton : MonoBehaviour
{
    public bool physicDoor;
    public GameObject gate;
    private DoorScript doorScript;
    private void Start()
    {
        doorScript=gate.GetComponent<DoorScript>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision != null)
        {
            doorScript.doorIsOpen = "open";
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision&&physicDoor)
        {
            doorScript.doorIsOpen = "close";
        }
    }
}
