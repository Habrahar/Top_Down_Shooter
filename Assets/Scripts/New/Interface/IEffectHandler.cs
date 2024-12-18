using UnityEngine;
namespace New.Interface
{
    public interface IEffectHandler
    {
        void PlayImpactEffect(Vector3 position, Quaternion rotation);
        void PlayAttackEffect(Vector3 position, Quaternion rotation);
        void PlayDeathEffect(Vector3 position, Quaternion rotation);
    }
}