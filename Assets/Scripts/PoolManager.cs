using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public static PoolManager poolManager;

    [Header("Bullet")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform bulletParent;
    [SerializeField] private int initialSize = 10;

    [Header("Pools")]
    [SerializeField] private List<GameObject> bulletPool = new List<GameObject>();

    private void Awake()
    {
        poolManager = this;
        
        for (int i = 0; i < initialSize; i++)
        {
            CreateObject();
        }
    }

    private GameObject CreateObject()
    {
        GameObject obj = Instantiate(bulletPrefab, bulletParent);
        obj.SetActive(false);
        bulletPool.Add(obj);
        return obj;
    }

    public GameObject GetBulletPrefab()
    {
        foreach (GameObject obj in bulletPool)
        {
            if (!obj.activeSelf)
            {
                obj.SetActive(true);
                return obj;
            }            
        }
        return CreateObject();
    }
}
