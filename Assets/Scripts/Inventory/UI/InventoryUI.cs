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
        //��ѡȡ���ߵ�ʱ�򣬵���������ߣ���ô��ʹ��ťʧЧ���ұ�ͬ��
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
        //����ť���úú󣬿�ʼ����UI
        EventHandler.CallItemChange(index);
    }
}
