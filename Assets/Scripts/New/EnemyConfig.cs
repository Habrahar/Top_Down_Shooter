using System;
using UnityEngine;


    [CreateAssetMenu(menuName = "EnemyConfig", fileName = "ScriptableObjects/EnemyConfig")]
    public class EnemyConfig : ScriptableObject
    {
        public float Health;
        public float Speed;
        public int Damage;
        public GameObject Prefab; // Префаб врага
    }
