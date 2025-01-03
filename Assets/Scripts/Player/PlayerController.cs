using System;
using System.Collections;
using System.Collections.Generic;
using New;
using New.Interface;
using UI;
using UnityEngine;

public class PlayerController : MonoBehaviour, IDamageable
{
    public IDamageable Target { get; set; }
    public float MaxHealth { get; set; }
    public float moveSpeed = 5f;
    public static event Action playerDead; // Событие обновления патронов

    [SerializeField] public float detectionRadius = 10f;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask Collision;
    [SerializeField] private float checkDistance = 4f;

    public float CurrentHealth { get; set; }

    [SerializeField] private Player_shooting shooting;
    [SerializeField] private Weapon_Controller weapon;
    [SerializeField] private PlayerMovement movement;
    [SerializeField] private Animator _animator;

    [SerializeField] private WeaponConfig currentWeapon;
    private AnimationController animationController;


    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {
        ;
    }

    public void Start()
    {
        MaxHealth = 100;
        CurrentHealth = MaxHealth;
        LocationObserver.RegisterPlayer(transform);
        EquipWeapon(currentWeapon);
        
    }

    public void EquipWeapon(WeaponConfig config)
    {
        currentWeapon = config;
        weapon.InitializeWeapon(currentWeapon);
        animationController = gameObject.AddComponent<AnimationController>();
        animationController.Initialize(_animator);
        movement.SetParameters(moveSpeed, null, checkDistance, Collision);
        shooting.SetParameters(detectionRadius, enemyLayer);
    }

    public void Update()
    {
        animationController.UpdateAnimation(movement.Direction, moveSpeed);
    }

    public void TakeDamage(int damage)
    {
        CurrentHealth -= damage;
        if (CurrentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        
        gameObject.SetActive(false);
        playerDead?.Invoke();
        
    }
}
