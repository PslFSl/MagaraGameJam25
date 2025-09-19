using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DialogeManager : MonoBehaviour
{
    int index;
    public Text dialogText;
    public float writeSpeed=0.1f;
    string[] dialogeArr;
    public void DialogeStart(string[] dialoge)
    {
        index = 0;
        dialogeArr = dialoge;
        WriteSentences();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && dialogeArr[index]==dialogText.text)
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

    }
    

}
