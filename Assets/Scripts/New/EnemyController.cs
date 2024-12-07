using UnityEngine;

public class EnemyController : MonoBehaviour, IDamageable, IMovable, ITriggerCheck
{
    #region Parameters
    public float MaxHealth {get; set;}
    public float CurrentHealth {get; set;}
    public int Damage {get; set;}
    public float speed {get; set;}

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
    public bool IsAggred { get;set; }
    public bool IsInAttackPosition { get; set; }

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
        Debug.Log(CurrentHealth);
        speed = config.Speed;
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
        Vector3 direction = (targetPosition - transform.position).normalized;

        Quaternion targetRotation = Quaternion.LookRotation(direction);

        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            targetRotation,
            rotationSpeed * Time.deltaTime
        );

        // Двигаемся к цели
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
    }

    public void SetAggroStatus(bool isAggred)
    {
        IsAggred = isAggred;
    }

    public void SetAttackPosition(bool isInAttackPosition)
    {
        IsInAttackPosition = isInAttackPosition;
    }
}

