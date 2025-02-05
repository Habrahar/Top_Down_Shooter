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
        public PlayerShop playerShop;
        public GoldWindowManager goldWindowManager;
        public GoldCounterWindow goldcurrency;

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
        public void OpenplayerShop()
        {
            playerShop.Open();
        }

        public void CloseplayerShop()
        {
            playerShop.Close();
        }
        public void OpenWinWindow()
        {
            winWindow.Open();
        }

        public void CloseWinWindow()
        {
            winWindow.Close();
        }
        public void OpenGoldManager()
        {
            goldWindowManager.Open();
        }

        public void CloseGoldManager()
        {
            goldWindowManager.Close();
        }
        public void OpenGoldCurrency()
        {
            goldcurrency.Open();
        }

        public void CloseGoldCurrency()
        {
            goldcurrency.Close();
        }
        
    }
}
