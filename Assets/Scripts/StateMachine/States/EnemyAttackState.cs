using System.Collections;
using New;
using UnityEngine;

public class EnemyAttackState : EnemyState
{
    private bool isAttacking;

    public EnemyAttackState(EnemyController enemy, EnemyStateMachine enemyStateMachine) : base(enemy, enemyStateMachine)
    {
       

    }

    public override void EnterState()
    {
        base.EnterState();
        StartAttack();
    }

    private void StartAttack()
    {
        
        if (!isAttacking && enemy.Target != null)
        {
            isAttacking = true;
            enemy.PlayAttackAnimation();
            enemy.StartCoroutine(DelayedAttack());
        }
    }

    private IEnumerator DelayedAttack()
    {
        yield return new WaitForSeconds(enemy.AttackDelay);

        if (enemy.Target != null && Vector3.Distance(enemy.transform.position, enemy.GetTargetPosition()) <= enemy.AttackRange)
        {
            // Наносим урон, если игрок в радиусе атаки
            enemy.Target.TakeDamage(enemy.Damage);
            Debug.Log("Нанесен урон игроку");
        }
        else
        {
            Debug.Log("Цель вышла из радиуса атаки");
        }

        yield return new WaitForSeconds(enemy.AttackInterval);
        isAttacking = false;

        // Проверяем состояние после атаки
        if (enemy.Target == null || Vector3.Distance(enemy.transform.position, enemy.GetTargetPosition()) > enemy.AttackRange)
        {
            enemy.SetChaseState(); // Возвращаемся в погоню
        }
        else
        {
            StartAttack(); // Повторяем атаку
        }
    }

    public override void ExitState()
    {
        base.ExitState();
        isAttacking = false;
        enemy.StopAllCoroutines(); // Останавливаем атаки при выходе из состояния
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();

        // Проверка выхода из радиуса атаки
        if (enemy.Target == null || Vector3.Distance(enemy.transform.position, enemy.GetTargetPosition()) > enemy.AttackRange)
        {
            enemy.SetChaseState();
        }
    }
}
