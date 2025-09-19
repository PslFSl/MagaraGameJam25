using UnityEngine;

public class DoorScript : MonoBehaviour
{
    public string doorIsOpen;
    private float speed;
    private Vector3 vec;
    private Vector3 velocity= Vector3.zero;
    private void Start()
    {
        vec = transform.position;
    }
    private void Update()
    {
        if (doorIsOpen=="open")
        {
            transform.position = Vector3.SmoothDamp(transform.position, new Vector3(vec.x+3, vec.y, vec.z), ref velocity, 0.1f);
        }
        else if(doorIsOpen=="close")
        {
            transform.position = Vector3.SmoothDamp(transform.position,vec, ref velocity, 0.1f);
        }
    }
}
