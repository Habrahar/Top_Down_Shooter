using UnityEngine;
namespace New
{
    public abstract class MovableEntity : MonoBehaviour
    {
        protected Vector3 direction;
        private float moveSpeed;
        private float collisionCheckRadius;
        private LayerMask obstacleLayerMask;

        public Vector3 Direction => direction; // Свойство для доступа к направлению

        protected void Initialize(float speed, float checkRadius, LayerMask layerMask)
        {
            moveSpeed = speed;
            collisionCheckRadius = checkRadius;
            obstacleLayerMask = layerMask;
        }

        protected void Move(Vector3 moveDirection)
        {
            if (moveDirection.magnitude >= 0.1f)
            {
                if (!CheckCollision(moveDirection))
                {
                    transform.Translate(moveDirection * (moveSpeed * Time.deltaTime), Space.World);
                }
            }
        }

        protected abstract void HandleRotation(Vector3 moveDirection);

        private bool CheckCollision(Vector3 moveDirection)
        {
            Vector3 targetPosition = transform.position + moveDirection * collisionCheckRadius;
            Collider[] hitColliders = Physics.OverlapSphere(targetPosition, collisionCheckRadius, obstacleLayerMask);
            return hitColliders.Length > 0;
        }
    }
}