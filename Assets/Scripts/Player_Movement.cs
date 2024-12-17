using System.Collections;
using System.Collections.Generic;
using New.Interface;
using UnityEngine;


public class PlayerMovement : MonoBehaviour, ISpawnable
{
    private float moveSpeed;
    private Animator animator; // Ссылка на Animator
    public Player_shooting shooting;
    private Vector3 direction;
    private bool setParam = false;

    public Vector3 GetDirection()
    {
        return direction;
    } 
    public void SetParam(float movespeed, Animator anim)
    {
        animator = anim;
        moveSpeed = movespeed;
        setParam = true;
    }

    void Update()
    {
        if (setParam == true)
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

        
            direction = new Vector3(horizontal, 0f, vertical).normalized;

        
            if (direction.magnitude >= 0.1f)
            {
                transform.Translate(direction * (moveSpeed * Time.deltaTime), Space.World);
            }
        
            if (shooting.currentTarget == null && direction.magnitude > 0f)
            {
                RotatePlayer(direction);
            }
        }
        
    }

    private void RotatePlayer(Vector3 direction)
    {
        // Поворот игрока в направлении движения
        Quaternion toRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, Time.deltaTime * 10f);
    }

}

