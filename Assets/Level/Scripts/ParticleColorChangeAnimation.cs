using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ParticleColorChangeAnimation : MonoBehaviour
{
    private SignalBus _signalBus;
    private ColorsManager _colorsManager;

    [SerializeField]
    private ParticleSystem _particleSystem;

    [Inject]
    public void Init(SignalBus signalBus, ColorsManager colorsManager)
    {
        _signalBus = signalBus;
        _colorsManager = colorsManager;
    }

    public void OnPlayerChangeColor(PlayerChangedColorSignal signal)
    {
        if (signal.oldColor == GameColor.NONE)
        {
            return;
        }
        Color startColor = _colorsManager.GetColor(signal.newColor);
        ParticleSystem.MainModule mainModule = _particleSystem.main;
        mainModule.startColor = new ParticleSystem.MinMaxGradient(startColor);
        _particleSystem.Stop(true, ParticleSystemStopBehavior.StopEmitting);
        _particleSystem.Play();
    }

    private void Awake()
    {
        _signalBus.Subscribe<PlayerChangedColorSignal>(s => this.OnPlayerChangeColor(s));
    }
}
