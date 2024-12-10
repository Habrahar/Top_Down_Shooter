using New.Interface;
using UnityEngine;

namespace New
{
    public class EnemyController : MonoBehaviour, IDamageable, IMovable, ITriggerCheck, ISpawnable
    {
        #region Parameters
        public float MaxHealth {get; set;}
        public float CurrentHealth {get; set;}
        public int Damage {get; set;}
        public float AttackRange {get; set;}
        public float speed {get; set;}
        public float ChaseRange {get; set;}

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

        private void Awake(){
            StateMachine = new EnemyStateMachine();

            IdleState = new EnemyIdleState(this, StateMachine);
            AttackState = new EnemyAttackState(this, StateMachine);
            ChaseState = new EnemyChaseState(this, StateMachine);
        }
        #endregion
    
        public void Initialize(EnemyConfig config)
        {
            Damage = config.Damage;
            MaxHealth = config.Health;
            CurrentHealth = MaxHealth;
            AttackRange = config.AttackRange;
            Debug.Log(CurrentHealth);
            speed = config.Speed;
            ChaseRange = config.ChaseRange;
            StateMachine.Initialize(IdleState);
            OnSpawned();
        }
        void Update(){
            StateMachine.currentState.FrameUpdate();
        }
        public void TakeDamage(int damage)
        {
            CurrentHealth -= damage;
            Debug.Log(CurrentHealth);
            if (CurrentHealth <= 0)
            {
                Die();
            }
        }

        public void Follow(Vector3 targetPosition)
        {
            Vector3 direction = (targetPosition - transform.position).normalized;

            Quaternion targetRotation = Quaternion.LookRotation(direction);

            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRotation,
                rotationSpeed * Time.deltaTime
            );

            transform.position = Vector3.MoveTowards(
                transform.position,
                targetPosition,
                speed * Time.deltaTime
            );
        }
    
        public void Die()
        {
            Destroy(gameObject);
            Debug.Log("Enemy died!");
            OnDespawned();
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
        
        public void OnSpawned()
        {
            LocationObserver.RegisterEnemy(this);
        }

        public void OnDespawned()
        {
            LocationObserver.UnregisterEnemy(this);
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
    }
}

