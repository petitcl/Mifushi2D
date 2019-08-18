using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class LevelEnd : MonoBehaviour
{
    [SerializeField]
    public ParticleSystem ParticleSystem;

    private SignalBus _signalBus;

    [Inject]
    public void Init(SignalBus signalBus)
    {
        _signalBus = signalBus;
        _signalBus.Subscribe<LevelEndSignal>(s => this.OnLevelEnd(s));
    }

    public void OnLevelEnd(LevelEndSignal signal)
    {
        Debug.Log("LevelEnd.OnLevelEnd");
        ParticleSystem.Play();
    }
}
