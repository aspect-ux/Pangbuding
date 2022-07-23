using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventHandler
{
    //�����������UI��ί��
    public static event Action<ItemDetails, int> UpdateUIEvent;

    //public static string test(ItemDetails itemDetails, int index) { return "where"; }
    public static void CallUpdateUIEvent(ItemDetails itemDetails,int index)
    {
        UpdateUIEvent?.Invoke(itemDetails,index);
    }


    //�����л���������ж�ص�ǰ����֮ǰҪ����Ʒ���ݱ��棬���ڼ����³�������Ҫ����Ʒ���ݼ��س���
    public static event Action beforeSceneUnload;
    public static void CallBeforeSceneUload()
    {
        beforeSceneUnload?.Invoke();
    }
    public static event Action afterSceneLoad;
    public static void CallAfterSceneLoad() { afterSceneLoad?.Invoke(); }



    //ѡ���������¼�
    public static event Action<ItemDetails,bool> ItemSelectEvent;

    public static void CallItemSelectEvent(ItemDetails itemDetails,bool isSelected)
    {
        ItemSelectEvent?.Invoke(itemDetails,isSelected);
    }

    //�Ƴ�ʹ�ù�������
    public static event Action<ItemName> itemUsedEvent;

    public static void CallItemUsedEvent(ItemName itemName)
    {
        itemUsedEvent?.Invoke(itemName);
    }

}
