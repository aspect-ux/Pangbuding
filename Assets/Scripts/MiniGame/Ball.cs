using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// �л���ͼ
/// </summary>
public class Ball : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    public BallDetails ballDetails;

    public bool isMatch;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void SetupBall(BallDetails ball)
    {
        ballDetails = ball;
        if (isMatch)
        {
            SetRight();
        }
        else
        {
            SetWrong();
        }

    }

    public void SetWrong()
    {
        spriteRenderer.sprite = ballDetails.wrongSprite;
    }

    public void SetRight()
    {
        spriteRenderer.sprite = ballDetails.rightSprite;
    }


}
