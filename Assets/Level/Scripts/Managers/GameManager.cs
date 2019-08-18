using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GameManager
{

    public void OnLevelEnd(LevelEndSignal signal)
    {
        Debug.Log("OnLevelEnd");
    }
}
