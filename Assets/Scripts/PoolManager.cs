using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public static PoolManager poolManager;

    
    [Header("Bullet")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform bulletParent;
    [SerializeField] private int initialBulletPoolSize = 10;

    [Header("Coin Stack")]
    [SerializeField] private GameObject coinStackPrefab;
    [SerializeField] private Transform coinStackParent;
    [SerializeField] private int initialCoinStackPoolSize = 10;

    [Header("Cowboy Obstacles")]
    [SerializeField] private GameObject[] cowboyStaticObstaclesPrefabs;
    [SerializeField] private GameObject[] cowboyBigMovableObstaclesPrefabs;
    [SerializeField] private GameObject[] cowboySmallMovableObstaclesPrefabs;
    [SerializeField] private Transform[] cowboyObstaclesParent;
    [SerializeField] private int initialObstaclesPoolSize = 10;

    [Header("Pools")]
    [SerializeField] private List<GameObject> bulletPool = new List<GameObject>();
    [SerializeField] private List<GameObject> coinStackPool = new List<GameObject>();

    [Header("CowboyPools")]
    [SerializeField] private List<GameObject> cowboyStaticObstaclesPool = new List<GameObject>();   
    [SerializeField] private List<GameObject> cowboyMovableBigObstaclesPool = new List<GameObject>();
    [SerializeField] private List<GameObject> cowboyMovableSmallObstaclesPool = new List<GameObject>();



    private void Awake()
    {
        poolManager = this;

        for (int i = 0; i < initialBulletPoolSize; i++)
        {
            CreateBulletObject();
        }

        for (int i = 0; i < initialCoinStackPoolSize; i++)
        {
            CreateCoinStack();
        }

        for (int i = 0; i < initialObstaclesPoolSize; i++)
        {
            CreateObstacles(0);
            CreateObstacles(1);
            CreateObstacles(2);            
        }

    }

    #region Create Objects
    private GameObject CreateBulletObject()
    {
        GameObject obj = Instantiate(bulletPrefab, bulletParent);
        obj.SetActive(false);
        bulletPool.Add(obj);
        return obj;
    }

    private GameObject CreateObstacles(int obstacleCode = 0)
    {
        int x = 0;

        if (obstacleCode == 0)
        {
            x = Random.Range(0, cowboyStaticObstaclesPrefabs.Length);

            GameObject obj = Instantiate(cowboyStaticObstaclesPrefabs[x], 
                cowboyObstaclesParent[obstacleCode]);

            obj.SetActive(false);
            cowboyStaticObstaclesPool.Add(obj);

            return obj;
        }
        else if (obstacleCode == 1)
        {
            x = Random.Range(0, cowboyBigMovableObstaclesPrefabs.Length);

            GameObject obj = Instantiate(cowboyBigMovableObstaclesPrefabs[x],
                cowboyObstaclesParent[obstacleCode]);

            obj.SetActive(false);
            cowboyMovableBigObstaclesPool.Add(obj);

            return obj;
        }
        else
        {
            x = Random.Range(0, cowboySmallMovableObstaclesPrefabs.Length);

            GameObject obj = Instantiate(cowboySmallMovableObstaclesPrefabs[x],
                cowboyObstaclesParent[obstacleCode]);

            obj.SetActive(false);
            cowboyMovableSmallObstaclesPool.Add(obj);

            return obj;
        }
    }

    private GameObject CreateCoinStack()
    {
        GameObject obj = Instantiate(coinStackPrefab, coinStackParent);
        obj.SetActive(false);
        coinStackPool.Add(obj);
        return obj;
    }

    #endregion

    #region Get Objects
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
        return CreateBulletObject();
    }

    public GameObject GetCoinStackPrefab()
    {
        foreach (GameObject obj in coinStackPool)
        {
            if (!obj.activeSelf)
            {
                obj.SetActive(true);
                return obj;
            }
        }
        return CreateBulletObject();
    }

    public GameObject GetObstaclePrefab(int obstacleCode = 0)
    {

        if (obstacleCode == 0)
        {

            GameObject obstacle = cowboyStaticObstaclesPool[Random.Range(0, cowboyStaticObstaclesPool.Count)];

            if (obstacle.activeSelf)
            {
                foreach (GameObject obj in cowboyStaticObstaclesPool)
                {
                    if (!obj.activeSelf)
                    {
                        //obj.SetActive(true);
                        return obj;
                    }
                }
            }
            else
            {
                //obstacle.SetActive(true);
                return obstacle;
            }

        }
        else if (obstacleCode == 1)
        {
            GameObject obstacle = cowboyMovableBigObstaclesPool[Random.Range(0, cowboyMovableBigObstaclesPool.Count)];

            if (obstacle.activeSelf)
            {
                foreach (GameObject obj in cowboyMovableBigObstaclesPool)
                {
                    if (!obj.activeSelf)
                    {
                        //obj.SetActive(true);
                        return obj;
                    }
                }
            }
            else
            {
                //obstacle.SetActive(true);
                return obstacle;
            }
        }
        else
        {
            GameObject obstacle = cowboyMovableSmallObstaclesPool[Random.Range(0, cowboyMovableSmallObstaclesPool.Count)];

            if (obstacle.activeSelf)
            {
                foreach (GameObject obj in cowboyMovableSmallObstaclesPool)
                {
                    if (!obj.activeSelf)
                    {
                        //obj.SetActive(true);
                        return obj;
                    }
                }
            }
            else
            {
                //obstacle.SetActive(true);
                return obstacle;
            }
        }
        return CreateObstacles(obstacleCode);
    }

    #endregion

}
