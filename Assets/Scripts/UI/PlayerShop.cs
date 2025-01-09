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
    [SerializeField] private TextMeshProUGUI costText;
    [SerializeField] private Button buyButton;
    [SerializeField] private Button nextButton;
    [SerializeField] private Button prevButton;

    private GameObject currentCharacter;
    private int currentIndex = 0;
    public override void UpdateWindow()
    {
            
    }

    private void ChangeCharacter(int direction)
    {
        currentIndex = (currentIndex + direction + characters.Count) % characters.Count;
        UpdateCharacterView();
    }

    private void UpdateCharacterView()
    {
        // Удаляем текущую модель
        if (currentCharacter != null)
        {
            Destroy(currentCharacter);
        }

        // Создаем новую модель
        currentCharacter = Instantiate(characters[currentIndex].modelPrefab, characterParent);
        currentCharacter.transform.localPosition = Vector3.zero; // Центрируем
        currentCharacter.transform.localRotation = Quaternion.identity; // Сбрасываем поворот

        // Обновляем текст
        characterNameText.text = characters[currentIndex].characterName;
        health.text = characters[currentIndex].MaxHp.ToString();
        speed.text = characters[currentIndex].speed.ToString();

        if (characters[currentIndex].isBought)
        {
            if (gm.playerPrefab != characters[currentIndex].modelPrefab)
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

    private void BuyCharacter()
    {
        if (characters[currentIndex].isBought)
        {
            gm.playerPrefab = characters[currentIndex].modelPrefab;
        }else
        {
            if( gm.Gold >= characters[currentIndex].cost)
            {
                gm.Gold -= characters[currentIndex].cost;
                characters[currentIndex].isBought = true;

                // Обновляем UI
                UpdateCharacterView();    
            }
            
        }
    }
    
    


        protected override void OnOpen()
        {
            base.OnOpen();
            UpdateCharacterView();

            // Навешиваем действия на кнопки
            nextButton.onClick.AddListener(() => ChangeCharacter(1));
            prevButton.onClick.AddListener(() => ChangeCharacter(-1));
            buyButton.onClick.AddListener(() => BuyCharacter());
        }

        

        protected override void OnClose()
        {
            base.OnClose();
        }
    }
}