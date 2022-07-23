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
        }
    }
}
