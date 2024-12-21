using System.Collections;
using System.Collections.Generic;
using New;
using New.Interface;
using UnityEngine;

public class PlayerMovement : MovableEntity, ISpawnable
{
    private Animator animator;
    public Player_shooting shooting;
    
    public void SetParameters(float speed, Animator anim, float checkRadius, LayerMask layerMask)
    {
        Initialize(speed, checkRadius, layerMask);
        animator = anim;
    }

    private void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        direction = new Vector3(horizontal, 0f, vertical).normalized;

        Move(direction);

        if (shooting.currentTarget == null && direction.magnitude > 0f)
        {
            HandleRotation(direction);
        }
    }

    protected override void HandleRotation(Vector3 moveDirection)
    {
        Quaternion toRotation = Quaternion.LookRotation(moveDirection);
        transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, Time.deltaTime * 10f);
    }
}