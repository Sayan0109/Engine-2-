using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretShooting : MonoBehaviour
{
    [Header("Shooting Settings")]
    public Transform[] firePoints;        // точки, откуда вылетают снар€ды
    public GameObject projectilePrefab;   // префаб снар€да
    public float fireRate = 0.2f;         // скорость стрельбы (чем меньше, тем быстрее стрел€ет)

    [Header("Control")]
    public Joystick aimJoystick;          // тот же правый джойстик, что и дл€ наведени€

    private float nextFireTime = 0f;

    void Update()
    {
        HandleShooting();
    }

    void HandleShooting()
    {
        // если джойстик не активен Ч не стрел€ем
        if (Mathf.Abs(aimJoystick.Horizontal) < 0.1f && Mathf.Abs(aimJoystick.Vertical) < 0.1f)
            return;

        // если прошло достаточно времени дл€ следующего выстрела
        if (Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
    }

    void Shoot()
    {
        foreach (Transform firePoint in firePoints)
        {
            Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        }
    }
}
