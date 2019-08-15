using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    public GameObject player;

    [SerializeField]
    public Vector2 positionOffset;

    private void Update()
    {
        Vector3 startPos = transform.position;
        Vector3 endPos = player.transform.position;
        endPos.x += positionOffset.x;
        endPos.y += positionOffset.y;
        endPos.z = -100;

        transform.position = endPos;
    }
}
