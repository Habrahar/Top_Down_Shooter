using Level;
using UnityEngine;

namespace UI
{
    public class WindowControl : MonoBehaviour
    {
        private WindowManager windowManager;
        public LevelManager gm;

        public void Initialized()
        {
            windowManager = FindObjectOfType<WindowManager>();
        }

        private void Update()
        {
            
            if (Input.GetKeyDown(KeyCode.Q)) // Открыть окно выбора оружия
            {
                gm.StartNextLevel(1);
                
            }

            /*if (Input.GetKeyDown(KeyCode.T)) // Открыть окно задания
            {
                windowManager.OpenQuestWindow();
            }*/
        }
    }
}
