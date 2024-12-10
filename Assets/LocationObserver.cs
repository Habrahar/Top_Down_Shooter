using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using New;


public class LocationObserver : MonoBehaviour
{
    private static List<EnemyController> enemies = new List<EnemyController>();
    private static Transform playerTransform;
    private float timer = 0f;

    [SerializeField] private float checkInterval = 0.05f; // Интервал проверки.
    
    private void Start()
    {
        timer = checkInterval;
    }

    void Update()
    {
        if (playerTransform != null)
        {
            if (timer <= 0)
            {
                CheckEnemiesInRange();
                timer = checkInterval;
            }
            else
            {
                timer -= Time.deltaTime;
            }    
        }
        
    }
    public static void RegisterEnemy(EnemyController enemy)
    {
        if (!enemies.Contains(enemy))
            enemies.Add(enemy);
    }

    public static void UnregisterEnemy(EnemyController enemy)
    {
        if (enemies.Contains(enemy))
            enemies.Remove(enemy);
    }
    public static void RegisterPlayer(Transform player)
    {
        playerTransform = player;
    }

    // ReSharper disable Unity.PerformanceAnalysis
    private void CheckEnemiesInRange()
    {
        foreach (var enemy in enemies)
        {
            if (Vector3.Distance(enemy.transform.position, playerTransform.position) >= enemy.AttackRange &&
                Vector3.Distance(enemy.transform.position, playerTransform.position) <= enemy.ChaseRange)
            {
                if (!Physics.Linecast(enemy.transform.position, playerTransform.position))
                {
                    enemy.SetChaseState();
                }
            }
            else
            {
                if (Vector3.Distance(enemy.transform.position, playerTransform.position) <= enemy.AttackRange)
                {
                    if (!Physics.Linecast(enemy.transform.position, playerTransform.position))
                    {
                        enemy.InitializeTarget(playerTransform.GetComponent<IDamageable>());
                        enemy.SetAttackState();
                    }
                }

            }
        }

    }
}

