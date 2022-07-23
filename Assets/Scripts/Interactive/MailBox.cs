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
        //场景加载出来后，如果没有发生点击，就关掉船票
        //如果发生点击，就打开信箱，关闭碰撞体
        if (!isDone)
        {
            transform.GetChild(0).gameObject.SetActive(false);//比如没有打开信箱，就关掉船票
        }
        else
        {
            //打开了信箱，因为点击事件只能触发一次，之后再次回到该场景，
            //信箱应当一直打开，表示事件触发过了
            spriteRenderer.sprite = openSprite;//点击后将图片换成打开的信箱
            coll.enabled = false;   //将碰撞体关闭
        }
    }

    /// <summary>
    /// 打开信箱出现船票
    /// </summary>
    protected override void onClickedAction()
    {
        spriteRenderer.sprite = openSprite;//点击后将图片换成打开的信箱
        transform.GetChild(0).gameObject.SetActive(true);
    }
}
