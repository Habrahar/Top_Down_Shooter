using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoBox : MonoBehaviour
{
    public int ammoAmount = 30; // Количество патронов, которое дает ящик

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("AmmoAdd");
        if (other.CompareTag("Player"))
        {
            Weapon_Controller weapon = other.GetComponentInChildren<Weapon_Controller>();
            if (weapon != null)
            {
                weapon.AddAmmo(ammoAmount); // Увеличиваем общее количество патронов
                Destroy(gameObject); // Удаляем ящик
            }
        }
    }
}
