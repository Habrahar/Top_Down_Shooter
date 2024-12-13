using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class WeaponSelectionWindow : WindowBase
    {
        public WeaponConfig[] availableWeapons; // Список доступных оружий
        private int selectedWeaponIndex = 0; // Индекс выбранного оружия
        public static event Action<WeaponConfig> ApplyWeapon; // Событие обновления патронов

        public void SelectWeapon(int index)
        {
            selectedWeaponIndex = index;
            ApplyWeapon?.Invoke(availableWeapons[selectedWeaponIndex]);
            UpdateWindow();
        }

        public override void UpdateWindow()
        {
            
            Debug.Log("Weapon Selected: " + availableWeapons[selectedWeaponIndex].weaponName);
        }

        protected override void OnOpen()
        {
            base.OnOpen();
            Debug.Log("Weapon Selection Window Opened");
        }

        protected override void OnClose()
        {
            base.OnClose();
            Debug.Log("Weapon Selection Window Closed");
        }
    }

}