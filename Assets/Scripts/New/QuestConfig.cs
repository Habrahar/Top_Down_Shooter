using UnityEngine;
namespace New
{
    [CreateAssetMenu(fileName = "QuestConfig", menuName = "ScriptableObjects/QuestConfig")]
    public class QuestConfig : ScriptableObject
    {
        public string questName;
        public string questDescription;

        [Header("Goals")]
        public QuestGoal[] goals;

        [Header("Reward")]
        public QuestReward reward;
    }
    
    [System.Serializable]
    public class QuestGoal
    {
        public EnemyConfig targetEnemyType; // Тип врага (можно null для других типов целей)
        public int requiredCount; // Сколько нужно убить/собрать
    }
    [System.Serializable]
    public class QuestReward
    {
        public int gold;
        public int experience;
        public GameObject rewardItem; // Например, оружие или броня
    }

   
}