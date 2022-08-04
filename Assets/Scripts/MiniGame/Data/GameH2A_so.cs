using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="new miniGame",menuName ="MiniGame/GameH2A_Data")]
public class GameH2A_so : ScriptableObject
{
    [Header("存放球的名字和图片")]
    public List<BallDetails> ballDetailsList;

    [Header("游戏逻辑数据")]
    public List<Connections> LineConnectionsList;

    public List<BallName> ballNamesList;

    public BallDetails GetBallDetails(BallName ballName)
    {
        return ballDetailsList.Find(x => x.ballName == ballName);
    }
}

[System.Serializable]
public class BallDetails
{
    public BallName ballName;

    public Sprite wrongSprite;

    public Sprite rightSprite;
}

[System.Serializable]
public class Connections
{
    public int from;
    public int to;
}
