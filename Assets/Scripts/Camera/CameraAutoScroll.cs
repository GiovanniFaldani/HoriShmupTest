using UnityEngine;

public class CameraAutoScroll : MonoBehaviour
{
    [SerializeField] private float scrollSpeed = 1f;

    void Start()
    {
        
    }

    // Update last on each frame
    void LateUpdate()
    {
        transform.position = new Vector3(transform.position.x + scrollSpeed * Time.deltaTime, transform.position.y, transform.position.z);
    }

    public float GetSpeed()
    {
        return scrollSpeed;
    }
}
