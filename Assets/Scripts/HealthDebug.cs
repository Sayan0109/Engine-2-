using UnityEngine;

public class HealthDebug : MonoBehaviour
{
    private IHealth health;

    void Start()
    {
        health = GetComponent<IHealth>();

        if (health == null)
        {
            Debug.LogError($"{gameObject.name} не имеет компонента, реализующего IHealth!");
            return;
        }

        // У Health-класса есть событие OnDeath — используем его
        Health realHealth = health as Health;
        if (realHealth != null)
        {
            realHealth.OnDeath += () =>
            {
                Debug.Log($"{gameObject.name} → DEAD! ☠️");
            };
        }
    }

    void Update()
    {
        if (health != null)
        {
            Debug.Log($"{gameObject.name} → HP: {health.CurrentHealth}/{health.MaxHealth}");
        }
    }
}
