using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class SlotUI : MonoBehaviour,IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    //SlotUI��������slot������,���������½���ʾ�����ѡ�е�����Ľű�����������ʾ��Ϣ���ڣ���tooltip����
    //����ui
    public Image itemImage;
    public ItemDetails currentItem;
    private bool isSelected;
    public ItemTooltip toolTip;



    public void SetItem(ItemDetails itemDetails)
    {
       
        currentItem = itemDetails;
        //��ʾ�����ͼ���Լ�ϸ��
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
    /// ������壬����ѡ���¼�
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerClick(PointerEventData eventData)
    {
       // Debug.Log("pointenter");
        isSelected = !isSelected;//�ٴ�ѡ�У�ȡ������ѡ��
        EventHandler.CallItemSelectEvent(currentItem,isSelected);
    }

    /// <summary>
    /// ��껬��slot����
    /// </summary>
    /// <param name="eventData"></param>
    /// <exception cref="System.NotImplementedException"></exception>
    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("jinru");
        //���������½���ʾ����
        if (this.gameObject.activeInHierarchy)
        {
            toolTip.gameObject.SetActive(true);//��ʾ�ı�
            if(currentItem != null)
                toolTip.ItemNameUpdate(currentItem.itemName);//�����ı�
        }
    }
    /// <summary>
    /// ��껬������
    /// </summary>
    /// <param name="eventData"></param>
    /// <exception cref="System.NotImplementedException"></exception>
    public void OnPointerExit(PointerEventData eventData)
    {
        toolTip.gameObject.SetActive(false);//�뿪�Ͳ���ʾ
    }
}
