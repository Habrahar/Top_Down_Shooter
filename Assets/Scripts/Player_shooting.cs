using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player_shooting : MonoBehaviour
{
    [SerializeField] private LayerMask groundLayer; 
    public static event Action<Vector3> OnShoot;
    [SerializeField] private float detectionRadius = 10f; // Радиус обнаружения врагов
    [SerializeField] private LayerMask enemyLayer; // Слой врагов

    public Transform currentTarget; // Текущий враг, на которого смотрит игрок

    public Transform firePoint; 
    

    void Update()
    {
        // Находим ближайшего врага
        currentTarget = GetClosestEnemy();
        if (currentTarget != null && Vector3.Distance(transform.position, currentTarget.position) <= detectionRadius)
        {
            RotatePlayerToEnemy(currentTarget.position);
            RotateWeaponToEnemy(currentTarget.position);
            HandleShooting(currentTarget.position);
        }
    }


    private void RotatePlayerToEnemy(Vector3 targetPosition)
    {
        Vector3 direction = (targetPosition - transform.position).normalized;
        direction = new Vector3(direction.x, 0f, direction.z);

        if (direction != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(direction);
        }
    }

    private void RotateWeaponToEnemy(Vector3 targetPosition)
    {
        if (firePoint == null) return;

        Vector3 direction = (targetPosition - firePoint.position).normalized;
        direction.y = 0f; // Убираем наклон по оси Y

        firePoint.rotation = Quaternion.LookRotation(direction);
    }
    private void HandleShooting(Vector3 targetPosition)
    {
        Debug.Log("Shooting at target: " + targetPosition);
        Player_shooting.OnShoot?.Invoke(targetPosition);
    }




    private Transform GetClosestEnemy()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, detectionRadius, enemyLayer);
        Transform closestEnemy = null;
        float closestDistance = Mathf.Infinity;

        foreach (var hitCollider in hitColliders)
        {
            float distance = Vector3.Distance(transform.position, hitCollider.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestEnemy = hitCollider.transform;
            }
        }
        return closestEnemy;
    }


}
