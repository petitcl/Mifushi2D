using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

[AddComponentMenu("Scripts/Mifushi 2D/OnPlayerTouched")]
public class OnPlayerTouched : MonoBehaviour
{
    private SignalBus _signalBus;

    private ColoredGameObject _coloredGameObject;

    [Inject]
    public void Init(SignalBus signalBus)
    {
        _signalBus = signalBus;
    }

    private void Start()
    {
        _coloredGameObject = GetComponent<ColoredGameObject>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("OnPlayerTouched.OnCollisionEnter2D -> collision between [" + gameObject.name + "] and [" + collision.gameObject.name + "]");
            _signalBus.Fire(new PlayerTouchedSignal() { touching = collision.gameObject });
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            Debug.Log("OnPlayerTouched.OnTriggerEnter2D -> collision between [" + gameObject.name + "] and [" + collider.gameObject.name + "]");
            _signalBus.Fire(new PlayerTouchedSignal() { touching = collider.gameObject });
        }
    }
}
