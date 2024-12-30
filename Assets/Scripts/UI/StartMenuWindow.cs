using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


namespace UI
{
    public class StartMenuWindow : WindowBase
    {
        public static event Action gameStart; // Событие обновления патронов
        public static event Action shopOpen; // Событие обновления патронов

        protected override void OnOpen()
        {
            base.OnOpen();
        }
        
        protected override void OnClose()
        {
            base.OnClose();
        }

        public void ButtonStart()
        {
            gameStart?.Invoke();
        }

        public void openShop()
        {
            shopOpen?.Invoke();
        }

        public override void UpdateWindow()
        {
            
        }
    }
}

