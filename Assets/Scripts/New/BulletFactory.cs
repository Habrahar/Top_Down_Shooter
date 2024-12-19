using UnityEngine;

namespace New
{
    public class BulletFactory
    {
        
        public static void SpawnBullet(Vector3 position, Vector3 direction, WeaponConfig config)
        {
            GameObject bullet = BulletPool.Instance.GetBullet(position, Quaternion.LookRotation(direction));

            bullet_controller bulletScript = bullet.GetComponent<bullet_controller>();
            bulletScript.Initialize(
                config.bulletSpeed, 
                config.bulletDamage, 
                direction, 
                config.bulletDistance, 
                config.hitEffect, 
                config.vanishEffect
            );
        }

        
    }
}