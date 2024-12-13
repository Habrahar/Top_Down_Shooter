using UnityEngine;

namespace UI
{
    public abstract class WindowBase : MonoBehaviour, IWindow
    {
        public virtual void Open()
        {
            gameObject.SetActive(true); // Показываем окно
            OnOpen(); // Дополнительные действия при открытии
        }
        public virtual void Close()
        {
            gameObject.SetActive(false); // Скрываем окно
            OnClose(); // Дополнительные действия при закрытии
        }

        protected virtual void OnOpen() { }
        protected virtual void OnClose() { }

        public abstract void UpdateWindow(); // Обновление содержимого
    }
}