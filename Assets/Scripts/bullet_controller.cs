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
    private GameObject hitEffect;
    private GameObject vanishEffect;

    public void Initialize(float bulletSpeed, int bulletDamage, Vector3 shootDirection, float distance, GameObject hitEffectPrefab)
    {
        speed = bulletSpeed;
        damage = bulletDamage;
        direction = shootDirection.normalized; 
        maxDistance = distance;
        startPosition = transform.position;
        targetPosition = startPosition + direction * maxDistance;
        targetPosition.y = startPosition.y;

        hitEffect = hitEffectPrefab;
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        if (transform.position == targetPosition)
        {
            TriggerEffect(vanishEffect);
            ReturnBulletToPool(); 
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        var damageable = other.GetComponent<IDamageable>();
        if (damageable != null)
        {
            damageable.TakeDamage(damage);
            TriggerEffect(hitEffect, transform.position);
            ReturnBulletToPool();
        }
    }

    private void TriggerEffect(GameObject effectPrefab, Vector3 position = default)
    {
        if (effectPrefab == null) return;

        GameObject effect = Instantiate(effectPrefab, position == default ? transform.position : position, Quaternion.identity);
        Destroy(effect, 2f); // Уничтожаем эффект через 2 секунды
    }

    private void ReturnBulletToPool()
    {
        BulletPool.Instance.ReturnBullet(gameObject);
    }
}