using UnityEngine;

public class EnemyController : MonoBehaviour, IDamageable, IMovable
{
    public float MaxHealth {get; set;}
    public float CurrentHealth {get; set;}
    public int Damage {get; set;}
    public float speed {get; set;}
    public Transform player {get; set;}

    #region State Machine Variables

    public float RandomMovemnt = 5f;
    public EnemyStateMachine StateMachine {get; set;}
    public EnemyAttackState AttackState {get; set;}
    public EnemyChaseState ChaseState {get; set;}
    public EnemyIdleState IdleState {get; set;}

    #endregion

    private void Awake(){
        StateMachine = new EnemyStateMachine();

        IdleState = new EnemyIdleState(this, StateMachine);
        AttackState = new EnemyAttackState(this, StateMachine);
        ChaseState = new EnemyChaseState(this, StateMachine);
    }

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
        if (CurrentHealth <= 0)
        {
            Die();
        }
    }

    public void Follow(Vector3 targetPosition)
    {
        transform.position = Vector3.MoveTowards(
            transform.position, // Текущая позиция
            targetPosition, // Целевая позиция
            speed * Time.deltaTime // Расстояние за кадр
        );
    }


    public void Die()
    {
        Destroy(gameObject);
        Debug.Log("Enemy died!");
    }


}

