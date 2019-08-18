using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour
{
    [SerializeField]
    public Transform pointA;

    [SerializeField]
    public Transform pointB;

    [SerializeField]
    public float travelTime = 1.0f;

    private float _time = 0.0f;
    private Vector2 _direction = Vector2.right;

    private void Start()
    {
        _time = 0.0f;
    }

    private void Update()
    {
        Transform startPosition = _direction == Vector2.right ? pointA : pointB;
        Transform endPosition = _direction == Vector2.right ? pointB : pointA;
        _time += Time.deltaTime / travelTime;
        transform.position = Vector3.Lerp(startPosition.position, endPosition.position, _time);
        if (Vector3.Distance(transform.position, endPosition.position) < Vector3.kEpsilon)
        {
            _time = 0.0f;
            // reverse direction
            _direction = -_direction;
        }
    }
}
