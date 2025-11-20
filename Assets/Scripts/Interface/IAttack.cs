public interface IAttack
{
    float Damage { get; }
    float FireRate { get; }

    void Attack();   // выполнить атаку (стрельбу)
}