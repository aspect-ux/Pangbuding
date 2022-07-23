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
        //���岻�ڱ�����
        if (itemDetails == null)
        {
            Debug.Log("����Ϊ��");
            //��û������ʱ
            slot.SetEmpty(); //�ر�
            currentIndex = -1; 
            //�رհ�ť
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
