using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    private void Start()
    {
        EventHandler.CallChangeGameState(GameStates.GamePlay);
    }
}
