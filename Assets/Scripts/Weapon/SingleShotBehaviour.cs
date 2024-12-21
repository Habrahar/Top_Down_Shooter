using UnityEngine;
namespace New
{
    public class SingleShotBehaviour : IShootingBehaviour
    {
        public void Shoot(Transform firePoint, Vector3 direction, WeaponConfig config)
        {
            BulletFactory.SpawnBullet(firePoint.position, direction, config);
        }
    }
}