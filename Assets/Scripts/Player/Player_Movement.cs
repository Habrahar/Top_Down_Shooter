using System.Collections;
using System.Collections.Generic;
using New;
using New.Interface;
using UnityEngine;

public class PlayerMovement : MovableEntity, ISpawnable
{
    private Animator animator;
    public Player_shooting shooting;
    private Vector3 touchStartPosition;
    private bool isTouching = false;

    public void SetParameters(float speed, Animator anim, float checkRadius, LayerMask layerMask)
    {
        Initialize(speed, checkRadius, layerMask);
        animator = anim;
    }

    private void Update()
    {
        HandleWASDMovement();
        HandleTouchMovement();
    }

    private void HandleWASDMovement()
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

    private void HandleTouchMovement()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    touchStartPosition = touch.position;
                    isTouching = true;
                    break;

                case TouchPhase.Moved:
                case TouchPhase.Stationary:
                    if (isTouching)
                    {
                        Vector2 touchDelta = touch.position - new Vector2(touchStartPosition.x, touchStartPosition.y);
                        direction = new Vector3(touchDelta.x, 0f, touchDelta.y).normalized;
                        Move(direction);

                        if (shooting.currentTarget == null && direction.magnitude > 0f)
                        {
                            HandleRotation(direction);
                        }
                    }
                    break;

                case TouchPhase.Ended:
                case TouchPhase.Canceled:
                    isTouching = false;
                    direction = Vector3.zero;
                    break;
            }
        }
    }

    protected override void HandleRotation(Vector3 moveDirection)
    {
        Quaternion toRotation = Quaternion.LookRotation(moveDirection);
        transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, Time.deltaTime * 10f);
    }
}