using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthController : MonoBehaviour
{
    public float currentHealth;
    public float maxHealth;
    private GameObject healthBar;
    [SerializeField] public Image fillImage; 
    [SerializeField]
    public GameObject healthBarPrefab;
    [SerializeField] private Transform healthBarAnchor; // Точка привязки для ХП бара

    
    public void SetHealth(float health)
    {
        maxHealth = health;
        currentHealth = maxHealth;
        Canvas canvas = GameObject.Find("HealthBarCanvas").GetComponent<Canvas>();
        if (canvas == null)
        {
            Debug.LogError("Canvas with name 'YourCanvasName' not found!");
            return;
        }

        // Создаем healthBar и делаем его дочерним объектом канваса
        healthBar = Instantiate(healthBarPrefab, canvas.transform);
        // Get the fill image component
        fillImage = healthBar.transform.Find("Fill").GetComponent<Image>();
        // Initialize the fill size based on current health
        UpdateHealthBar(0);
    }
    
    public void UpdateHealthBar(float damage)
    {
        currentHealth -= damage;
        if (healthBar == null) return;
        // Calculate the fill ratio
        float fillAmount = (float)currentHealth / maxHealth;
        // Get the fill image component
        Image fillImage = healthBar.transform.Find("Fill").GetComponent<Image>();
        // Set the fill size
        fillImage.fillAmount = fillAmount;
    }

    void AlignWithCamera()
    {
        if (healthBar == null || healthBarAnchor == null) return;

        // Позиционируем healthBar над юнитом
        healthBar.transform.position = healthBarAnchor.position;

        // Поворачиваем healthBar к камере
        healthBar.transform.rotation = Camera.main.transform.rotation;
    }

    public void destroyHP()
    {
        Destroy(healthBar);
    }

    void Update()
    {
        AlignWithCamera();
    }
    
}

