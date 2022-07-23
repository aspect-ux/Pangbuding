using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class SlotUI : MonoBehaviour,IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    //SlotUI是作用于slot物体上,用于在右下角显示鼠标所选中的物体的脚本，即物体提示信息窗口，与tooltip搭配
    //物体ui
    public Image itemImage;
    public ItemDetails currentItem;
    private bool isSelected;
    public ItemTooltip toolTip;



    public void SetItem(ItemDetails itemDetails)
    {
       
        currentItem = itemDetails;
        //显示物体的图像以及细节
        this.gameObject.SetActive(true);

        this.gameObject.GetComponent<Image>().sprite = itemDetails.itemSprite;
        this.gameObject.GetComponent<Image>().SetNativeSize();

        itemImage.sprite = itemDetails.itemSprite;
        itemImage.SetNativeSize();
        Debug.Log("slot " + itemDetails.itemName);

    }

    public void SetEmpty()
    {
        this.gameObject.SetActive(false);
    }

    /// <summary>
    /// 点击物体，调用选中事件
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerClick(PointerEventData eventData)
    {
       // Debug.Log("pointenter");
        isSelected = !isSelected;//再次选中，取消或者选中
        EventHandler.CallItemSelectEvent(currentItem,isSelected);
    }

    /// <summary>
    /// 鼠标滑入slot区域
    /// </summary>
    /// <param name="eventData"></param>
    /// <exception cref="System.NotImplementedException"></exception>
    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("jinru");
        //当进入右下角提示窗口
        if (this.gameObject.activeInHierarchy)
        {
            toolTip.gameObject.SetActive(true);//显示文本
            if(currentItem != null)
                toolTip.ItemNameUpdate(currentItem.itemName);//更新文本
        }
    }
    /// <summary>
    /// 鼠标滑出区域
    /// </summary>
    /// <param name="eventData"></param>
    /// <exception cref="System.NotImplementedException"></exception>
    public void OnPointerExit(PointerEventData eventData)
    {
        toolTip.gameObject.SetActive(false);//离开就不显示
    }
}
