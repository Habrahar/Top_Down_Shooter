using UnityEngine;

namespace UI
{
    public class WindowControl : MonoBehaviour
    {
        private WindowManager windowManager;

        private void Start()
        {
            windowManager = FindObjectOfType<WindowManager>();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Q)) // Открыть окно выбора оружия
            {
                windowManager.OpenWeaponSelectionWindow();
            }

            if (Input.GetKeyDown(KeyCode.T)) // Открыть окно задания
            {
                windowManager.OpenQuestWindow();
            }
        }
    }
}
