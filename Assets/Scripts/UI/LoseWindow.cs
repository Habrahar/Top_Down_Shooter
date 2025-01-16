using System.Collections;
using New;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace UI
{
    public class LoseWindow : WindowBase
    {
        public GameManager gm;
        [Header("Animation Settings")]
        [SerializeField] private float animationDuration = 0.5f; // Длительность анимации
        private RectTransform rectTransform;
        
        public override void UpdateWindow()
        {
            
        }
        

        protected override void OnOpen()
        {
            base.OnOpen();
            StartCoroutine(AnimateWindowOpen());
            
        }

        public void Restart()
        {
            gm.StartGame();
            base.Close();
        }
        private IEnumerator AnimateWindowOpen()
        {
            float elapsedTime = 0f;
            CanvasGroup canvasGroup = GetComponent<CanvasGroup>();

            if (canvasGroup == null)
            {
                canvasGroup = gameObject.AddComponent<CanvasGroup>();
            }

            canvasGroup.alpha = 0f; // Начальная прозрачность
            canvasGroup.interactable = false; // Отключаем взаимодействие во время анимации
            canvasGroup.blocksRaycasts = false;

            while (elapsedTime < animationDuration)
            {
                elapsedTime += Time.deltaTime;
                float t = elapsedTime / animationDuration;

                // Линейная интерполяция прозрачности
                canvasGroup.alpha = Mathf.Lerp(0f, 1f, t);
                yield return null;
            }

            canvasGroup.alpha = 1f; // Устанавливаем финальную прозрачность
            canvasGroup.interactable = true; // Включаем взаимодействие
            canvasGroup.blocksRaycasts = true;
        }
        

        protected override void OnClose()
        {
            base.OnClose();
        }
       
    }
}