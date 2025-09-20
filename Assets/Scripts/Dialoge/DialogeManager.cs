using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DialogeManager : MonoBehaviour
{
    int index;
    public Text dialogText;
    public float writeSpeed=0.1f;
    string[] dialogeArr;
    public GameObject Image;
    public bool isStarted=false;
    public void DialogeStart(string[] dialoge)
    {
        Image.SetActive(true); 
        index = 0;
        dialogeArr = dialoge;
        isStarted = true;
        WriteSentences();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F)&&isStarted && dialogeArr[index]==dialogText.text)
        {
            if (dialogeArr[index] != null)
            {
                NextSentence();
            }
               
        } 
    }
    void WriteSentences()
    {
        if (index < dialogeArr.Length)
        {
            StartCoroutine(TypeSentence(dialogeArr[index]));
        }
        else { DialogeEnd(); }
    }
    IEnumerator TypeSentence(string dialoge)
    {
        dialogText.text = "";
        foreach(char letter in dialoge.ToCharArray())
        {
            dialogText.text += letter;
            yield return new WaitForSeconds(writeSpeed);
        }

    }
    public void NextSentence()
    {
        index++;
        WriteSentences();
    }
    private void DialogeEnd()
    {
        isStarted = false;
        Image.SetActive(false);
    }
    

}
