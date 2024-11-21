using UnityEngine;
using TMPro;
using UnityEngine.UI;  // Для использования UI Text


public class DamagePopup_Controller : MonoBehaviour
{
   public float moveSpeed = 1f;  // Скорость движения текста
    public float lifeTime = 1f;   // Время жизни попапа
    private Text damageText;

    private void Start()
    {
        damageText = GetComponent<Text>();  // Получаем ссылку на компонент текста
    }

    public void Initialize(int damageAmount, Vector3 spawnPosition)
    {
        damageText.text = damageAmount.ToString();  // Устанавливаем значение урона
        transform.position = spawnPosition;  // Устанавливаем позицию начала движения
        Destroy(gameObject, lifeTime);  // Уничтожаем попап через время жизни
    }

    private void Update()
    {
        // Перемещаем попап вверх
        transform.position += Vector3.up * moveSpeed * Time.deltaTime;
    }
}
