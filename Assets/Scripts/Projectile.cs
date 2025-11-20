using UnityEngine;

public class Projectile : MonoBehaviour, IProjectile
{
    public float speed = 20f;
    public float lifetime = 3f;

    [Header("Damage")]
    [SerializeField] private float damage = 20f;
    public float Damage => damage;  // реализация IProjectile

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    // движение
    void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        // наносим урон только объектам с IHealth
        if (other.TryGetComponent(out IHealth health))
        {
            health.TakeDamage(Damage);
        }

        Destroy(gameObject);
    }

    // возможность установить damage извне (от TurretController)
    public void SetDamage(float value)
    {
        damage = value;
    }
}