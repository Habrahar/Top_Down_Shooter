using System.Collections;
using New.Interface;
using UnityEngine;
using System.Collections.Generic;

namespace New
{
    public class EffectHandler : MonoBehaviour, IEffectHandler
    {
        [SerializeField] private GameObject impactEffectPrefab;
        [SerializeField] private GameObject attackEffectPrefab;
        [SerializeField] private GameObject deathEffectPrefab;

        private List<GameObject> impactEffectPool = new List<GameObject>();
        private List<GameObject> attackEffectPool = new List<GameObject>();
        private List<GameObject> deathEffectPool = new List<GameObject>();

        public void PlayImpactEffect(Vector3 position, Quaternion rotation)
        {
            PlayEffectFromPool(impactEffectPrefab, position, rotation, impactEffectPool);
        }

        public void PlayAttackEffect(Vector3 position, Quaternion rotation)
        {
            PlayEffectFromPool(attackEffectPrefab, position, rotation, attackEffectPool);
        }

        public void PlayDeathEffect(Vector3 position, Quaternion rotation)
        {
            PlayEffectFromPool(deathEffectPrefab, position, rotation, deathEffectPool);
        }

        private void PlayEffectFromPool(GameObject prefab, Vector3 position, Quaternion rotation, List<GameObject> pool)
        {
            GameObject effect = GetEffectFromPool(pool);

            if (effect == null)
            {
                effect = Instantiate(prefab, position, rotation);
            }
            else
            {
                effect.transform.position = position;
                effect.transform.rotation = rotation;
                effect.SetActive(true);
            }

            // Возвращаем эффект в пул после его завершения (корутина)
            StartCoroutine(DeactivateEffectAfterDelay(effect, pool));
        }

        private GameObject GetEffectFromPool(List<GameObject> pool)
        {
            foreach (var effect in pool)
            {
                if (!effect.activeInHierarchy)
                {
                    return effect;
                }
            }

            return null;
        }

        // Корутина для деактивации эффекта и его возвращения в пул
        private IEnumerator DeactivateEffectAfterDelay(GameObject effect, List<GameObject> pool)
        {
            // Подождать, пока эффект не завершится, например, через 2 секунды (или используйте подходящий таймер/аниматор)
            yield return new WaitForSeconds(2f); // Поменяйте на нужное время, в зависимости от длительности эффекта

            effect.SetActive(false); // Деактивируем эффект
            pool.Add(effect); // Возвращаем эффект в пул
        }
    }
}
