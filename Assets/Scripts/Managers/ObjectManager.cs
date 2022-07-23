using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    //����������¼�����е��ߵĴ�����񣬿ɻ����¼���һ���ԣ��������
    //��ֹ��Ϊ�л��������������ݶ�ʧ����


    private Dictionary<ItemName,bool> itemAvaliableDict = new Dictionary<ItemName,bool>();
    private Dictionary<string,bool> interactiveStateDict = new Dictionary<string,bool>();   

    private void OnEnable()
    {
        EventHandler.beforeSceneUnload += onBeforeSceneUload;
        EventHandler.afterSceneLoad += onAfterSceneLoad;
        EventHandler.UpdateUIEvent += onUpdataUIEvent;
    }

    private void OnDisable()
    {
        EventHandler.beforeSceneUnload -= onBeforeSceneUload;
        EventHandler.afterSceneLoad -= onAfterSceneLoad;
        EventHandler.UpdateUIEvent -= onUpdataUIEvent;
    }


    private void onAfterSceneLoad()
    {
        //�߼������ڣ��ڱ�ĳ����л�ȡ�ĵ��ߣ���Ҫ����һ�������дӱ�����ʹ�û�ѡ��
        //��Ҫ������Щ��������
        //Ȼ�󽫳����е����е��߼��������ߵ��ֵ���
        foreach (var item in FindObjectsOfType<Item>())
        {
            //�����ǰ������û��֮ǰ��������壬�Ǿͽ���������ʾ����
            if (!itemAvaliableDict.ContainsKey(item.ItemName))
            {
                Debug.Log("�����еĵ��߻����ڱ�����");
                itemAvaliableDict.Add(item.ItemName, true);
            }
            else//����У���ô�Ͳ��ض��һ�٣�ֱ�������屣��֮ǰ��״̬����
            {
                Debug.Log("�������Ѿ�����"+itemAvaliableDict.Count);

                item.gameObject.SetActive(!itemAvaliableDict[item.ItemName]);
            }
        }

        //�����³��������ػ�������
        foreach (var item in FindObjectsOfType<Interactive>())
        {
            if (interactiveStateDict.ContainsKey(item.name))
            {
                item.isDone = interactiveStateDict[item.name];
            }
            else
            {
                interactiveStateDict.Add(item.name, item.isDone);
            }
        }
    }

    private void onBeforeSceneUload()
    {
        //�����Ѿ���ȡ�ĵ���
        foreach (var item in FindObjectsOfType<Item>())
        {
            if (!itemAvaliableDict.ContainsKey(item.ItemName))
            {
                itemAvaliableDict.Add(item.ItemName, true);
            }
        }

        //ж�س���ǰ�����浱ǰ���пɻ����¼��������������
        foreach(var item in FindObjectsOfType<Interactive>())
        {
            if (interactiveStateDict.ContainsKey(item.name))
            {
                interactiveStateDict[item.name] = item.isDone;
            }
            else
            {
                interactiveStateDict.Add(item.name, item.isDone);
            }
        }
    }

    private void onUpdataUIEvent(ItemDetails itemDetails, int arg2)
    {
        if(itemDetails != null)
        {
            itemAvaliableDict[itemDetails.itemName] = false;
        }
    }
}
