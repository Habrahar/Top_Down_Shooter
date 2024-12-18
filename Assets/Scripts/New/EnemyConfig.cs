using System;
using UnityEngine;


    [CreateAssetMenu(menuName = "EnemyConfig", fileName = "ScriptableObjects/EnemyConfig")]
    public class EnemyConfig : ScriptableObject
    {
        public float Health;
        public float Speed;
        public int Damage;
        public float AttackRange;
        public float AttackInterval;
        public float AttackDelay;

        public float ChaseRange;
        public GameObject Prefab; // Префаб врага
    }
