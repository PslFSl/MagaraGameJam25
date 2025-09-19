using UnityEngine;

public class CamFallow : MonoBehaviour
{
    private GameObject myChar;
    Vector3 velocity = Vector3.zero;
    private void Start()
    {
        myChar = GameObject.FindGameObjectWithTag("Player");
    }
    private void Update()
    {
        transform.position = Vector3.SmoothDamp(transform.position,myChar.transform.position + new Vector3(0,2,-10), ref velocity, 0.2f);
    }
}
