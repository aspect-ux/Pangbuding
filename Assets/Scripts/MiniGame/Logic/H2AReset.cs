using System.Collections;
using System.Collections.Generic;
using DG.Tweening;  //DOTWEEN²å¼þ
using UnityEngine;

public class H2AReset : Interactive
{
    private Transform gearSprite;

    private void Awake()
    {
        gearSprite = transform.GetChild(0);
    }


    public override void EmptyClick()
    {
        gearSprite.DOPunchRotation(Vector3.forward*180,1,1,0);
        GameController.Instance.ResetGame();
    }
}
