using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    public static BulletPool Instance;

    public GameObject bulletPrefab;
    [SerializeField] private int poolSize = 20;
    private Queue<GameObject> bulletPool = new Queue<GameObject>(); 

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
        
        for (int i = 0; i < poolSize; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab);
            bullet.SetActive(false); 
            bulletPool.Enqueue(bullet);
        }
    }

    public GameObject GetBullet(Vector3 position, Quaternion rotation)
    {
        if (bulletPool.Count > 0)
        {
            GameObject bullet = bulletPool.Dequeue(); 
            bullet.SetActive(true); 
            bullet.transform.position = position;
            bullet.transform.rotation = rotation;
            return bullet;
        }
        else
        {
            GameObject bullet = Instantiate(bulletPrefab, position, rotation);
            return bullet;
        }
    }

    public void ReturnBullet(GameObject bullet)
    {
        bullet.SetActive(false); 
        bulletPool.Enqueue(bullet);
    }
}
