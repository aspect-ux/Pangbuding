using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InventoryUI : MonoBehaviour
{
    public SlotUI slot;
   public Button leftButton,rightButton;
    public int currentIndex;

    private void OnEnable()
    {
        EventHandler.UpdateUIEvent += onUpdataUIEvent;
    }
    private void OnDisable()
    {
        EventHandler.UpdateUIEvent -= onUpdataUIEvent;
    }

    private void onUpdataUIEvent(ItemDetails itemDetails, int index)
    {
        //物体不在背包里
        if (itemDetails == null)
        {
            Debug.Log("背包为空");
            //当没有物体时
            slot.SetEmpty(); //关闭
            currentIndex = -1; 
            //关闭按钮
            leftButton.interactable = false;
            rightButton.interactable = false;
        }
        else
        {
            slot.SetItem(itemDetails);
            currentIndex = index;

            if(index > 0)
                leftButton.interactable = true;
            if(index == -1)
            {
                leftButton.interactable = false;
                rightButton.interactable = false;
            }
        }
    }

    public void SwitchItem(int amount)
    {
        var index = currentIndex + amount;
        //在选取道具的时候，当按到最左边，那么就使左按钮失效；右边同理
        if (index < currentIndex)
        {
            leftButton.interactable = false;
            rightButton.interactable = true;
        }
        else if(index > currentIndex)
        {
            leftButton.interactable = true;
            rightButton.interactable = false;
        }
        else
        {
            leftButton.interactable = true;
            leftButton.interactable = true;
        }
        //当按钮设置好后，开始更新UI
        EventHandler.CallItemChange(index);
    }
}
