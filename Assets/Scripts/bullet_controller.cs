using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet_controller : MonoBehaviour
{ 
    private float speed;
    private int damage;
    private Vector3 direction;
    private float maxDistance;
    private Vector3 startPosition;
    private Vector3 targetPosition; 
    
    public void Initialize(float bulletSpeed, int bulletDamage, Vector3 shootDirection, float distance)
    {
        speed = bulletSpeed;
        damage = bulletDamage;
        direction = shootDirection.normalized; 
        maxDistance = distance;
        startPosition = transform.position;
        targetPosition = startPosition + direction * maxDistance;
        targetPosition.y = startPosition.y;
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        if (transform.position == targetPosition)
        {
            ReturnBulletToPool(); 
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        var damageable = other.GetComponent<IDamageable>();
        if(damageable != null){
            damageable.TakeDamage(damage);
            ReturnBulletToPool();
        }
    }

    // Возвращаем пулю в пул
    private void ReturnBulletToPool()
    {
        BulletPool.Instance.ReturnBullet(gameObject);
    }
    
}