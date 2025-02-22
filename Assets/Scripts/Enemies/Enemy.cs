using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.Splines;
using UnityEngine.UIElements;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float timeBetweenShots = 3f;
    [SerializeField] private int health = 1;
    [SerializeField] private int contactDamage = 1;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] Transform shotSocket;
    [SerializeField] Transform playerPos;


    private float shotCooldown;

    void Awake()
    {
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

    private void CheckHP()
    {
        if(health <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    public void Shoot()
    {
        Instantiate(projectilePrefab, shotSocket).GetComponent<Projectile>()
            .SetTarget(GameLibrary.Instance.playerPos.position);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Projectile"))
        {
            if (other.GetComponent<Projectile>().GetShotSource() == ShotSources.Player)
            {
                int damage = other.GetComponent<Projectile>().GetDamage();
                health -= damage;
            }
        }
        if (other.CompareTag("Player"))
        {
            Destroy(this.gameObject);
        }
    }

}
