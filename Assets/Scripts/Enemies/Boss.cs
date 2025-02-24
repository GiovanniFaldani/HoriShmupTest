using UnityEngine;
using System.Collections;
using UnityEditor;
using UnityEngine.Splines;

public class Boss : MonoBehaviour
{
    [SerializeField] private float timeBetweenShots = 0.1f;
    [SerializeField] private int health = 30;
    [SerializeField] private int contactDamage = 1;
    [SerializeField] Transform shotSocket;
    [SerializeField] Transform playerPos;
    [SerializeField] GameObject sprite;
    
    
    private GameObject projectilePrefab;
    private float shotCooldown;
    [SerializeField] private float shotAngle;
    [SerializeField] private Vector2 shotDirection;
    [SerializeField] private Vector2 target;
    private float angleIncrement;

    void Awake()
    {
        shotAngle = 1.25f * Mathf.PI;
        angleIncrement = 0.05f * Mathf.PI;
        target = new Vector2(Camera.main.transform.position.x, Camera.main.transform.position.y-10);
        shotCooldown = timeBetweenShots;
    }

    // shoot once every timeBetweenShots seconds
    void Update()
    {
        shotCooldown -= Time.deltaTime;
        if (shotCooldown <= 0)
        {
            shotCooldown = timeBetweenShots;
            Shoot();
        }

        CheckHP();
    }
    public int GetDamage() => contactDamage;

    public void SetShotType(GameObject newShotType) => projectilePrefab = newShotType;
    private void CheckHP()
    {
        if (health <= 0)
        {
            sprite.SetActive(false);
            GameLibrary.Instance.GameOver();;
        }
    }

    public void Shoot()
    {
        shotDirection = new Vector2(Mathf.Cos(shotAngle), Mathf.Sin(shotAngle));
        Instantiate(projectilePrefab, shotSocket).GetComponent<Projectile>()
            .SetTarget(target);
        if(shotAngle > 0.75f * Mathf.PI)
        {
            shotAngle -= angleIncrement;
            target = new Vector2(target.x, target.y + 2);
        }
        else
        {
            shotAngle = 1.25f * Mathf.PI;
            target = new Vector2(target.x, target.y - 20);
        }
    }

    private IEnumerator BlinkSprite()
    {
        for (int i = 0; i < 4; i++)
        {
            sprite.SetActive(!sprite.activeSelf);
            yield return new WaitForSeconds(0.2f);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Projectile"))
        {
            if (other.GetComponent<Projectile>().GetShotSource() == ShotSources.Player)
            {
                int damage = other.GetComponent<Projectile>().GetDamage();
                health -= damage;
                StartCoroutine(BlinkSprite());
            }
        }
        if (other.CompareTag("Player"))
        {
            Destroy(this.gameObject);
        }
    }
}
