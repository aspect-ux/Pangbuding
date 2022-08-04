using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DialogueController : MonoBehaviour
{
    public DialogData_so dialogDataEmpty;
    public DialogData_so dialogDataFinish;

    Stack<string> dialogDataEmpty_Stack;
    Stack<string> dialogDataFinish_Stack;

    private bool isTalking;

    private void Awake()
    {
        FillDialogStack(); //���Ի�������䵽��ջ��ʹ�õ�һ����������
    }

    private void FillDialogStack()
    {
        dialogDataFinish_Stack = new Stack<string>();
        dialogDataEmpty_Stack = new Stack<string>();

        for(int i = dialogDataEmpty.dialogueList.Count-1; i >= 0; i--)
        {
            dialogDataEmpty_Stack.Push(dialogDataEmpty.dialogueList[i]);
        }
        for(int i = dialogDataFinish.dialogueList.Count-1; i >= 0; i--)
        {
            dialogDataFinish_Stack.Push((string)dialogDataFinish.dialogueList[i]);
        }
    }

    public void ShowDialogEmpty()
    {
        Debug.Log("show");
        if(!isTalking)
            StartCoroutine(DialogueRoutine(dialogDataEmpty_Stack));
    }

    public void ShowDialogFinish()
    {
        if(!isTalking)
            StartCoroutine(DialogueRoutine(dialogDataFinish_Stack));
    }



    private IEnumerator DialogueRoutine(Stack<string> data)
    {
        isTalking = true;
        //��ջ�л��жԻ�
        if (data.Count>0)
        {
            EventHandler.CallShowDialogueEvent(data.Peek());
            data.Pop();
            yield return null;
            isTalking = false;
            EventHandler.CallChangeGameState(GameStates.Pause);
        }
        else
        {
            //���������ˣ���������
            EventHandler.CallShowDialogueEvent(string.Empty);
            FillDialogStack();
            isTalking = false;
            EventHandler.CallChangeGameState(GameStates.GamePlay);
        }
    }

}
