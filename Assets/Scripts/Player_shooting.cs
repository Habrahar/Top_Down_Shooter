using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player_shooting : MonoBehaviour
{
    private LayerMask groundLayer; 
    public static event Action<Vector3> OnShoot;
    private float detectionRadius; // Радиус обнаружения врагов
    private LayerMask enemyLayer; // Слой врагов

    public Transform currentTarget; // Текущий враг, на которого смотрит игрок

    public Transform firePoint;
    public bool CanShoot;

    public void SetParameters(float detection, LayerMask layerEnemy, LayerMask layerGround)
    {
        detectionRadius = detection;
        groundLayer = layerGround;
        enemyLayer = layerEnemy;
    }
    void Update()
    {
        currentTarget = GetClosestEnemy();
        if (currentTarget != null && Vector3.Distance(transform.position, currentTarget.position) <= detectionRadius)
        {
            CanShoot = true;
            RotatePlayerToEnemy(currentTarget.position);
            RotateWeaponToEnemy(currentTarget.position);
        }
        else
        {
            CanShoot = false;
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
        direction.y = 0f;
        firePoint.rotation = Quaternion.LookRotation(direction);
    }
    private void HandleShooting(Vector3 targetPosition)
    {
        //Player_shooting.OnShoot?.Invoke(targetPosition);
        
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
