using System.Collections;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.XInput;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1.0f;
    [SerializeField] private int health = 5;
    [SerializeField] private float timeBetweenShots = 0.5f;
    [SerializeField] private float invincibilityTime = 1f;
    [SerializeField] private GameObject sprite;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform shotSocket;
    [SerializeField] private TextMeshProUGUI displayHealth;

    private float cameraSpeed;
    private float xMax = 8.4f;
    private float xMin = -7.9f;
    private float yMax = 4.5f;
    private float yMin = -4.5f;
    private float cooldown = 0f;
    private float invincibilityTimer = 0f;
    private Vector2 shotDirection = new Vector2(1,0);

    private void Awake()
    {
        cameraSpeed = Camera.main.GetComponent<CameraAutoScroll>().GetSpeed();
    }

    void Start()
    {
        
    }


    private void Update()
    {
        CheckTimers();
        HandleInpus();
        CheckHP();
    }

    private void CheckTimers()
    {
        cooldown -= Time.deltaTime;
        invincibilityTimer -= Time.deltaTime;
        if(invincibilityTimer > 0)
        {
            // blink sprite
            StartCoroutine(BlinkSprite());
        }
        else
        {
            // stop blinking sprite
            StopCoroutine(BlinkSprite());
            sprite.SetActive(true);
        }
    }

    private IEnumerator BlinkSprite()
    {
        sprite.SetActive(!sprite.activeSelf);
        yield return new WaitForSeconds(0.2f);
    }

    private void HandleInpus()
    {
        float xInput = Input.GetAxisRaw("Horizontal");
        float yInput = Input.GetAxisRaw("Vertical");
        ApplyMovement(xInput, yInput);
        if (Input.GetAxisRaw("Shoot") > 0 && cooldown <= 0) // this should enable autofire by holding down the button
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
        Instantiate(projectilePrefab, shotSocket).GetComponent<Projectile>()
            .SetDirection(shotDirection);
    }

    public void CheckHP()
    {
        displayHealth.text = health.ToString();
        if (health <= 0)
        {
            GameLibrary.Instance.GameOver();
        }
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (invincibilityTimer <=0 ) {
            if (other.CompareTag("Enemy"))
            {
                int damage = other.GetComponent<Enemy>().GetDamage();
                health -= damage;
                invincibilityTimer = invincibilityTime;
            }
            if (other.CompareTag("Projectile"))
            {
                if (other.GetComponent<Projectile>().GetShotSource() == ShotSources.Enemy)
                {
                    int damage = other.GetComponent<Projectile>().GetDamage();
                    health -= damage;
                    invincibilityTimer = invincibilityTime;
                }
            }
        }
    }


}
