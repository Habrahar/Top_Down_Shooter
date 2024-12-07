using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AggroDistanceCheck : MonoBehaviour
{
    public GameObject PlayerTarget {get; set;}
    private EnemyController _enemy;

    private void Awake(){
        PlayerTarget = GameObject.FindGameObjectWithTag("Player");

        _enemy = GetComponentInParent<EnemyController>();
    }


    private void OnTriggerEnter(Collider other) {
        if(other.gameObject == PlayerTarget){
            _enemy.SetAggroStatus(true);
        }
    }

    
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject == PlayerTarget){
            _enemy.SetAggroStatus(false);
        }
    }
}