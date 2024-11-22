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
    private Transform damagePoint;


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
            .Setup(damage, position, moveSpeed, lifetime);
    }

   
}
