using System.Collections;
using System.Collections.Generic;
using System.Linq;
using New;
using UnityEngine;

public class QuestHandler : MonoBehaviour
{
    [SerializeField] private List<QuestConfig> levelQuests; // Список всех квестов для уровней
    private int currentLevelIndex = 0; // Текущий индекс уровня (начинается с 0)
    private Quest currentQuest;

    public void Initialize(int levelIndex)
    {
        currentLevelIndex = levelIndex;
        LoadQuestForCurrentLevel();
    }

    private void OnEnable()
    {
        EnemyController.OnDieTrigger += OnEnemyKilled;
    }

    private void OnDisable()
    {
        EnemyController.OnDieTrigger -= OnEnemyKilled;
    }
    private void LoadQuestForCurrentLevel()
    {
        if (currentLevelIndex >= 0 && currentLevelIndex < levelQuests.Count)
        {
            QuestConfig questConfig = levelQuests[currentLevelIndex];
            currentQuest = new Quest(questConfig);
            Debug.Log($"Quest for Level {currentLevelIndex + 1} initialized: {questConfig.questName}");
        }
        else
        {
            Debug.LogError($"No quest found for Level {currentLevelIndex + 1}");
        }
    }

    public void OnEnemyKilled(EnemyConfig enemyConfig)
    {
        if (currentQuest == null || currentQuest.IsCompleted) return;

        foreach (var goal in currentQuest.config.goals)
        {
            if (goal.targetEnemyType == enemyConfig)
            {
                currentQuest.IncrementGoal(goal);
                Debug.Log($"Quest Progress Updated:\n{currentQuest.GetProgress()}");
            }
        }

        if (currentQuest.IsCompleted)
        {
            Debug.Log($"Quest Completed: {currentQuest.config.questName}");
            RewardPlayer();
            AdvanceToNextLevel();
        }
    }

    private void RewardPlayer()
    {
        Debug.Log($"Player received reward for completing {currentQuest.config.questName}");
        // Здесь можно добавить логику награждения (деньги, опыт, предметы и т.д.)
    }

    private void AdvanceToNextLevel()
    {
        currentLevelIndex++;
        if (currentLevelIndex < levelQuests.Count)
        {
            LoadQuestForCurrentLevel();
        }
        else
        {
            Debug.Log("All levels completed! No more quests available.");
            currentQuest = null;
        }
    }

    public Quest GetCurrentQuest()
    {
        return currentQuest;
    }

    public int GetCurrentLevelIndex()
    {
        return currentLevelIndex;
    }
}