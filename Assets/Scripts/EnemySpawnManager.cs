using UnityEngine;
using System.Collections.Generic;

public class EnemySpawnManager : MonoBehaviour
{
    [Header("References")]
    public Transform player;               // игрок
    public GameObject enemyPrefab;         // префаб врага

    [Header("Spawn Settings")]
    public float spawnRadius = 20f;        // радиус вокруг игрока
    public float minSpawnDistance = 5f;    // чтобы не появлялся прямо под ногами
    public float spawnInterval = 15f;      // время между спавнами (секунды)
    public int maxEnemies = 10;            // максимальное количество врагов

    private float nextSpawnTime;
    private List<GameObject> aliveEnemies = new List<GameObject>();

    void Start()
    {
        if (player == null)
        {
            GameObject go = GameObject.FindWithTag("Player");
            if (go != null) player = go.transform;
        }

        nextSpawnTime = Time.time + spawnInterval;
    }

    void Update()
    {
        // удаляем мёртвых из списка
        aliveEnemies.RemoveAll(e => e == null);

        if (Time.time >= nextSpawnTime)
        {
            TrySpawnEnemy();
            nextSpawnTime = Time.time + spawnInterval;
        }
    }

    private void TrySpawnEnemy()
    {
        if (aliveEnemies.Count >= maxEnemies) return;

        Vector3 spawnPos = GetRandomSpawnPoint();

        GameObject enemy = Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
        aliveEnemies.Add(enemy);
    }

    private Vector3 GetRandomSpawnPoint()
    {
        Vector3 point;

        do
        {
            // случайная точка вокруг игрока в горизонтальной плоскости
            Vector2 circle = Random.insideUnitCircle.normalized * spawnRadius;
            point = new Vector3(circle.x, 0, circle.y) + player.position;

        } while (Vector3.Distance(point, player.position) < minSpawnDistance);

        return point;
    }
}
