using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DialogueController))]
public class CharacterH2 : Interactive
{
    DialogueController dialogueController;

    private void Awake()
    {
        dialogueController = GetComponent<DialogueController>();
    }

    public override void EmptyClick()
    {
        if(isDone)
            dialogueController.ShowDialogFinish();
        else
            dialogueController.ShowDialogEmpty();
    }

    protected override void onClickedAction()
    {
        dialogueController.ShowDialogFinish();
    }

}
