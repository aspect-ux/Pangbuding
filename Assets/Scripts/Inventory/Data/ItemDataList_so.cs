using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="itemDatalist_so",menuName ="Inventory/itemDataList_so")]
public class ItemDataList_so : ScriptableObject
{
    public List<ItemDetails> itemDetailsList;

    public ItemDetails GetItemDetails(ItemName itemName)
    {
        //用名字获取背包物体信息
        return itemDetailsList.Find(i => i.itemName == itemName);
    }
}

[System.Serializable]
public class ItemDetails
{
   
    public ItemName itemName;

    public Sprite itemSprite;
    
}
