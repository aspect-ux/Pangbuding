using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CursorManager : MonoBehaviour
{
    public RectTransform hand;

    //lambda���ʽ������ǲ����ұ��Ƿ���ֵ
    private Vector3 mouseWorldPos => Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,Input.mousePosition.y,0));
    bool canClick;

    private ItemName currentItem;

    private bool holdItem;


    private void OnEnable()
    {
        //���Ҫ��ӵ��¼���
        //1.����ѡ�е�״̬���ƣ����ѡ�У�������"��"��ͼ��
        //2.�����Ƿ�ʹ�ù��Ŀ��ƣ���������Ѿ�ʹ���ˣ�ȥ��ͼ�꣬��ʾѡ�н���
        EventHandler.itemUsedEvent += onItemUsedEvent;
        EventHandler.ItemSelectEvent += onItemSelectEvent;
    }
    private void OnDisable()
    {
        EventHandler.itemUsedEvent -= onItemUsedEvent;
        EventHandler.ItemSelectEvent -= onItemSelectEvent;
    }

    private void onItemUsedEvent(ItemName obj)
    {
        //ѡ�е�����none,����ͼ��
        currentItem = ItemName.None;
        holdItem = false;
        hand.gameObject.SetActive(false);
    }

    private void onItemSelectEvent(ItemDetails itemDetails, bool isSelected)
    {
        holdItem = isSelected; //���Ƿ�ѡ�д��������еĲ������������������к���������ʹ��
        if (isSelected)
        {
            currentItem = itemDetails.itemName;
        }
        hand.gameObject.SetActive(holdItem);//�����Ƿ�ѡ����ȷ���Ƿ���ʾ
    }

    private void Update()
    {
        Debug.Log(mouseWorldPos);
        canClick = ObjectAtMousePosition();

        //��Ϊ��ʱ�������UI,���ܴ���UI�µ�����
        //if (InteracWithUI()) return;   //���������UI�Ͳ����е���¼����ж�

        if(canClick && Input.GetMouseButtonDown(0))
        {
            //��⻥�����
            clickAction(ObjectAtMousePosition().gameObject);
        }

        if (hand.gameObject.activeInHierarchy)
        {
            hand.position = Input.mousePosition;
        }
    }

    private void clickAction(GameObject clickObject)
    {
        switch (clickObject.tag)
        {
            case "Teleport":
                var teleport = clickObject.GetComponent<Teleport>();
                teleport?.TeleportToScene();
                break;
            case "item":
                var item = clickObject.GetComponent<Item>();
                item?.ItemClicked();
                break;
            case "Interactive":
                var interactive = clickObject.GetComponent<Interactive>();
                if (holdItem) //ѡ��Կ�׻���߲��ܻ����������ǵ���ͻ���
                {
                    interactive?.CheckItem(currentItem);
                    
                }
                else
                {
                    interactive?.EmptyClick();
                }
                break;
            default:
                break;
        }
    }

    public Collider2D ObjectAtMousePosition()
    {
        return Physics2D.OverlapPoint(mouseWorldPos);
    }


    /// <summary>
    /// �ж��Ƿ�����UI����
    /// </summary>
    /// <returns></returns>
    private bool InteracWithUI()
    {
        //��UI��Ϊ���ҵ�ǰ���������UI
        if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
        {
            return true;
        }
        return false;
    }
}
