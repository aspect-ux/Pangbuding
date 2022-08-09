using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MiniGame : MonoBehaviour
{
    public UnityEvent onGameFinish;

    [SceneName] public string gameName;

    public bool isPass;

    public void UpdateMiniGameState()
    {
        if (isPass)
        {
            GetComponent<Collider2D>().enabled = false; //防止再次点击生效
            GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
            onGameFinish?.Invoke();
        }
    }
}
