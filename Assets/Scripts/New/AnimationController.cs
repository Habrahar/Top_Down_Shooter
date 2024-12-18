using New.Interface;
using UnityEngine;

namespace New
{
    public class AnimationController : MonoBehaviour, IAnimatable
    {
        private Animator animator;

        public void Initialize(Animator anim)
        {
            animator = anim;
        }

        public void UpdateAnimation(Vector3 direction, float speed)
        {
            animator.SetFloat("MoveSpeed", direction.magnitude * speed);

            if (direction.magnitude > 0)
            {
                float angle = Vector3.Angle(transform.forward, direction);
                animator.SetBool("IsMovingBackward", angle > 90f);
            }
            else
            {
                animator.SetBool("IsMovingBackward", false);
            }
        }

        public void SetTrigger(string triggerName)
        {
            animator.SetTrigger(triggerName);
        }
    }
}