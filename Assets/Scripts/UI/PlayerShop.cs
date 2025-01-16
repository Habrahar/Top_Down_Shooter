using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace UI
{
    public class PlayerShop : WindowBase
    {
        public List<CharacterConfig> characters = new List<CharacterConfig>();

        public GameManager gm;
        
        
        [SerializeField] private Transform characterParent; // Родитель для моделей
        [SerializeField] private TextMeshProUGUI characterNameText;
        [SerializeField] private TextMeshProUGUI health;
        [SerializeField] private TextMeshProUGUI speed;
        [SerializeField] private TextMeshProUGUI radiusAttack;
        [SerializeField] private TextMeshProUGUI costText;
        [SerializeField] private Button buyButton;
        [SerializeField] private Button nextButton;
        [SerializeField] private Button prevButton;

        private GameObject currentCharacter;
        private int currentIndex = 0;

        public void ResetAllCharachters(CharacterConfig playerdef)
        {
            foreach (var player in characters)
            {
                if (player.isBought && player != playerdef)
                {
                    player.isBought = false;
                }
            }
        }
        public override void UpdateWindow()
        {
            // Удаляем текущую модель
            if (currentCharacter != null)
            {
                Destroy(currentCharacter);
            }

            // Создаем новую модель
            currentCharacter = Instantiate(characters[currentIndex].lobbyPrefab, characterParent);
            currentCharacter.transform.localPosition = Vector3.zero; // Центрируем
            currentCharacter.transform.localRotation = Quaternion.identity; // Сбрасываем поворот

            // Обновляем текст
            characterNameText.text = characters[currentIndex].characterName;
            health.text = characters[currentIndex].MaxHp.ToString();
            speed.text = characters[currentIndex].speed.ToString();
            radiusAttack.text = characters[currentIndex].radiusAttack.ToString();

            if (characters[currentIndex].isBought)
            {
                if (gm.playerPrefab != characters[currentIndex])
                {
                    costText.text = "Выбрать";
                    buyButton.interactable = true;
                        
                }
                else
                {
                    costText.text = "Выбрано";
                    buyButton.interactable = false;    
                }
            }
            else
            {
                costText.text = "Цена: " + characters[currentIndex].cost;
                buyButton.interactable = true;
            }
        }

        public void LeftButton()
        {
            if (currentIndex == 0)
            {

                currentIndex = characters.Count - 1;
            }
            else
            {
                currentIndex--;
            }
            UpdateWindow();
        }

        public void RightButton()
        {
            if (currentIndex == characters.Count - 1)
            {
                currentIndex = 0;
            }
            else
            {
                currentIndex++;
            }
            UpdateWindow();
        }

        public void ChangeCharacter(int direction)
        {
            currentIndex = (currentIndex + direction + characters.Count) % characters.Count;
            UpdateWindow();
        }
        
        public void BuyCharacter()
        {
            if (characters[currentIndex].isBought)
            {
                gm.playerPrefab = characters[currentIndex];
                UpdateWindow();
            }else
            {
                if( gm.Gold >= characters[currentIndex].cost)
                {
                    gm.Gold -= characters[currentIndex].cost;
                    gm.UpdateGold();
                    characters[currentIndex].isBought = true;

                    // Обновляем UI
                    UpdateWindow();    
                }
                
            }
        }
    
    


        protected override void OnOpen()
        {
            base.OnOpen();
            UpdateWindow();
        }

        

        protected override void OnClose()
        {
            base.OnClose();
        }
    }
}