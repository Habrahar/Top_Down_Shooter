using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Ammo_UI : MonoBehaviour
{
    [SerializeField]
    public TextMeshProUGUI ammoText;



    private void OnEnable()
    {
        Weapon_Controller.OnAmmoUpdate += UpdateAmmoUI;
    }

    private void OnDisable()
    {
        Weapon_Controller.OnAmmoUpdate -= UpdateAmmoUI;
    }

    private void UpdateAmmoUI(int magazineAmmo, int totalAmmo)
    {
        // Пример: обновляем текст UI
        ammoText.text = $"{magazineAmmo}/{totalAmmo}";
    }

}
