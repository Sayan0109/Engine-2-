using UnityEngine;

public class EnemyAI : MonoBehaviour, IEnemy
{
    [Header("State")]
    [SerializeField] private AiState aiState = AiState.Idle;

    [Header("References")]
    public Transform player;
    private IHealth playerHealth;

    [Header("Stats")]
    [SerializeField] private float speed = 5f;
    [SerializeField] private float rotationSpeed = 5f;

    [Header("Detection & Attack")]
    [SerializeField] private float detectionRange = 20f;
    [SerializeField] private float attackRange = 3f;
    [SerializeField] private float damage = 10f;
    [SerializeField] private float attackRate = 1f;

    private float nextAttackTime = 0f;
    private Rigidbody rb;
    private IHealth myHealth;

    // интерфейс
    public float Damage => damage;
    public float AttackRate => attackRate;
    public float DetectionRange => detectionRange;
    public float AttackRange => attackRange;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        myHealth = GetComponent<IHealth>();

        if (player == null)
        {
            GameObject go = GameObject.FindWithTag("Player");
            if (go != null)
            {
                player = go.transform;
                playerHealth = player.GetComponent<IHealth>();
            }
        }
    }

    void FixedUpdate()
    {
        if (myHealth == null || myHealth.CurrentHealth <= 0) return;
        if (player == null || playerHealth == null) return;

        float distance = Vector3.Distance(transform.position, player.position);

        // --- FSM управление состояниями ---
        switch (aiState)
        {
            case AiState.Idle:
                HandleIdle(distance);
                break;

            case AiState.MoveToTarget:
                HandleMoveToTarget(distance);
                break;

            case AiState.Attack:
                HandleAttack(distance);
                break;
        }
    }

    // ----------------- STATE HANDLERS ------------------

    private void HandleIdle(float distance)
    {
        if (distance <= detectionRange)
        {
            aiState = AiState.MoveToTarget;
        }
    }

    private void HandleMoveToTarget(float distance)
    {
        if (distance > detectionRange)
        {
            aiState = AiState.Idle;
            return;
        }

        if (distance <= attackRange)
        {
            aiState = AiState.Attack;
            return;
        }

        MoveTowardsPlayer();
    }

    private void HandleAttack(float distance)
    {
        if (distance > attackRange)
        {
            aiState = AiState.MoveToTarget;
            return;
        }

        RotateTowardsPlayer();

        // атака по таймеру
        if (Time.time >= nextAttackTime)
        {
            AttackPlayer(playerHealth);
            nextAttackTime = Time.time + 1f / AttackRate;
        }
    }

    // ----------------- ACTIONS ------------------

    private void MoveTowardsPlayer()
    {
        Vector3 dir = (player.position - transform.position).normalized;

        RotateTowardsPlayer();

        rb.MovePosition(transform.position + dir * speed * Time.fixedDeltaTime);
    }

    private void RotateTowardsPlayer()
    {
        Vector3 dir = (player.position - transform.position).normalized;
        Quaternion targetRotation = Quaternion.LookRotation(dir, Vector3.up);
        rb.rotation = Quaternion.Slerp(rb.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
    }

    public void AttackPlayer(IHealth playerHealth)
    {
        if (playerHealth.CurrentHealth <= 0) return;

        playerHealth.TakeDamage(Damage);
        Debug.Log($"Enemy атакует игрока! Нанесено {Damage} урона.");
    }
}