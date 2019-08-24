using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class TilingBackground : MonoBehaviour
{
    public int offsetX = 2;
    public Transform rightInstance;
    public Transform leftInstance;
    public bool reverseScale = false;

    private float _spriteWidth = 0.0f;
    private float _killDistance = 0.0f; 
    private Camera _camera;

    private void Awake()
    {
        _camera = Camera.main;
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        _spriteWidth = renderer.sprite.bounds.size.x;
        _killDistance = _spriteWidth * 1.5f;
    }

    private void Update()
    {
        float cameraHorizontalExtend = _camera.orthographicSize * Screen.width / Screen.height;
        float rightVisibleEdge = (transform.position.x + _spriteWidth / 2) - cameraHorizontalExtend;
        float leftVisibleEdge = (transform.position.x - _spriteWidth / 2) + cameraHorizontalExtend;

        if (!leftInstance || !rightInstance)
        {
            if (_camera.transform.position.x >= (rightVisibleEdge - offsetX) && !rightInstance)
            {
                MakeNewBuddy(1);
            }
            else if (_camera.transform.position.x <= (leftVisibleEdge + offsetX) && !leftInstance)
            {
                MakeNewBuddy(-1);
            }
        }

        if (_camera.transform.position.x >= (rightVisibleEdge + _killDistance) && rightInstance)
        {
            rightInstance.GetComponent<TilingBackground>().leftInstance = null;
            Debug.Log("transform.parent.childCount before " + transform.parent.childCount);
            Destroy(gameObject);
            Debug.Log("transform.parent.childCount after " + transform.parent.childCount);
            //ransform.parent.GetComponent<ParallaxBackground>().RefreshChildCount();
        }
        else if (_camera.transform.position.x <= (leftVisibleEdge - _killDistance) && leftInstance)
        {
            leftInstance.GetComponent<TilingBackground>().rightInstance = null;
            Debug.Log("transform.parent.childCount before " + transform.parent.childCount);
            Destroy(gameObject);
            Debug.Log("transform.parent.childCount after " + transform.parent.childCount);
            //transform.parent.GetComponent<ParallaxBackground>().RefreshChildCount();
        }
    }

    private void MakeNewBuddy(int direction)
    {
        
        Vector3 newPosition = new Vector3(
            transform.position.x + transform.localScale.x * _spriteWidth * direction,
            transform.position.y,
            transform.position.z
        );
        Transform newInstance = Instantiate(transform, newPosition, transform.rotation);

        if (reverseScale)
        {
            Vector3 newScale = new Vector3(
                newInstance.localScale.x * -1,
                newInstance.localScale.y,
                newInstance.localScale.z
            );
            newInstance.localScale = newScale;
        }
        newInstance.parent = transform.parent;
        if (direction > 0)
        {
            newInstance.GetComponent<TilingBackground>().leftInstance = this.transform;
            this.rightInstance = newInstance.transform;
        }
        else
        {
            newInstance.GetComponent<TilingBackground>().rightInstance = this.transform;
            this.leftInstance = newInstance.transform;
        }
        //transform.parent.GetComponent<ParallaxBackground>().RefreshChildCount();
    }
}
