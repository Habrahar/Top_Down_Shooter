using New.Interface;
using UnityEngine;

namespace New
{
    public class EnemyController : MonoBehaviour, IDamageable, IMovable, ITriggerCheck, IActivatable
    {
        #region Parameters
        public float MaxHealth {get; set;}
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
            StateMachine.Initialize(IdleState);
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
            if (isActive == false) return;
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

    }
}

