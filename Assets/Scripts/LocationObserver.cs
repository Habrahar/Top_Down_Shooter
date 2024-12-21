using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using New;
using New.Interface;
using Unity.VisualScripting;

public class LocationObserver : MonoBehaviour
{
    private static List<EnemyController> enemies = new List<EnemyController>();
    private static Transform playerTransform;

    [SerializeField] private float checkInterval = 0.1f; // Интервал проверки.
    [SerializeField] private float viseableArea = 35f;   // Радиус видимости.

    private float nextCheckTime;

    private void Update()
    {
        if (playerTransform == null) 
            return;
        
        if (Time.time >= nextCheckTime + checkInterval)
        {
            CheckEnemies();
            nextCheckTime = Time.time;
        }
    }

    public static void RegisterPlayer(Transform player)
    {
        playerTransform = player;
    }

    public static void RegisterEnemy(EnemyController enemy)
    {
        if (!enemies.Contains(enemy))
            enemies.Add(enemy);
    }

    public static void UnregisterEnemy(EnemyController enemy)
    {
        enemies.Remove(enemy);
    }

    private void CheckEnemies()
    {
        
        var enemiesSnapshot = new List<EnemyController>(enemies);

        foreach (var enemy in enemiesSnapshot)
        {
            if (Vector3.Distance(enemy.transform.position, playerTransform.position) <= viseableArea)
            {
                enemy.Activate();
            }
            else
            {
                enemy.Deactivate();
            }
        }

        enemies = enemiesSnapshot;

    }
}
