using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactive : MonoBehaviour
{
    public ItemName requireItem;
    public bool isDone; //�Ƿ񴥷���������ĵ��

    public void CheckItem(ItemName itemName)
    {
        //��ѡ�е������Ƿ���Ҫ��ģ���������¼���
        if(itemName == requireItem && !isDone)
        {
            //��������壬�������¼����Ƴ�ʹ�ù��ĵ���
            isDone = true;
            onClickedAction();
            EventHandler.CallItemUsedEvent(itemName);
        }
        else
        {
            EmptyClick();
        }
    }

    protected virtual void onClickedAction()
    {
        Debug.Log("select wrong");
    }

    public void EmptyClick()
    {

    }
}
