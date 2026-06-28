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

    [Header("Samurai Obstacles")]
    [SerializeField] private GameObject[] samuraiStaticObstaclesPrefabs;
    [SerializeField] private GameObject[] samuraiBigMovableObstaclesPrefabs;
    [SerializeField] private GameObject[] samuraiSmallMovableObstaclesPrefabs;
    [SerializeField] private Transform[] samuraiObstaclesParent;

    [Header("Dullahan Obstacles")]
    [SerializeField] private GameObject[] dullahanStaticObstaclesPrefabs;
    [SerializeField] private GameObject[] dullahanBigMovableObstaclesPrefabs;
    [SerializeField] private GameObject[] dullahanSmallMovableObstaclesPrefabs;
    [SerializeField] private Transform[] dullahanObstaclesParent;

    [Header("Bullets")]
    [SerializeField] private List<GameObject> cowboyBulletPool = new List<GameObject>();
    [SerializeField] private List<GameObject> samuraiBulletPool = new List<GameObject>();
    [SerializeField] private List<GameObject> dullahanBulletPool = new List<GameObject>();
    [SerializeField] private List<GameObject> coinStackPool = new List<GameObject>();

    [Header("CowboyPools")]
    [SerializeField] private List<GameObject> cowboyStaticObstaclesPool = new List<GameObject>();   
    [SerializeField] private List<GameObject> cowboyMovableBigObstaclesPool = new List<GameObject>();
    [SerializeField] private List<GameObject> cowboyMovableSmallObstaclesPool = new List<GameObject>();

    [Header("SamuraiPools")]
    [SerializeField] private List<GameObject> samuraiStaticObstaclesPool = new List<GameObject>();
    [SerializeField] private List<GameObject> samuraiMovableBigObstaclesPool = new List<GameObject>();
    [SerializeField] private List<GameObject> samuraiMovableSmallObstaclesPool = new List<GameObject>();

    [Header("DullahanPools")]
    [SerializeField] private List<GameObject> dullahanStaticObstaclesPool = new List<GameObject>();
    [SerializeField] private List<GameObject> dullahanMovableBigObstaclesPool = new List<GameObject>();
    [SerializeField] private List<GameObject> dullahanMovableSmallObstaclesPool = new List<GameObject>();



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
            CreateCowboyObstacles(0);
            CreateCowboyObstacles(1);
            CreateCowboyObstacles(2);

            CreateSamuraiObstacles(0);
            CreateSamuraiObstacles(1);
            CreateSamuraiObstacles(2);

            CreateDullahanObstacles(0);
            CreateDullahanObstacles(1);
            CreateDullahanObstacles(2);
        }

        for (int i = 0; i < initialObstaclesPoolSize; i++)
        {
            CreateCowboyObstacles(0);
            CreateSamuraiObstacles(0);
            CreateDullahanObstacles(0);
        }
    }

    #region Create Bullets
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

    #endregion

    #region Obstacles & Coin Stack

    private GameObject CreateCowboyObstacles(int obstacleCode = 0)
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

    private GameObject CreateSamuraiObstacles(int obstacleCode = 0)
    {
        int x = 0;

        if (obstacleCode == 0)
        {
            x = Random.Range(0, samuraiStaticObstaclesPrefabs.Length);

            GameObject obj = Instantiate(samuraiStaticObstaclesPrefabs[x],
                samuraiObstaclesParent[obstacleCode]);

            obj.SetActive(false);
            samuraiStaticObstaclesPool.Add(obj);

            return obj;
        }
        else if (obstacleCode == 1)
        {
            x = Random.Range(0, samuraiBigMovableObstaclesPrefabs.Length);

            GameObject obj = Instantiate(samuraiBigMovableObstaclesPrefabs[x],
                samuraiObstaclesParent[obstacleCode]);

            obj.SetActive(false);
            samuraiMovableBigObstaclesPool.Add(obj);

            return obj;
        }
        else
        {
            x = Random.Range(0, samuraiSmallMovableObstaclesPrefabs.Length);

            GameObject obj = Instantiate(samuraiSmallMovableObstaclesPrefabs[x],
                samuraiObstaclesParent[obstacleCode]);

            obj.SetActive(false);
            samuraiMovableSmallObstaclesPool.Add(obj);

            return obj;
        }
    }

    private GameObject CreateDullahanObstacles(int obstacleCode = 0)
    {
        int x = 0;

        if (obstacleCode == 0)
        {
            x = Random.Range(0, dullahanStaticObstaclesPrefabs.Length);

            GameObject obj = Instantiate(dullahanStaticObstaclesPrefabs[x],
                dullahanObstaclesParent[obstacleCode]);

            obj.SetActive(false);
            dullahanStaticObstaclesPool.Add(obj);

            return obj;
        }
        else if (obstacleCode == 1)
        {
            x = Random.Range(0, dullahanBigMovableObstaclesPrefabs.Length);

            GameObject obj = Instantiate(dullahanBigMovableObstaclesPrefabs[x],
                dullahanObstaclesParent[obstacleCode]);

            obj.SetActive(false);
            dullahanMovableBigObstaclesPool.Add(obj);

            return obj;
        }
        else
        {
            x = Random.Range(0, dullahanSmallMovableObstaclesPrefabs.Length);

            GameObject obj = Instantiate(dullahanSmallMovableObstaclesPrefabs[x],
                dullahanObstaclesParent[obstacleCode]);

            obj.SetActive(false);
            dullahanMovableSmallObstaclesPool.Add(obj);

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

    #region Bullets & Coin
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

    #endregion

    #region Obstacles
    public GameObject GetCowboyObstaclePrefab(int obstacleCode = 0)
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
        return CreateCowboyObstacles(obstacleCode);
    }

    public GameObject GetSamuraiObstaclePrefab(int obstacleCode = 0)
    {

        if (obstacleCode == 0)
        {

            GameObject obstacle = samuraiStaticObstaclesPool[Random.Range(0, samuraiStaticObstaclesPool.Count)];

            if (obstacle.activeSelf)
            {
                foreach (GameObject obj in samuraiStaticObstaclesPool)
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
            GameObject obstacle = samuraiMovableBigObstaclesPool[Random.Range(0, samuraiMovableBigObstaclesPool.Count)];

            if (obstacle.activeSelf)
            {
                foreach (GameObject obj in samuraiMovableBigObstaclesPool)
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
            GameObject obstacle = samuraiMovableSmallObstaclesPool[Random.Range(0, samuraiMovableSmallObstaclesPool.Count)];

            if (obstacle.activeSelf)
            {
                foreach (GameObject obj in samuraiMovableSmallObstaclesPool)
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
        return CreateSamuraiObstacles(obstacleCode);
    }

    public GameObject GetDullahanObstaclePrefab(int obstacleCode = 0)
    {

        if (obstacleCode == 0)
        {

            GameObject obstacle = dullahanStaticObstaclesPool[Random.Range(0, dullahanStaticObstaclesPool.Count)];

            if (obstacle.activeSelf)
            {
                foreach (GameObject obj in dullahanStaticObstaclesPool)
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
            GameObject obstacle = dullahanMovableBigObstaclesPool[Random.Range(0, dullahanMovableBigObstaclesPool.Count)];

            if (obstacle.activeSelf)
            {
                foreach (GameObject obj in dullahanMovableBigObstaclesPool)
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
            GameObject obstacle = dullahanMovableSmallObstaclesPool[Random.Range(0, dullahanMovableSmallObstaclesPool.Count)];

            if (obstacle.activeSelf)
            {
                foreach (GameObject obj in dullahanMovableSmallObstaclesPool)
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
        return CreateDullahanObstacles(obstacleCode);
    }


    #endregion

    #endregion

}
