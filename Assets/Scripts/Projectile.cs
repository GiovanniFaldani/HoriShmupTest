using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float speed = 2;
    [SerializeField] int damage = 1;
    [SerializeField] float lifeTime = 10f;
    [SerializeField] ShotSources shotSource;
    [SerializeField] ShotTypes shotType;

    private float timeAlive = 0f;
    private Vector2 target;
    private Vector2 direction;


    private void Start()
    {
        if (shotType == ShotTypes.Target) 
        {
            direction = (target - (Vector2)this.transform.position);
        }
        this.transform.SetParent(Camera.main.transform);
        this.transform.rotation = Quaternion.Euler(direction);
    }


    void Update()
    {
        Move();
        CheckLifetime();
    }

    public ShotSources GetShotSource() => shotSource;

    public ShotTypes GetShotType() => shotType;

    public Projectile SetTarget(Vector2 newTarget)
    {
        this.target = newTarget;
        return this;
    }

    public Projectile SetDirection(Vector2 newDirection) 
    { 
        this.direction = newDirection;
        return this;
    }

    public int GetDamage() => damage;

    private void CheckLifetime()
    {
        timeAlive += Time.deltaTime;
        if(timeAlive > lifeTime)
        {
            Destroy(this.gameObject);
        }
    }

    private void Move()
    {
        this.transform.position += (Vector3)direction.normalized * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") && shotSource != ShotSources.Player)
            Destroy(this.gameObject);
        if(collision.CompareTag("Enemy") && shotSource != ShotSources.Enemy)
            Destroy(this.gameObject);
    }
}
