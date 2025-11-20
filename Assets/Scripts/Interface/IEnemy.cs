public interface IEnemy
{
    float Damage { get; }
    float AttackRate { get; }
    float DetectionRange { get; }
    float AttackRange { get; }

    void AttackPlayer(IHealth playerHealth);
}
