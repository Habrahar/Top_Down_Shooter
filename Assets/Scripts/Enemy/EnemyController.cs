using System;
using Interface;
using New.Interface;
using UnityEngine;
using UnityEngine.AI;

namespace New
{
    public class EnemyController : MovableEntity, IDamageable, IMovable, ITriggerCheck, IActivatable, IPoolable
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
        private ObjectPool<EnemyController> pool;
        public NavMeshAgent agent;

        #endregion
    
        #region Movement
    
        public float RandomMovemnt = 5f;
        [SerializeField] private float rotationSpeed = 5f; // Скорость поворота врага
        public Transform player {get; set;}
        private EnemyConfig _enemy;

        #endregion

        #region State Machine Variables
        public EnemyStateMachine StateMachine {get; set;}
        public EnemyAttackState AttackState {get; set;}
        public EnemyChaseState ChaseState {get; set;}
        public EnemyIdleState IdleState {get; set;}
        private AnimationController animationController;
        [SerializeField] private Animator _animation;
        private Vector3 movementDirection;
        public static event Action<EnemyConfig> OnDieTrigger; // Событие обновления патронов
        public static event Action dieTrigger; // Событие обновления патронов

        [SerializeField] public HealthController hpBar;
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
            _enemy = config;
            agent = GetComponent<NavMeshAgent>();
            agent.speed = speed;
            // Инициализация базового движения
            Initialize(config.Speed, 0.5f, LayerMask.GetMask("Collision"));

            StateMachine.Initialize(IdleState);
            animationController = gameObject.AddComponent<AnimationController>();
            animationController.Initialize(_animation);
            setNavMesh();
        }

        private void setNavMesh()
        {
            if (!agent.isOnNavMesh)
            {
                Debug.LogError($"Враг {gameObject.name} не на NavMesh! Перемещаем...");
                NavMeshHit hit;
                if (NavMesh.SamplePosition(transform.position, out hit, 5f, NavMesh.AllAreas))
                {
                    transform.position = hit.position;
                    agent.Warp(hit.position); // Перемещаем агента на правильную позицию на NavMesh
                }
                else
                {
                    Debug.LogError("Не удалось найти ближайшую точку NavMesh!");
                }
            }
        }


        public void SetPool(ObjectPool<EnemyController> pool)
        {
            this.pool = pool;
        }
        void Update(){
            StateMachine.currentState.FrameUpdate();

            // Обновление анимаций
            animationController.UpdateAnimation(movementDirection, speed);
        }
        public void TakeDamage(int damage)
        {
            CurrentHealth -= damage;
            hpBar.UpdateHealthBar(damage);
            if (CurrentHealth <= 0)
            {
                Die();
            }
        }

        private void AvoidObstacles(ref Vector3 direction)
        {
            if (Physics.SphereCast(transform.position, 0.5f, direction, out RaycastHit hit, 1f, LayerMask.GetMask("Collision")))
            {
                Vector3 avoidDirection = Vector3.Reflect(direction, hit.normal);
                direction = avoidDirection.normalized;
            }
        }

        public void Follow(Vector3 targetPosition)
        {
            if (!isActive || !IsPlayerInLineOfSight(targetPosition)) return;
            
            
            Vector3 direction = (targetPosition - transform.position).normalized;

            //AvoidObstacles(ref direction);

            //HandleRotation(direction);
            Move(direction);
            movementDirection = direction;
        }
    
        public bool IsPlayerInLineOfSight(Vector3 target)
        {
            Vector3 directionToTarget = (target - transform.position).normalized;
            float distanceToTarget = Vector3.Distance(transform.position, target);
            
            if (Physics.Raycast(transform.position, directionToTarget, out RaycastHit hit, ChaseRange))
            {
                if (hit.transform.CompareTag("Player"))
                {
                    return true;
                }
                else if(hit.transform.CompareTag("Enemy"))
                {
                    return true; // Есть преграда
                }
                else
                {
                    return false;
                }
            }
            return true;
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
            hpBar.destroyHP();
            
            EnemySpawner.Instance.ReturnEnemy(this, _enemy);
            dieTrigger?.Invoke();
            
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
            // Проверяем, не равен ли Target null
            if (Target == null)
            {
               // Debug.LogWarning("Target отсутствует! Возвращаем текущую позицию.");
                return transform.position;
            }

            // Проверяем, является ли Target MonoBehaviour и имеет Transform
            if (Target is MonoBehaviour targetMono)
            {
                // Убеждаемся, что Transform не удален
                if (targetMono != null && targetMono.transform != null)
                {
                    return targetMono.transform.position;
                }
            }

            // Если что-то пошло не так, возвращаем текущую позицию врага
            //Debug.LogError("Target не содержит Transform или недоступен!");
            return transform.position;
        }


        private void OnEnable()
        {
            LocationObserver.RegisterEnemy(this);
        }

        private void OnDisable()
        {
            LocationObserver.UnregisterEnemy(this);
        }
        public void OnSpawn()
        {
            // Здесь можно сбросить состояние врага
        }

        public void OnDespawn()
        {
            // Здесь можно очистить состояние врага
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
        /*private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Obstacle") || collision.gameObject.CompareTag("Player")) // Проверяем, что столкнулись с врагом
            {
                Vector3 pushDirection = (transform.position - collision.transform.position).normalized;
                transform.position += pushDirection * 0.5f; // Смещаем врага назад
            }
        }*/
        


    }
}

