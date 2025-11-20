public interface IHealth
{
    float MaxHealth { get; }
    float CurrentHealth { get; }

    void TakeDamage(float amount);   // получить урон
    void Heal(float amount);         // восстановить здоровье
    void Die();                      // смерть
}
