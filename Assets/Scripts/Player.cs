using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.XInput;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1.0f;
    [SerializeField] private uint health = 3;

    private float cameraSpeed;
    private float xMax = 8.4f;
    private float xMin = -7.9f;
    private float yMax = 4.5f;
    private float yMin = -4.5f;
    private float cooldown = 0f;
    private float timeBetweenShots = 0.1f;

    private void Awake()
    {
        cameraSpeed = Camera.main.GetComponent<CameraAutoScroll>().GetSpeed();
    }

    void Start()
    {
        
    }


    private void Update()
    {
        HandleInpus();
    }

    private void HandleInpus()
    {
        float xInput = Input.GetAxisRaw("Horizontal");
        float yInput = Input.GetAxisRaw("Vertical");
        ApplyMovement(xInput, yInput);
        if (Input.GetKey("Shoot") && cooldown <= 0)
        {
            cooldown = timeBetweenShots;
            Shoot();
        }
    }

    public void ApplyMovement(float xInput, float yInput)
    {
        var direction = new Vector2(xInput, yInput).normalized;
        direction *= moveSpeed * Time.deltaTime; // apply speed
        float xValidPosition = Mathf.Clamp(transform.position.x + direction.x, xMin, xMax);
        float yValidPosition = Mathf.Clamp(transform.position.y + direction.y, yMin, yMax);
        transform.position = new Vector3(xValidPosition, yValidPosition, transform.position.z);
    }

    public void Shoot()
    {

    }

    void LateUpdate()
    {
        FollowCamera();
    }

    // assures the player always moves with the camera
    private void FollowCamera()
    {
        transform.position = new Vector3(transform.position.x + cameraSpeed * Time.deltaTime, transform.position.y, transform.position.z);
        xMax += cameraSpeed * Time.deltaTime;
        xMin += cameraSpeed * Time.deltaTime;
    }
}
