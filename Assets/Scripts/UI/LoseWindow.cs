using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace UI
{
    public class LoseWindow : WindowBase
    {
        public GameManager gm;
        
        public override void UpdateWindow()
        {
            
        }
        

        protected override void OnOpen()
        {
            base.OnOpen();
            
        }

        public void Restart()
        {
            gm.StartGame();
            base.Close();
        }

        protected override void OnClose()
        {
            base.OnClose();
        }
       
    }
}