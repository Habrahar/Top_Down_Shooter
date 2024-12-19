using System.Collections.Generic;

namespace New
{
    public class Quest
    {
        public QuestConfig config { get; private set; }
        public Dictionary<QuestGoal, int> goalProgress { get; private set; } = new Dictionary<QuestGoal, int>();
        public bool IsCompleted => CheckCompletion();

        public Quest(QuestConfig config)
        {
            this.config = config;
            foreach (var goal in config.goals)
            {
                goalProgress[goal] = 0; // Инициализация прогресса
            }
        }

        public void IncrementGoal(QuestGoal goal)
        {
            if (goalProgress.ContainsKey(goal) && goalProgress[goal] < goal.requiredCount)
            {
                goalProgress[goal]++;
            }
        }

        private bool CheckCompletion()
        {
            foreach (var goal in config.goals)
            {
                if (goalProgress[goal] < goal.requiredCount)
                    return false;
            }
            return true;
        }

        public string GetProgress()
        {
            string progress = "";
            foreach (var goal in config.goals)
            {
                progress += $"{goal.targetEnemyType.enemyName}: {goalProgress[goal]}/{goal.requiredCount}\n";
            }
            return progress;
        }
    }
}
