using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : Singleton<InventoryManager>
{
    //���嵥�������Ƶ�������,�����ȡ�Ƿ���ڣ�������ߣ�ɾ������
    public ItemDataList_so itemData;

    [SerializeField]private List<ItemName> itemList = new List<ItemName>();

    private void OnEnable()
    {
        EventHandler.itemUsedEvent += onItemUsedEvent;
        EventHandler.ItemChange += onItemChange;
        EventHandler.afterSceneLoad += onAfterSceneLoad;
    }

    private void OnDisable()
    {
        EventHandler.itemUsedEvent -= onItemUsedEvent;
        EventHandler.ItemChange -= onItemChange;
        EventHandler.afterSceneLoad -= onAfterSceneLoad;
    }

    private void onAfterSceneLoad()
    {
        if (itemList.Count == 0)  //���غ����UI
            EventHandler.CallUpdateUIEvent(null, -1);
    }

    private void onItemChange(int index)
    {
        if (index >= 0 && index < itemList.Count)
        {
            //���indexû�г���Χ���͸���UI
            ItemDetails item = itemData.itemDetailsList[index];
            EventHandler.CallUpdateUIEvent(item, index);
        }
    }

    private void onItemUsedEvent(ItemName itemName)
    {
        //��ʹ�ù�����Ʒ�Ƴ��嵥
        //var index = GetItemIndex(itemName);
        //itemList.RemoveAt(index);


        //����UI
        if (itemList.Count == 0)
            EventHandler.CallUpdateUIEvent(null,-1);
    }

    public void AddItem(ItemName itemName)
    {
        if (!itemList.Contains(itemName))
        {
            itemList.Add(itemName);
            //ui��ʾ
            try
            {
                EventHandler.CallUpdateUIEvent(itemData.GetItemDetails(itemName), itemList.Count - 1);
            }
            catch
            {
                if (itemData.GetItemDetails(itemName) == null)
                    Debug.Log("wrong");
            }
            finally
            {
                
            }
            
        }
        
    }

    //�첽��������
    private int GetItemIndex(ItemName itemName)
    {
        for(int i = 0; i < itemList.Count; i++)
        {
            if (itemList[i] == itemName)
            {
                return i;
            }
        }
        return -1;
    }
}
