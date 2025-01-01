using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;


namespace UI
{
    public class WeaponSelectionWindow : WindowBase
    {
        public WeaponConfig[] availableWeapons;
        private int selectedWeaponIndex = 0;
        public static event Action<WeaponConfig, int> ApplyWeapon; // Событие обновления патронов
        [SerializeField] private TextMeshProUGUI weaponNameText;
        [SerializeField] private TextMeshProUGUI buyCost;
        [SerializeField] private Image weaponImage;
        [SerializeField] private Button buyButton;
        public GameManager gm;

        public void SelectWeapon()
        {
            if (availableWeapons[selectedWeaponIndex].isBought)
            {
                ApplyWeapon?.Invoke(availableWeapons[selectedWeaponIndex], availableWeapons[selectedWeaponIndex].cost);    
            }
            else
            {
                gm.Gold -= availableWeapons[selectedWeaponIndex].cost;
                availableWeapons[selectedWeaponIndex].isBought = true;    
            }
            SlotUpdate();
            
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
            if (availableWeapons[selectedWeaponIndex].isBought)
            {
                if (gm.currentWeapon == availableWeapons[selectedWeaponIndex])
                {
                    buyButton.interactable = false;
                    buyCost.text ="Экиперовано";
                }
                else
                {
                    buyButton.interactable = true;
                    buyCost.text ="Экиперовать";
                }
                
            }
            else
            {
                buyCost.text = availableWeapons[selectedWeaponIndex].cost.ToString();    
                
                if (gm.Gold <= availableWeapons[selectedWeaponIndex].cost)
                {
                    buyButton.interactable = false;
                }
                else
                {
                    buyButton.interactable = true;
                }
            }
            weaponImage.sprite = availableWeapons[selectedWeaponIndex].weaponImage;
            weaponNameText.text = availableWeapons[selectedWeaponIndex].weaponName;
        }
    }

}