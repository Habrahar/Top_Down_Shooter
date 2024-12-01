using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DamagePopup_ShowUp : MonoBehaviour
{
    [SerializeField]
    public GameObject DamagePopUp_Prefab;
    public float moveSpeed = 1f;
    public float lifetime = 1f;
    public float arcAngle = 60f;            // Ширина дуги
    public float initialVerticalSpeed = 2f; // Начальная вертикальная скорость
    public float gravityForce = 5f;         // Сила гравитации
    private Transform damagePoint;
    public float fadeDelay = 0.5f;          // Задержка перед началом фейда 



    private void OnEnable()
    {
        Enemy_Controller.OnDamageTaken += Spawn;
    }

    private void OnDisable()
    {
        Enemy_Controller.OnDamageTaken -= Spawn;
    }   

    public void Spawn(float damage, Vector3 position)
    {

        Instantiate(DamagePopUp_Prefab, position, Quaternion.identity)
            .GetComponent<DamagePopup_Controller>()
            .Setup(damage, position, moveSpeed, lifetime, arcAngle, initialVerticalSpeed, gravityForce, fadeDelay);
    }


   
}
