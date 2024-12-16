using System.Collections;
using System.Collections.Generic;
using New.Interface;
using UnityEngine;


public class PlayerMovement : MonoBehaviour, ISpawnable
{
    public float moveSpeed = 5f;
    public Animator animator; // Ссылка на Animator
    public Player_shooting shooting;
    

    private void Start()
    {
    }

    void Update()
    {
        // Получаем ввод по осям X и Z
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Направление в мировом пространстве
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        // Двигаем игрока в мировых координатах
        if (direction.magnitude >= 0.1f)
        {
            transform.Translate(direction * (moveSpeed * Time.deltaTime), Space.World);
        }

        // Если врага нет в радиусе, поворачиваем игрока в направлении его движения
        if (shooting.currentTarget == null && direction.magnitude > 0f)
        {
            RotatePlayer(direction);
        }

        // Управляем анимациями
        HandleAnimations(direction);
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

    // Поворот игрока в сторону движения
    private void RotatePlayer(Vector3 direction)
    {
        // Поворот игрока в направлении движения
        Quaternion toRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, Time.deltaTime * 10f);
    }

}

