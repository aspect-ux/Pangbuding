using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemTooltip : MonoBehaviour
{
    //�����������ʾѡ�����������
    public Text itemNameText;

    public void ItemNameUpdate(ItemName itemName)
    {
        //�﷨��ͨ��ö����������text���и�ֵ
        itemNameText.text = itemName switch
        {
            ItemName.Key => "һ��Կ��",
            ItemName.Ticket => "һ�Ŵ�Ʊ",
            _ => ""
        };
    }
}
