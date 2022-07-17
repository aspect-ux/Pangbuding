using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : Singleton<InventoryManager>
{
    //���嵥������������
    public ItemDataList_so itemData;

    [SerializeField]private List<ItemName> itemList = new List<ItemName>();
    public void AddItem(ItemName itemName)
    {
        if (!itemList.Contains(itemName))
        {
            itemList.Add(itemName); 
            //ui��ʾ
        }
    }
}
