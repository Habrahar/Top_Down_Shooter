using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player_Movement : MonoBehaviour
{
   public float moveSpeed = 5f;

    void Update()
    {
        // Получаем ввод по оси X и Z
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Направление в мировом пространстве
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        // Двигаем игрока в мировых координатах
        if (direction.magnitude >= 0.1f)
        {
            transform.Translate(direction * moveSpeed * Time.deltaTime, Space.World);
        }
    }
}
