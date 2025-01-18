using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRadiusShow : MonoBehaviour
{
     [SerializeField] public SpriteRenderer radiusSprite; // Спрайт для отображения радиуса
        public float attackRadius = 5f; // Радиус атаки

        private void Update()
        {
            // Обновляем размер спрайта в зависимости от радиуса атаки
            radiusSprite.transform.localScale = new Vector3(attackRadius * 2, attackRadius * 2, 1);
        }
}
