using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.Splines;
using UnityEngine.U2D;
using UnityEngine.UIElements;
using System.Collections;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float timeBetweenShots = 3f;
    [SerializeField] private int health = 1;
    [SerializeField] private int contactDamage = 1; 
    [SerializeField] Transform shotSocket;
    [SerializeField] Transform playerPos;
    [SerializeField] GameObject sprite;


    private float shotCooldown;
    private GameObject projectilePrefab;

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

    public void SetShotType(GameObject newShotType) => projectilePrefab = newShotType;
    private void CheckHP()
    {
        if(health <= 0)
        {
            Destroy(this.gameObject);
            GameLibrary.Instance.EnemyKilled();
        }
    }

    public void Shoot()
    {
        Instantiate(projectilePrefab, shotSocket).GetComponent<Projectile>()
            .SetTarget(GameLibrary.Instance.playerPos.position);
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
