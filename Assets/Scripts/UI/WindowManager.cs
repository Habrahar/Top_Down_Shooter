using UnityEngine;

namespace UI
{
    public class WindowManager : MonoBehaviour
    {
        public WeaponSelectionWindow weaponSelectionWindow;
        public StartMenuWindow startWindow;
        public LoseWindow loseWindow;
        //public IWindow questWindow;
        public WinWindow winWindow;

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

        public void OpenStartMenu()
        {
            startWindow.Open();
        }

        public void CloseStartMenu()
        {
            startWindow.Close();
        }
        public void OpenLoseWindow()
        {
            loseWindow.Open();
        }

        public void CloseLoseWindow()
        {
            loseWindow.Close();
        }
        public void OpenWinWindow()
        {
            winWindow.Open();
        }

        public void CloseWinWindow()
        {
            winWindow.Close();
        }
        
    }
}
