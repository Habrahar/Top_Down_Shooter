using UnityEngine;

namespace UI
{
    public class WindowManager : MonoBehaviour
    {
        public WeaponSelectionWindow weaponSelectionWindow;
        public StartMenuWindow startWindow;
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

        public void OpenStartMenu()
        {
            startWindow.Open();
        }

        public void CloseStartMenu()
        {
            startWindow.Close();
        }
        
    }
}
