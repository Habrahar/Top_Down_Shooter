using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


namespace UI
{
    public class WeaponSelectionWindow : WindowBase
    {
        public WeaponConfig[] availableWeapons;
        private int selectedWeaponIndex = 0;
        public static event Action<WeaponConfig> ApplyWeapon; // Событие обновления патронов
        [SerializeField] private TextMeshProUGUI weaponNameText;
        [SerializeField] private Image weaponImage;

        public void SelectWeapon()
        {
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
            SlotUpdate();
        }

        protected override void OnClose()
        {
            base.OnClose();
        }

        public void NextWeapon()
        {
            if (selectedWeaponIndex == availableWeapons.Length - 1)
            {
                selectedWeaponIndex = 0;
            }
            else
            {
                selectedWeaponIndex++;    
            };
            SlotUpdate();
        }

        public void PreviousWeapon()
        {
            if (selectedWeaponIndex == 0)
            {
                selectedWeaponIndex = availableWeapons.Length - 1;
            }
            else
            {
                selectedWeaponIndex--;    
            }
            
            SlotUpdate();
        }
        
        public void SlotUpdate()
        {
            weaponImage.sprite = availableWeapons[selectedWeaponIndex].weaponImage;
            weaponNameText.text = availableWeapons[selectedWeaponIndex].weaponName;
        }
    }

}