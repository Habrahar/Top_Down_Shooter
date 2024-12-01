using UnityEngine;
using System;


    [CreateAssetMenu(fileName = "TurretConfig", menuName = "ScriptableObjects/turretConfig")]
    public class TurretConfig : ScriptableObject
    {
        public GameObject visualPrefab;
        public GameObject bulletPrefab;
        
        [Header("Fire Settings")]
        [SerializeField]
        public float fireRate;
        public float bulletSpeed;
        public float bulletDamage;
        public float attackDistance;
        
        [Header("Ammo Settings")]
        public int maxTotalAmmo = 0; // Общее количество патронов
    }