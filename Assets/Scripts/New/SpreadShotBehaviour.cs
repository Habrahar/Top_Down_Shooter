using UnityEngine;
namespace New
{
    public class SpreadShotBehaviour : IShootingBehaviour
    {
        private int bulletCount;

        public SpreadShotBehaviour(int bulletCount)
        {
            this.bulletCount = bulletCount;
        }

        public void Shoot(Transform firePoint, Vector3 direction, WeaponConfig config)
        {
            float spreadAngle = config.bulletSpread;

            for (int i = 0; i < bulletCount; i++)
            {
                float angle = Random.Range(-spreadAngle, spreadAngle);
                Vector3 spreadDirection = Quaternion.Euler(0, angle, 0) * direction;
                BulletFactory.SpawnBullet(firePoint.position, spreadDirection, config);
            }
        }
    }

}