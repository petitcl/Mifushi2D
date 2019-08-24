using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    [SerializeField]
    public float smoothing = 1.0f;

    [SerializeField]
    public float parallaxStrength = 1.0f;

    private Transform _camera;
    private Transform[] _backgrounds;
    private Vector3 _previousCameraPosition;
    private float[] _parallaxScales;

    public void RefreshChildCount()
    {
        _backgrounds = new Transform[transform.childCount];
        for (int i = 0; i < transform.childCount; ++i)
        {
            _backgrounds[i] = transform.GetChild(i);
        }
        _parallaxScales = new float[transform.childCount];
        for (int i = 0; i < transform.childCount; ++i)
        {
            _parallaxScales[i] = _backgrounds[i].position.z * -1;
        }
    }

    private void Awake()
    {
        _camera = Camera.main.transform;    
        _previousCameraPosition = _camera.position;
        RefreshChildCount();
    }

    private void Update() {
        for (int i = 0; i < _backgrounds.Length; ++i)
        {
            float parallax = (_previousCameraPosition.x - _camera.position.x) * _parallaxScales[i]  * parallaxStrength;
            Vector3 targetPosition = new Vector3(
               _backgrounds[i].position.x + parallax,
               _backgrounds[i].position.y,
               _backgrounds[i].position.z
            );
            _backgrounds[i].position = Vector3.Lerp(_backgrounds[i].position, targetPosition, smoothing * Time.deltaTime);
        }
        _previousCameraPosition = _camera.position;
    }
}
