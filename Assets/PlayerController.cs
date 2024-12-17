using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, IDamageable
{
    public IDamageable Target { get; set; }
    public float MaxHealth { get; set; }
    public float moveSpeed = 5f;
    [SerializeField] public float detectionRadius = 10f; // Радиус обнаружения врагов
    [SerializeField] private LayerMask enemyLayer; // Слой врагов
    [SerializeField] private LayerMask groundLayer;
    public float CurrentHealth { get; set; }
    
    [SerializeField] private Player_shooting shooting;
    [SerializeField] private PlayerMovement movement;
    public Animator animator; // Ссылка на Animator
    public void Start()
    {
        MaxHealth = 100;
        CurrentHealth = MaxHealth;
        LocationObserver.RegisterPlayer(transform);
        movement.SetParam(moveSpeed, animator);
        shooting.SetParameters(detectionRadius, enemyLayer, groundLayer);
    }

    public void Update()
    {
        HandleAnimations(movement.GetDirection());
    }

    public void TakeDamage(int damage)
    {
        CurrentHealth -= damage;
        if(CurrentHealth <= 0)
        {
            Die();
        }
    }
    private void HandleAnimations(Vector3 direction)
    {
        float moveSpeedValue = direction.magnitude * moveSpeed;
        
        animator.SetFloat("MoveSpeed", moveSpeedValue);

        if (direction.magnitude > 0)
        {
            float angle = Vector3.Angle(transform.forward, direction);
            
            animator.SetBool("IsMovingBackward", angle > 90f);
        }
        else
        {
            animator.SetBool("IsMovingBackward", false);
        }
    }

    public void Die()
    {
        Destroy(gameObject);
    }
    

    

   
}
