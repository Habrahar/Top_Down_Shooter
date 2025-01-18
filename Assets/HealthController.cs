using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthController : MonoBehaviour
{
    public enum HealthControllerType
    {
        None,    // Для врагов (без патронов и перезарядки)
        Bullets  // Для игрока (с патронами и перезарядкой)
    }

    [SerializeField] private HealthControllerType type = HealthControllerType.None; // Тип HealthController

    public float currentHealth;
    public float maxHealth;
    private GameObject healthBar;
    private Image fillImage;
    [SerializeField] public GameObject healthBarPrefab;
    [SerializeField] private Transform healthBarAnchor; // Точка привязки для ХП бара

    // Переменные для управления патронами и перезарядкой
    private Image reloadImage; // Спрайт перезарядки
    private Image bulletReloadImage; // Спрайт патронов
    private TextMeshProUGUI bulletCountText; // Текст для отображения количества патронов
    private int currentBullets; // Текущее количество патронов
    public int maxBullets; // Максимальное количество патронов
    private bool isReloading; // Флаг перезарядки

    public void SetHealth(float health)
    {
        maxHealth = health;
        currentHealth = maxHealth;

        // Ищем Canvas с именем "HealthBarCanvas"
        GameObject canvasObject = GameObject.Find("HealthBarCanvas");
        if (canvasObject == null)
        {
            Debug.LogError("Объект с именем 'HealthBarCanvas' не найден!");
            return;
        }

        // Получаем компонент Canvas
        Canvas canvas = canvasObject.GetComponent<Canvas>();
        if (canvas == null)
        {
            Debug.LogError("Компонент Canvas не найден на объекте 'HealthBarCanvas'!");
            return;
        }

        // Создаем healthBar и делаем его дочерним объектом канваса
        healthBar = Instantiate(healthBarPrefab, canvas.transform);
        if (healthBar == null)
        {
            Debug.LogError("Не удалось создать healthBar из префаба!");
            return;
        }

        // Получаем компонент fillImage
        Transform fillTransform = healthBar.transform.Find("Fill");
        if (fillTransform == null)
        {
            Debug.LogError("Объект 'Fill' не найден в healthBar!");
            return;
        }

        fillImage = fillTransform.GetComponent<Image>();
        if (fillImage == null)
        {
            Debug.LogError("Компонент Image не найден на объекте 'Fill'!");
            return;
        }

        // Инициализируем fillImage в зависимости от текущего здоровья
        UpdateHealthBar(0);

        // Если тип HealthController - Bullets, инициализируем компоненты для патронов и перезарядки
        if (type == HealthControllerType.Bullets)
        {
            Transform bulletsTransform = healthBar.transform.Find("Bullets");
            if (bulletsTransform == null)
            {
                Debug.LogError("Объект 'Bullets' не найден в healthBar!");
                return;
            }

            Transform reloadTransform = bulletsTransform.Find("Reload");
            if (reloadTransform == null)
            {
                Debug.LogError("Объект 'Bullets/Reload' не найден в healthBar!");
                return;
            }

            reloadImage = reloadTransform.GetComponent<Image>();
            if (reloadImage == null)
            {
                Debug.LogError("Компонент Image не найден на объекте 'Bullets/Reload'!");
                return;
            }

            Transform bulletReloadTransform = bulletsTransform.Find("BulletReload");
            if (bulletReloadTransform == null)
            {
                Debug.LogError("Объект 'Bullets/BulletReload' не найден в healthBar!");
                return;
            }

            bulletReloadImage = bulletReloadTransform.GetComponent<Image>();
            if (bulletReloadImage == null)
            {
                Debug.LogError("Компонент Image не найден на объекте 'Bullets/BulletReload'!");
                return;
            }

            Transform bulletCountTransform = bulletsTransform.Find("BulletCount");
            if (bulletCountTransform == null)
            {
                Debug.LogError("Объект 'Bullets/BulletCount' не найден в healthBar!");
                return;
            }

            bulletCountText = bulletCountTransform.GetComponent<TextMeshProUGUI>();
            if (bulletCountText == null)
            {
                Debug.LogError("Компонент Text не найден на объекте 'Bullets/BulletCount'!");
                return;
            }

            // Скрываем спрайты перезарядки при инициализации
            reloadImage.gameObject.SetActive(false);
            bulletReloadImage.gameObject.SetActive(false);
        }
    }

    public void UpdateHealthBar(float damage)
    {
        currentHealth -= damage;
        if (healthBar == null) return;
        // Вычисляем fillAmount
        float fillAmount = (float)currentHealth / maxHealth;
        // Устанавливаем fillAmount
        fillImage.fillAmount = fillAmount;
    }

    // Метод для обновления количества патронов
    public void UpdateBullets(int bullets)
    {
        if (type != HealthControllerType.Bullets) return; // Если тип не Bullets, выходим

        currentBullets = bullets;
        bulletCountText.text = currentBullets.ToString();
    }

    // Метод для начала перезарядки
    public void StartReload(float reloadTime)
    {
        if (type != HealthControllerType.Bullets || isReloading) return; // Если тип не Bullets или уже перезаряжаемся, выходим

        StartCoroutine(ReloadCoroutine(reloadTime));
    }

    // Корутина для перезарядки
    private IEnumerator ReloadCoroutine(float reloadTime)
    {
        isReloading = true;
        reloadTime -= 0.1f;
        // Показываем спрайт перезарядки
        reloadImage.gameObject.SetActive(true);
        bulletReloadImage.gameObject.SetActive(true);

        float elapsedTime = 0f;
        while (elapsedTime < reloadTime)
        {
            // Обновляем fillAmount спрайта перезарядки
            reloadImage.fillAmount = elapsedTime / reloadTime;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Скрываем спрайт перезарядки
        reloadImage.gameObject.SetActive(false);
        bulletReloadImage.gameObject.SetActive(false);

        // Обновляем количество патронов
        UpdateBullets(maxBullets);

        isReloading = false;
    }

    // Метод для уничтожения ХП бара
    public void destroyHP()
    {
        Destroy(healthBar);
    }

    // Метод для выравнивания ХП бара относительно камеры
    void AlignWithCamera()
    {
        if (healthBar == null || healthBarAnchor == null) return;

        // Позиционируем healthBar над юнитом
        healthBar.transform.position = healthBarAnchor.position;

        // Поворачиваем healthBar к камере
        healthBar.transform.rotation = Camera.main.transform.rotation;
    }

    void Update()
    {
        AlignWithCamera();
    }
}