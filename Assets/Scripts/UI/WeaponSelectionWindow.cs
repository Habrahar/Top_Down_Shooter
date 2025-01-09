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
        public static event Action<WeaponConfig, int> ApplyWeapon; // Событие обновления патронов
        [SerializeField] private TextMeshProUGUI weaponNameText;
        [SerializeField] private TextMeshProUGUI buyCost;
        [SerializeField] private Image weaponImage;
        [SerializeField] private Button buyButton;
        [SerializeField] private GameObject weaponSlotPrefab; // Префаб слота
        [SerializeField] private Transform contentParent;    // Ссылка на Content в Scroll View

        private List<GameObject> instantiatedSlots = new List<GameObject>();
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
                gm.UpdateGold();
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
            PopulateWeaponList();
            //SlotUpdate();
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
        public void PopulateWeaponList()
{
    // Удаляем старые слоты
    foreach (var slot in instantiatedSlots)
    {
        Destroy(slot);
    }
    instantiatedSlots.Clear();

    // Создаем новые слоты
    foreach (var weapon in availableWeapons)
    {
        GameObject slot = Instantiate(weaponSlotPrefab, contentParent);
        instantiatedSlots.Add(slot);

        // Получаем ссылки на UI-элементы внутри слота
        TextMeshProUGUI nameText = slot.transform.Find("WeaponName").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI damageText = slot.transform.Find("Damage").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI fireRateText = slot.transform.Find("FireRate").GetComponent<TextMeshProUGUI>();
        Image weaponImage = slot.transform.Find("WeaponImage").GetComponent<Image>();
        Button actionButton = slot.transform.Find("ActionButton").GetComponent<Button>();
        TextMeshProUGUI buttonText = actionButton.GetComponentInChildren<TextMeshProUGUI>();

        // Заполняем данные
        nameText.text = weapon.weaponName;
        damageText.text = "Урон: " + weapon.bulletDamage.ToString();
        fireRateText.text = "Скорость: " + weapon.fireRate.ToString();
        weaponImage.sprite = weapon.weaponImage;

        // Настраиваем кнопку
        if (weapon.isBought)
        {
            if (gm.currentWeapon != weapon)
            {
                buttonText.text = "Экипировать";
            
                    
            }
            else
            {
                actionButton.interactable = false;
                buttonText.text = "Экипировано";    
            }
        }
        else
        {
            buttonText.text = weapon.cost.ToString();
            actionButton.interactable = gm.Gold >= weapon.cost; // Заблокируем, если недостаточно золота
        }

        // Добавляем функционал для кнопки
        actionButton.onClick.RemoveAllListeners();
        actionButton.onClick.AddListener(() =>
        {
            if (weapon.isBought)
            {
                EquipWeapon(weapon);
            }
            else
            {
                BuyWeapon(weapon, buttonText, actionButton);
            }
        });
    }
}
        private void BuyWeapon(WeaponConfig weapon, TextMeshProUGUI buttonText, Button actionButton)
        {
            if (gm.Gold >= weapon.cost)
            {
                gm.Gold -= weapon.cost;
                gm.UpdateGold();
                weapon.isBought = true;

                // Обновляем кнопку после покупки
                buttonText.text = "Экипировать";
                actionButton.interactable = gm.currentWeapon != weapon;
            }
        }
        private void EquipWeapon(WeaponConfig weapon)
        {
            gm.currentWeapon = weapon;
            PopulateWeaponList(); // Обновляем весь список после изменения текущего оружия
        }




        private void SelectWeaponFromSlot(WeaponConfig weapon)
        {
            selectedWeaponIndex = System.Array.IndexOf(availableWeapons, weapon);
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