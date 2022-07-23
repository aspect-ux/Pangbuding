using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemTooltip : MonoBehaviour
{
    //这个类用于提示选中物体的名称
    public Text itemNameText;

    public void ItemNameUpdate(ItemName itemName)
    {
        //语法糖通过枚举类型来对text进行赋值
        itemNameText.text = itemName switch
        {
            ItemName.Key => "一把钥匙",
            ItemName.Ticket => "一张船票",
            _ => ""
        };
    }
}
