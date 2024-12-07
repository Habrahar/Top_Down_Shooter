using UnityEngine;

public interface ITriggerCheck
{
    bool IsAggred {get; set;}
    bool IsInAttackPosition{get; set;}
    public void SetAggroStatus(bool IsAggred);
    public void SetAttackPosition(bool IsInAttackPosition);
}
