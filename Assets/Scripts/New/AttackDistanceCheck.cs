using System.Collections;
using System.Collections.Generic;
using New;
using UnityEngine;

public class AttackDistanceCheck : MonoBehaviour
{
    public GameObject PlayerTarget {get; set;}
    private EnemyController _enemy;

    private void Awake(){
        PlayerTarget = GameObject.FindGameObjectWithTag("Player");

        _enemy = GetComponentInParent<EnemyController>();
    }


    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player"))
        {
            _enemy.SetAttackState();
        }
    }

    
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _enemy.SetIdleState();
        }
    }
}
