using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : MonoBehaviour, IAttack
{
    [Header("Turret Settings")]
    public Transform[] turrets;
    public float rotationSpeed = 5f;
    public Vector3 rotationOffset;

    [Header("Shooting Settings")]
    public Transform[] firePoints;
    public GameObject projectilePrefab;
    public float fireRate = 0.2f;
    public float damage = 20f;         // Добавили урон!
    public float projectileLifetime = 3f;

    [Header("Control")]
    public Joystick aimJoystick;

    private float nextFireTime = 0f;

    // ← Эти свойства требуются интерфейсом IAttack
    public float Damage => damage;
    public float FireRate => fireRate;

    void Start()
    {
        if (aimJoystick == null)
        {
            GameObject joystickObj = GameObject.Find("AimJoystick");
            if (joystickObj != null)
            {
                aimJoystick = joystickObj.GetComponent<Joystick>();
                Debug.Log("🎯 AimJoystick найден автоматически.");
            }
            else
            {
                Debug.LogWarning("⚠️ AimJoystick не найден! Укажи его вручную.");
            }
        }
    }

    void Update()
    {
        if (aimJoystick == null) return;

        float horizontal = aimJoystick.Horizontal;
        float vertical = aimJoystick.Vertical;

        if (Mathf.Abs(horizontal) < 0.1f && Mathf.Abs(vertical) < 0.1f)
            return;

        // ---- Вращение башен ----
        Vector3 aimDirection = new Vector3(horizontal, 0f, vertical).normalized;
        Quaternion targetRotation = Quaternion.LookRotation(aimDirection, Vector3.up);
        targetRotation *= Quaternion.Euler(rotationOffset);

        foreach (Transform turret in turrets)
        {
            turret.rotation = Quaternion.Slerp(
                turret.rotation,
                targetRotation,
                rotationSpeed * Time.deltaTime
            );
        }

        // ---- Стрельба ----
        if (Time.time >= nextFireTime)
        {
            Attack();  // ← теперь вызываем интерфейсный метод
            nextFireTime = Time.time + fireRate;
        }
    }

    // Реализация Attack() из интерфейса
    public void Attack()
    {
        Shoot();
    }
    void Shoot()
    {
        foreach (Transform firePoint in firePoints)
        {
            GameObject projectileGO = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);

            if (projectileGO.TryGetComponent(out IProjectile proj))
            {
                (proj as Projectile).SetDamage(damage);
            }

            // всё, проектил сам полетит и убьётся по Lifetime
        }
    }

    /*void Shoot()
    {
        foreach (Transform firePoint in firePoints)
        {
            if (projectilePrefab == null) continue;

            GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);

            // Если снаряд сам не умеет двигаться — добавим простое движение
            if (projectile.GetComponent<Rigidbody>() == null && projectile.GetComponent<Projectile>() == null)
            {
                projectile.AddComponent<Projectile>();
            }

            Destroy(projectile, projectileLifetime);
        }
    }*/
}
