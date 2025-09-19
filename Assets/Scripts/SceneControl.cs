using System.Collections;
using System.Dynamic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneControl : MonoBehaviour
{
    public void waitford(int delay ,PlayerEvent playerScript)
    {
        StartCoroutine(WaitForDeath(delay,playerScript));
    }
    IEnumerator WaitForDeath(int delay,PlayerEvent playerScript)
    {
        Destroy(playerScript);
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }
}
