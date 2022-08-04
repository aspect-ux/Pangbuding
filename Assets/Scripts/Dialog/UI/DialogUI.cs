using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DialogUI : MonoBehaviour
{
    public GameObject panel;
    public Text dialogueText;


    private void OnEnable()
    {
        EventHandler.ShowDialogueEvent += onShowDialogueEvent;
    }
    private void OnDisable()
    {
        EventHandler.ShowDialogueEvent -= onShowDialogueEvent;
    }

    private void onShowDialogueEvent(string data)
    {
        Debug.Log("weh");
        ShowDialogue(data);
        Debug.Log(data);
    }

    private void ShowDialogue(string dialogue)
    {
        if (dialogue != string.Empty)
        {
            panel.SetActive(true);
        }
            
        else
            panel.SetActive(false);
        dialogueText.text = dialogue;
    }
}
