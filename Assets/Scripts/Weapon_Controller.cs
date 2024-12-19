using System;
using New;
using UI;
using UnityEngine;
using Random = UnityEngine.Random;

public class Weapon_Controller : MonoBehaviour
{
    [SerializeField] private WeaponConfig weaponConfig; // Конфигурация оружия
    private GameObject currentWeaponInstance; // Текущий экземпляр оружия
    public Transform firePoint;
    private Transform ejectPoint;
    private Player_shooting player;
    private PlayerController player_controller;
    private IShootingBehaviour shootingBehaviour;

    private float nextFireTime;
    private int currentMagazineAmmo; // Текущий боезапас в магазине
    private int totalAmmo; // Общее количество патронов
    private bool isReloading = false;

    public static event Action<int, int> OnAmmoUpdate; // Событие обновления патронов

    private void OnEnable()
    {
        Player_shooting.OnShoot += HandleShoot;
        WeaponSelectionWindow.ApplyWeapon += InitializeWeapon;
    }

    private void OnDisable()
    {
        Player_shooting.OnShoot -= HandleShoot;
        WeaponSelectionWindow.ApplyWeapon -= InitializeWeapon;
    }
    

    public void InitializeWeapon(WeaponConfig config)
    {
        if (config != null)
        {
            Destroy(currentWeaponInstance);
        }

        weaponConfig = config;
        currentWeaponInstance = Instantiate(config.weaponPrefab, transform);
        InitializeAmmo(config);
        firePoint = currentWeaponInstance.transform.Find("FirePoint");
        shootingBehaviour = config.shootingBehaviourConfig.GetShootingBehaviour(); // Инициализация поведения стрельбы
        if (player == null)
        {
            player = FindObjectOfType<Player_shooting>();
        }
    }


    private void InitializeAmmo(WeaponConfig config)
    {
        totalAmmo = config.maxTotalAmmo; // Устанавливаем общее количество патронов
        currentMagazineAmmo = config.magazineSize; // Полный магазин
        BulletPool.Instance.InitializePool(config.bulletPrefab, 20);
        OnAmmoUpdate?.Invoke(currentMagazineAmmo, totalAmmo); // Обновляем UI
    }

    private void Update()
    {
        if (player == null || player.currentTarget == null) return;

        if (player.CanShoot)
        {
            Debug.Log("Shooting");
            HandleShoot(player.currentTarget.position);
        }
    }

    private void HandleShoot(Vector3 targetPosition)
    {
        if (isReloading || Time.time < nextFireTime || firePoint == null || currentMagazineAmmo <= 0) return;

        nextFireTime = Time.time + weaponConfig.fireRate;

        Vector3 direction = (targetPosition - firePoint.position).normalized;
        shootingBehaviour.Shoot(firePoint, direction, weaponConfig);
        
        EffectPool.Instance.GetEffect(weaponConfig.fireEffect, firePoint.position, Quaternion.identity);
        
        currentMagazineAmmo--;
        OnAmmoUpdate?.Invoke(currentMagazineAmmo, totalAmmo);

        if (currentMagazineAmmo <= 0)
        {
            TryReload();
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
    
    public void AddAmmo(int amount)
    {
        totalAmmo += amount;
        OnAmmoUpdate?.Invoke(currentMagazineAmmo, totalAmmo); // Обновляем UI
    }
    
    

    private void ChangeWeapon(WeaponConfig weapon)
    {
        weaponConfig = weapon;
        InitializeWeapon(weapon);
    }



}
