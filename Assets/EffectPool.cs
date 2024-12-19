using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectPool : MonoBehaviour
{
    public static EffectPool Instance;

    private Dictionary<string, Queue<GameObject>> effectPools = new Dictionary<string, Queue<GameObject>>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public GameObject GetEffect(GameObject effectPrefab, Vector3 position, Quaternion rotation)
    {
        string key = effectPrefab.name;

        // Если пула для эффекта нет, создаём его
        if (!effectPools.ContainsKey(key))
        {
            effectPools[key] = new Queue<GameObject>();
        }

        var pool = effectPools[key];

        GameObject effect;
        if (pool.Count > 0)
        {
            effect = pool.Dequeue();
        }
        else
        {
            effect = Instantiate(effectPrefab);
        }

        effect.transform.position = position;
        effect.transform.rotation = rotation;
        effect.SetActive(true);

        return effect;
    }

    public void ReturnEffect(GameObject effect)
    {
        string key = effect.name.Replace("(Clone)", "").Trim();

        if (!effectPools.ContainsKey(key))
        {
            effectPools[key] = new Queue<GameObject>();
        }

        effect.SetActive(false);
        effectPools[key].Enqueue(effect);
    }
}
