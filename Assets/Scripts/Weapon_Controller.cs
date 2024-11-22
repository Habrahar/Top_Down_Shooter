using System;
using UnityEngine;

public class Weapon_Controller : MonoBehaviour
{
    [SerializeField] private WeaponConfig weaponConfig; // Конфигурация оружия
    private GameObject currentWeaponInstance; // Текущий экземпляр оружия
    private Transform firePoint;
    private Transform ejectPoint;

    private float nextFireTime;
    private int currentMagazineAmmo; // Текущий боезапас в магазине
    private int totalAmmo; // Общее количество патронов
    private bool isReloading = false;

    public static event Action<int, int> OnAmmoUpdate; // Событие обновления патронов

    private void OnEnable()
    {
        Player_shooting.OnShoot += HandleShoot;
    }

    private void OnDisable()
    {
        Player_shooting.OnShoot -= HandleShoot;
    }

    private void Start()
    {
        if (weaponConfig == null)
        {
            Debug.LogError("WeaponConfig не назначен в Weapon_Controller!");
            return;
        }

        InitializeWeapon();
        InitializeAmmo();
    }

    private void InitializeWeapon()
    {
        if (currentWeaponInstance != null)
        {
            Destroy(currentWeaponInstance);
        }

        currentWeaponInstance = Instantiate(weaponConfig.weaponPrefab, transform);

        firePoint = currentWeaponInstance.transform.Find("FirePoint");
        ejectPoint = currentWeaponInstance.transform.Find("EjectPoint");

        if (firePoint == null) Debug.LogError("FirePoint не найден в префабе оружия!");
        if (ejectPoint == null) Debug.LogError("EjectPoint не найден в префабе оружия!");
    }

    private void InitializeAmmo()
    {
        totalAmmo = weaponConfig.maxTotalAmmo; // Устанавливаем общее количество патронов
        currentMagazineAmmo = weaponConfig.magazineSize; // Полный магазин
        OnAmmoUpdate?.Invoke(currentMagazineAmmo, totalAmmo); // Обновляем UI
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && !isReloading) // Ручная перезарядка
        {
            TryReload();
        }
    }

    private void HandleShoot(Vector3 targetPosition)
    {
        if (isReloading || Time.time < nextFireTime || firePoint == null || currentMagazineAmmo <= 0) return;

        nextFireTime = Time.time + weaponConfig.fireRate;

        // Стреляем пулей
        Vector3 direction = (targetPosition - firePoint.position).normalized;
        direction.y = 0f;

        GameObject bullet = Instantiate(weaponConfig.bulletPrefab, firePoint.position, Quaternion.LookRotation(direction));
        bullet_controller bulletScript = bullet.GetComponent<bullet_controller>();
        bulletScript.Initialize(weaponConfig.bulletSpeed, weaponConfig.bulletDamage, direction, weaponConfig.bulletDistance);

        // Вылет гильзы
        EjectShell();

        currentMagazineAmmo--; // Уменьшаем количество патронов в магазине
        OnAmmoUpdate?.Invoke(currentMagazineAmmo, totalAmmo); // Обновляем UI

        if (currentMagazineAmmo <= 0)
        {
            TryReload(); // Автоматическая перезарядка
        }
    }

    private void TryReload()
    {
        if (currentMagazineAmmo == weaponConfig.magazineSize || totalAmmo <= 0) return; // Если магазин полон или патронов нет

        isReloading = true;
        Invoke(nameof(Reload), weaponConfig.reloadTime);
    }

    private void Reload()
    {
        int neededAmmo = weaponConfig.magazineSize - currentMagazineAmmo; // Сколько нужно для полного магазина
        int ammoToReload = Mathf.Min(neededAmmo, totalAmmo); // Сколько можем перезарядить

        totalAmmo -= ammoToReload;
        currentMagazineAmmo += ammoToReload;

        OnAmmoUpdate?.Invoke(currentMagazineAmmo, totalAmmo); // Обновляем UI

        isReloading = false;
    }

   private void EjectShell() // Вылет гильз из точки
    {
        if (weaponConfig.shellPrefab != null && ejectPoint != null)
        {
            GameObject shell = Instantiate(weaponConfig.shellPrefab, ejectPoint.position, ejectPoint.rotation);
            Rigidbody shellRb = shell.GetComponent<Rigidbody>();

            if (shellRb != null)
            {
                // Рассчитываем направление вылета вбок
                Vector3 randomOffset = new Vector3(
                    UnityEngine.Random.Range(-0.4f, 0.4f), // случайное смещение по X
                    UnityEngine.Random.Range(0.5f, 0.10f), // случайное смещение по Y (немного вверх)
                    UnityEngine.Random.Range(-0.2f, 0.2f)  // случайное смещение по Z
                );

                // Основное направление вбок
                Vector3 shellDirection = (firePoint.right + randomOffset).normalized; // Используем firePoint.right для направления вбок

                // Применяем силу к Rigidbody
                shellRb.AddForce(shellDirection * weaponConfig.shellEjectForce, ForceMode.Impulse);
            }

            Destroy(shell, weaponConfig.shellLifeTime);
        }
    }




    
    public void AddAmmo(int amount)
    {
        totalAmmo += amount;
        OnAmmoUpdate?.Invoke(currentMagazineAmmo, totalAmmo); // Обновляем UI
    }

}
