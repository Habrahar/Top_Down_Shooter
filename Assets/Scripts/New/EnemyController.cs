using New.Interface;
using UnityEngine;

namespace New
{
    public class EnemyController : MovableEntity, IDamageable, IMovable, ITriggerCheck, IActivatable
    {
        #region Parameters
        public float MaxHealth {get; set;}
        public float AttackInterval {get; set;}
        public float AttackDelay {get; set;}
        public float CurrentHealth {get; set;}
        public int Damage {get; set;}
        public float AttackRange {get; set;}
        public float speed {get; set;}
        public float ChaseRange {get; set;}
        public bool isActive = false;

        #endregion
    
        #region Movement
    
        public float RandomMovemnt = 5f;
        [SerializeField] private float rotationSpeed = 5f; // Скорость поворота врага
        public Transform player {get; set;}

        #endregion

        #region State Machine Variables
        public EnemyStateMachine StateMachine {get; set;}
        public EnemyAttackState AttackState {get; set;}
        public EnemyChaseState ChaseState {get; set;}
        public EnemyIdleState IdleState {get; set;}
        private AnimationController animationController;
        [SerializeField] private Animator _animation;
        private Vector3 movementDirection;

        private void Awake(){
            StateMachine = new EnemyStateMachine();

            IdleState = new EnemyIdleState(this, StateMachine);
            AttackState = new EnemyAttackState(this, StateMachine);
            ChaseState = new EnemyChaseState(this, StateMachine);
        }
        #endregion
    
        public void Initialize(EnemyConfig config)
        {
            Activate();
            Damage = config.Damage;
            MaxHealth = config.Health;  
            CurrentHealth = MaxHealth;
            AttackRange = config.AttackRange;
            speed = config.Speed;
            ChaseRange = config.ChaseRange;
            AttackInterval = config.AttackInterval;
            AttackDelay = config.AttackDelay;

            // Инициализация базового движения
            Initialize(config.Speed, 0.5f, LayerMask.GetMask("Obstacle"));

            StateMachine.Initialize(IdleState);
            animationController = gameObject.AddComponent<AnimationController>();
            animationController.Initialize(_animation);
        }
        void Update(){
            StateMachine.currentState.FrameUpdate();

            // Обновление анимаций
            animationController.UpdateAnimation(movementDirection, speed);
        }
        public void TakeDamage(int damage)
        {
            CurrentHealth -= damage;
            if (CurrentHealth <= 0)
            {
                Die();
            }
        }

        public void Follow(Vector3 targetPosition)
        {
            if (!isActive) return;

            Vector3 direction = (targetPosition - transform.position).normalized;
            HandleRotation(direction);

            // Используем метод Move из MovableEntity
            Move(direction);

            // Обновляем направление для анимаций
            movementDirection = direction;
        }
        protected override void HandleRotation(Vector3 moveDirection)
        {
            if (moveDirection.magnitude > 0.1f)
            {
                Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }
        }
    
        public void Die()
        {
            Deactivate();
            //Destroy(gameObject);
            Debug.Log("Enemy died!");
        }

        public IDamageable Target { get; set; }

        public void SetChaseState()
        {
            StateMachine.ChangeState(ChaseState);
        }

        public void SetAttackState()
        {
            StateMachine.ChangeState(AttackState);
        }

        public void SetIdleState()
        {
            StateMachine.ChangeState(IdleState);
        }
        public void InitializeTarget(IDamageable target)
        {
            Target = target;
        }
        public Vector3 GetTargetPosition()
        {
            if (Target is MonoBehaviour targetMono)
            {
                return targetMono.transform.position;
            }
            else
            {
                Debug.LogError("Target не содержит Transform!");
                return transform.position;
            }
        }

        private void OnEnable()
        {
            LocationObserver.RegisterEnemy(this);
        }

        private void OnDisable()
        {
            LocationObserver.UnregisterEnemy(this);
        }

        public void Activate()
        {
            isActive = true;
            gameObject.SetActive(true);
        }

        public void Deactivate()
        {
            isActive = false;
            gameObject.SetActive(false);
        }
        public void PlayAttackAnimation()
        {
            if (_animation != null)
            {
                _animation.SetTrigger("IsPunching");
            }
        }
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Player")) // Проверяем, что столкнулись с врагом
            {
                Vector3 pushDirection = (transform.position - collision.transform.position).normalized;
                transform.position += pushDirection * 0.5f; // Смещаем врага назад
            }
        }

    }
}

