using UnityEngine;
namespace New
{
    public interface IShootingBehaviour
    {
        void Shoot(Transform firePoint, Vector3 direction, WeaponConfig config);
    }
}