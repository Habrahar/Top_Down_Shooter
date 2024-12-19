using New;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponConfig", menuName = "ScriptableObjects/WeaponConfig")]
public class WeaponConfig : ScriptableObject
{
    public GameObject weaponPrefab;
    public GameObject bulletPrefab;
    public string weaponName;

    public float fireRate;
    public float bulletSpeed;
    public int bulletDamage;
    public float bulletDistance;
    public float bulletSpread;

    [Header("Ammo Settings")]
    public int magazineSize = 30; 
    public int maxTotalAmmo = 90; 
    public float reloadTime = 2f; 

    [Header("Effects")]
    public GameObject hitEffect;
    public GameObject vanishEffect;

    [Header("Shooting Behaviour")]
    public ShootingBehaviourConfig shootingBehaviourConfig;
}
