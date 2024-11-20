using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Weapon_Controller : MonoBehaviour
{
    public GameObject bulletPrefab;  // Префаб пули
    public Transform firePoint;      // Точка выстрела
    public float bulletSpeed = 10f;  // Скорость пули
    public int bulletDamage = 10;    // Урон пули
    public float fireRate = 0.5f;    // Скорострельность
    public float bulletDistance = 20f; // Максимальная дистанция

    private float nextFireTime = 0f;

    private void OnEnable()
    {
        Player_shooting.OnShoot += HandleShoot;
    }

    private void OnDisable()
    {
        Player_shooting.OnShoot -= HandleShoot;
    }

    public void HandleShoot(Vector3 targetPosition)
{
    if (Time.time < nextFireTime || firePoint == null) return;

    nextFireTime = Time.time + fireRate;

    // Рассчитываем направление от игрока к мыши
    Vector3 direction = targetPosition - firePoint.position;

    // Игнорируем компоненту Y, чтобы пуля летела только в плоскости XZ
    direction.y = 0f;

    // Нормализуем направление, чтобы пуля летела с одинаковой скоростью по всем направлениям
    direction.Normalize();

    // Создаем пулю
    GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.LookRotation(direction));

    // Передаем параметры пули
    bullet_controller bulletScript = bullet.GetComponent<bullet_controller>();
    bulletScript.Initialize(bulletSpeed, bulletDamage, direction, bulletDistance); // Передаем максимальное расстояние
}

}
