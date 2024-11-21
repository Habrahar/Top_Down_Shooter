using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player_shooting : MonoBehaviour
{
  
    public static event Action<Vector3> OnShoot;

    void Update()
    {
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit))
        {
            RotatePlayer(hit.point); // Поворачиваем игрока

            if (Input.GetMouseButton(0)) // Если нажата левая кнопка мыши
            {
                OnShoot?.Invoke(hit.point); // Передаем точку, на которую наведен курсор
            }
        }
    }

    private void RotatePlayer(Vector3 targetPosition)
    {
        Vector3 direction = (targetPosition - transform.position).normalized;
        direction.y = 0f; // Убираем наклон по оси Y
        if (direction != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(direction);
        }
    }
}
