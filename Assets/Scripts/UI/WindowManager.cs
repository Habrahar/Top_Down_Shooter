using UnityEngine;

namespace UI
{
    public class WindowManager : MonoBehaviour
    {
        public WeaponSelectionWindow weaponSelectionWindow;
        //public IWindow questWindow;

        private void Start()
        {
            //weaponSelectionWindow = FindObjectOfType<WeaponSelectionWindow>();
            //questWindow = FindObjectOfType<QuestWindow>();
        }

        public void OpenWeaponSelectionWindow()
        {
            weaponSelectionWindow.Open();
        }

        public void CloseWeaponSelectionWindow()
        {
            weaponSelectionWindow.Close();
        }

        public void OpenQuestWindow()
        {
            //questWindow.Open();
        }

        public void CloseQuestWindow()
        {
            // questWindow.Close();
        }

        // Дополнительно, метод для обновления окна (например, по мере выполнения задания)
        public void UpdateQuestWindowProgress(int killedEnemies)
        {
            //(questWindow as QuestWindow)?.UpdateProgress(killedEnemies);
        }
    }
}
