using UnityEngine;

namespace New.Interface
{
    public interface IAnimatable
    {
        void UpdateAnimation(Vector3 direction, float speed);
        void SetTrigger(string triggerName);
    }
}