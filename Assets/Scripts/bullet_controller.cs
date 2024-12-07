using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet_controller : MonoBehaviour
{ 
    private float speed;
    private int damage;
    private Vector3 direction;
    private float maxDistance; // Максимальная дистанция
    private Vector3 startPosition;
    private Vector3 targetPosition; // Конечная позиция пули

    // Инициализация пули
   public void Initialize(float bulletSpeed, int bulletDamage, Vector3 shootDirection, float distance)
    {
        speed = bulletSpeed;
        damage = bulletDamage;
        direction = shootDirection.normalized; // Нормализуем направление, чтобы пуля не ускорялась
        maxDistance = distance;
        startPosition = transform.position;

        // Устанавливаем конечную точку пули, которая ограничена maxDistance
        targetPosition = startPosition + direction * maxDistance;

        // Принудительно делаем компонент Y равным нулю, чтобы пуля не отклонялась по оси Y
        targetPosition.y = startPosition.y;
    }


    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        if (transform.position == targetPosition)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        var damageable = other.GetComponent<IDamageable>();
        if(damageable != null){
            damageable.TakeDamage(damage);
            Destroy(gameObject);
        }
        
    }
}
