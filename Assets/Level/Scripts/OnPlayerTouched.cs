using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

[AddComponentMenu("Scripts/Mifushi 2D/OnPlayerTouched")]
public class OnPlayerTouched : MonoBehaviour
{
    [SerializeField]
    public ObjectTouchType objectTouchType;

    [SerializeField]
    public bool onlyOnce = false;

    [SerializeField]
    public UnityEvent onTouched;

    private SignalBus _signalBus;

    [Inject]
    public void Init(SignalBus signalBus)
    {
        _signalBus = signalBus;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log(String.Format("OnPlayerTouched.OnCollisionEnter2D -> collision between {0} and {1}", gameObject.name, collision.gameObject.name));
            _signalBus.Fire(new PlayerTouchedSignal() { touching = collision.gameObject, objectTouchType = objectTouchType });
            onTouched?.Invoke();
            if (onlyOnce)
            {
                gameObject.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            Debug.Log(String.Format("OnPlayerTouched.OnTriggerEnter2D -> collision between {0} and {1}", gameObject.name, collider.gameObject.name));
            _signalBus.Fire(new PlayerTouchedSignal() { touching = collider.gameObject, objectTouchType = objectTouchType });
            onTouched?.Invoke();
            if (onlyOnce)
            {
                gameObject.SetActive(false);
            }
        }
        onTouched?.Invoke();
    }
}
