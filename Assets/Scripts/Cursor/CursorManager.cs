using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    public RectTransform hand;

    //lambda表达式，左边是参数右边是返回值
    private Vector3 mouseWorldPos => Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,Input.mousePosition.y,0));
    bool canClick;

    private ItemName currentItem;

    private bool holdItem;


    private void OnEnable()
    {
        //鼠标要添加的事件有
        //1.道具选中的状态控制，如果选中，鼠标添加"手"的图标
        //2.道具是否使用过的控制，如果道具已经使用了，去除图标，表示选中结束
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
        //选中的物体none,消除图标
        currentItem = ItemName.None;
        holdItem = false;
        hand.gameObject.SetActive(false);
    }

    private void onItemSelectEvent(ItemDetails itemDetails, bool isSelected)
    {
        holdItem = isSelected; //将是否选中传给本类中的参数，这样本类中所有函数都可以使用
        if (isSelected)
        {
            currentItem = itemDetails.itemName;
        }
        hand.gameObject.SetActive(holdItem);//根据是否选中来确认是否显示
    }

    private void Update()
    {
        canClick = ObjectAtMousePosition();

        if(canClick && Input.GetMouseButtonDown(0))
        {
            //检测互动情况
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
                if (holdItem) //选中钥匙或道具才能互动，而不是点击就互动
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
}
