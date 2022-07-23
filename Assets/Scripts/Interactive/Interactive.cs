using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactive : MonoBehaviour
{
    public ItemName requireItem;
    public bool isDone; //是否触发互动物体的点击

    public void CheckItem(ItemName itemName)
    {
        //当选中的物体是符合要求的，触发点击事件，
        if(itemName == requireItem && !isDone)
        {
            //点击了物体，触发了事件，移除使用过的道具
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
