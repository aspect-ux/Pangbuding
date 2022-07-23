using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MailBox : Interactive
{
    private SpriteRenderer spriteRenderer;

    private Collider2D coll;

    public Sprite openSprite;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        coll = GetComponent<Collider2D>();

    }

    private void OnEnable()
    {
        EventHandler.afterSceneLoad += onAfterSceneLoad;
    }
    private void OnDisable()
    {
        EventHandler.afterSceneLoad -= onAfterSceneLoad;
    }

    private void onAfterSceneLoad()
    {
        //�������س��������û�з���������͹ص���Ʊ
        //�������������ʹ����䣬�ر���ײ��
        if (!isDone)
        {
            transform.GetChild(0).gameObject.SetActive(false);//����û�д����䣬�͹ص���Ʊ
        }
        else
        {
            //�������䣬��Ϊ����¼�ֻ�ܴ���һ�Σ�֮���ٴλص��ó�����
            //����Ӧ��һֱ�򿪣���ʾ�¼���������
            spriteRenderer.sprite = openSprite;//�����ͼƬ���ɴ򿪵�����
            coll.enabled = false;   //����ײ��ر�
        }
    }

    /// <summary>
    /// ��������ִ�Ʊ
    /// </summary>
    protected override void onClickedAction()
    {
        spriteRenderer.sprite = openSprite;//�����ͼƬ���ɴ򿪵�����
        transform.GetChild(0).gameObject.SetActive(true);
    }
}
