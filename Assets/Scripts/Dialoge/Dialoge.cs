using Unity.VisualScripting;
using UnityEngine;

public class Dialoge : MonoBehaviour
{
    public string[] dialoge;
    public DialogeManager dialogeManager;
    private Collider2D isRange;
    private float rad = 3;
    private bool pressF=true;
    private void Update()
    {
        GameObject prof = GameObject.Find("prof");

        Collider2D isRange = Physics2D.OverlapCircle(prof.transform.position,rad);
        if (Input.GetKeyDown(KeyCode.F)&&isRange.name=="Player"&&pressF)
        {
            GameObject gmO = GameObject.Find("kaan");
            gmO.SetActive(false);
            dialogeManager.DialogeStart(dialoge);
            pressF = false;
        }
    }
}
