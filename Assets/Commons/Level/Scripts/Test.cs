using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Test : MonoBehaviour
{
    private LevelManager _levelManager;

    [Inject]
    public void Init(LevelManager levelManager)
    {
        _levelManager = levelManager;
        Debug.Log(levelManager != null);
    }

    public void OnClick()
    {
        Debug.Log("click");
    }
}
