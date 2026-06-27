using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public static PoolManager poolManager;

    
    [Header("Bullet")]    
    [SerializeField] private GameObject cowboyBulletPrefab;
    [SerializeField] private GameObject samuraiBulletPrefab;
    [SerializeField] private GameObject dullahanBulletPrefab;
    [SerializeField] private Transform cowboyBulletParent;
    [SerializeField] private Transform samuraiBulletParent;
    [SerializeField] private Transform dullahanBulletParent;
    [SerializeField] private int initialBulletPoolSize = 10;

    [Header("Coin Stack")]
    [SerializeField] private GameObject coinStackPrefab;
    [SerializeField] private Transform coinStackParent;
    [SerializeField] private int initialCoinStackPoolSize = 20;

    [Header("Cowboy Obstacles")]
    [SerializeField] private GameObject[] cowboyStaticObstaclesPrefabs;
    [SerializeField] private GameObject[] cowboyBigMovableObstaclesPrefabs;
    [SerializeField] private GameObject[] cowboySmallMovableObstaclesPrefabs;
    [SerializeField] private Transform[] cowboyObstaclesParent;
    [SerializeField] private int initialObstaclesPoolSize = 10;

    [Header("Bullets")]
    [SerializeField] private List<GameObject> cowboyBulletPool = new List<GameObject>();
    [SerializeField] private List<GameObject> samuraiBulletPool = new List<GameObject>();
    [SerializeField] private List<GameObject> dullahanBulletPool = new List<GameObject>();
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
            CreateCowboyBulletObject();
            CreateSamuraiBulletObject();
            CreateDullahanBulletObject();
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

        for (int i = 0; i < initialObstaclesPoolSize; i++)
        {
            CreateObstacles(0);
        }
    }

    #region Create Objects
    private GameObject CreateCowboyBulletObject()
    {
        GameObject obj = Instantiate(cowboyBulletPrefab, cowboyBulletParent);
        obj.SetActive(false);
        cowboyBulletPool.Add(obj);
        return obj;
    }

    private GameObject CreateSamuraiBulletObject()
    {
        GameObject obj = Instantiate(samuraiBulletPrefab, samuraiBulletParent);
        obj.SetActive(false);
        samuraiBulletPool.Add(obj);
        return obj;
    }

    private GameObject CreateDullahanBulletObject()
    {
        GameObject obj = Instantiate(dullahanBulletPrefab, dullahanBulletParent);
        obj.SetActive(false);
        dullahanBulletPool.Add(obj);
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
    public GameObject GetCowboyBulletPrefab()
    {
        foreach (GameObject obj in cowboyBulletPool)
        {
            if (!obj.activeSelf)
            {
                obj.SetActive(true);
                return obj;
            }
        }
        return CreateCowboyBulletObject();
    }

    public GameObject GetSamuraiBulletPrefab()
    {
        foreach (GameObject obj in samuraiBulletPool)
        {
            if (!obj.activeSelf)
            {
                obj.SetActive(true);
                return obj;
            }
        }
        return CreateSamuraiBulletObject();
    }

    public GameObject GetDullahanBulletPrefab()
    {
        foreach (GameObject obj in dullahanBulletPool)
        {
            if (!obj.activeSelf)
            {
                obj.SetActive(true);
                return obj;
            }
        }
        return CreateDullahanBulletObject();
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
        return CreateCoinStack();
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
