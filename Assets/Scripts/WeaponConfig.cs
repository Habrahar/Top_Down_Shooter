using UnityEngine;

[CreateAssetMenu(fileName = "WeaponConfig", menuName = "ScriptableObjects/WeaponConfig")]
public class WeaponConfig : ScriptableObject
{
    public GameObject weaponPrefab;
    public GameObject bulletPrefab;

    public float fireRate;
    public float bulletSpeed;
    public int bulletDamage;
    public float bulletDistance;

    [Header("Ammo Settings")]
    public int magazineSize = 30; // Размер магазина
    public int maxTotalAmmo = 90; // Общее количество патронов
    public float reloadTime = 2f; // Время перезарядки
}
