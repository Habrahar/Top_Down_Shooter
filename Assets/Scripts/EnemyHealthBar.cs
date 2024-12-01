using UnityEngine;
using UnityEngine.UI;
using System;

public class EnemyHealthBar : MonoBehaviour
{
    [SerializeField]
    private Image _healthBarSprite;
    private Transform CameraTransform; // Ссылка на камеру



    public void Start(){
        CameraTransform = Camera.main.transform; // Получаем главную камеру

    }

    private void LateUpdate()
    {
        
        transform.LookAt(transform.position + CameraTransform.forward);
        
    }

    public void UpdateHealthBar(float currentHealth, float maxHealth)
    {
        _healthBarSprite.fillAmount = currentHealth / maxHealth; // Получаем текущее здоровье врага
    }
}
