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
    public float moveSpeed;
    public static event Action playerDead; // Событие обновления патронов

    public float detectionRadius;
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
    [SerializeField] public HealthController hpBar;

    public void Start()
    {
        RestartPlayer();
        
    }

    public void EquipWeapon(WeaponConfig config)
    {
        currentWeapon = config;
        weapon.InitializeWeapon(currentWeapon);
        animationController = gameObject.AddComponent<AnimationController>();
        animationController.Initialize(_animator);
        movement.SetParameters(moveSpeed, null, checkDistance, Collision);
        shooting.SetParameters(detectionRadius, enemyLayer);
        weapon.SetPlayerController(this);
        hpBar.maxBullets = currentWeapon.magazineSize;
    }

    public void Update()
    {
        animationController.UpdateAnimation(movement.Direction, moveSpeed);
    }

    public void TakeDamage(int damage)
    {
        CurrentHealth -= damage;
        hpBar.UpdateHealthBar(damage);
        if (CurrentHealth <= 0)
        {
            Die();
        }
    }

    public void RestartPlayer()
    {
        
        CurrentHealth = MaxHealth;
        hpBar.SetHealth(MaxHealth);
        LocationObserver.RegisterPlayer(transform);
        EquipWeapon(currentWeapon);
        hpBar.UpdateBullets(currentWeapon.magazineSize);
        
    }

    public void Die()
    {
        hpBar.destroyHP();
        gameObject.SetActive(false);
        playerDead?.Invoke();
        
    }
}
