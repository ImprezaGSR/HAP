using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public string sentence;
    public float delay = 0.01f;
    private TextMeshProUGUI dialogueText;
    private bool isSkipped = false;
    

    public void SetSentence(TextMeshProUGUI dText, string text){
        dialogueText = dText;
        sentence = text;
        StartCoroutine(Continuous(sentence));
    }
    public void Skip(){
        if(dialogueText.text != sentence){
            dialogueText.text = sentence;
            StopCoroutine(Continuous(sentence));
            isSkipped = true;
        }else{
            if(!dialogueText.transform.parent.parent.Find("Upgrade").gameObject.activeSelf){
                dialogueText.transform.parent.gameObject.SetActive(false);
            }else{
                dialogueText.gameObject.SetActive(false);
            }
        }
    }

    IEnumerator Continuous(string sentence){
        dialogueText.text = "";
        foreach(char letter in sentence.ToCharArray()){
            if(isSkipped){
                isSkipped = false;
                break;
            }else{
                dialogueText.text += letter;
                yield return new WaitForSeconds(delay);
                yield return null;
            }
        }
    }
}
