using System;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    [Header("Spawn Management")]
    [SerializeField] private int staticObstacleSpawnRate = 5;
    [SerializeField] private int movableObstacleSpawnRate = 10;
    [SerializeField] private int coinSpawnRate = 5;
    public ObstacleSpawn[,] spawnPointsMatrizA = new ObstacleSpawn[9,3];
    public ObstacleSpawn[,] spawnPointsMatrizB = new ObstacleSpawn[9,3];
    private bool matrizFlowControl = true;

    [Header("Obstacles")]
    [SerializeField] private GameObject[] staticObstacles;
    [SerializeField] private GameObject[] movableObstacles;

    [Header("Coins")]
    [SerializeField] private GameObject[] coinStacks;

    [Header("References")]
    private LevelManager levelManager;

    void Start()
    {
        GameController.gameController.obstacleManager = this;
        levelManager = GameController.gameController.levelManager;        
    }

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            //spawnPointsMatrizA = levelManager.currentPrefab
            for (int i = 0; i < spawnPointsMatrizA.GetLength(0); i++)
            {
                for (int j = 0; j < spawnPointsMatrizA.GetLength(1); j++)
                {
                    Debug.Log("Elemento [" + i + "," + j + "] = " + spawnPointsMatrizA[i, j]);
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Y))
        {
            //spawnPointsMatrizA = levelManager.currentPrefab
            for (int i = 0; i < spawnPointsMatrizB.GetLength(0); i++)
            {
                for (int j = 0; j < spawnPointsMatrizB.GetLength(1); j++)
                {
                    Debug.Log("Elemento [" + i + "," + j + "] = " + spawnPointsMatrizB[i, j]);
                }
            }
        }
    } //APENAS PARA TESTES, APAGAR DEPOIS!!!

    public void UpdateMatriz(ObstacleSpawn[,] matriz)
    {
        if (matrizFlowControl)
        {
            spawnPointsMatrizA = matriz;            
        }
        else
        {
            spawnPointsMatrizB = matriz;            
        }

        Invoke("StaticObjectsSpawnManager", .5f);

        
    }

    private void StaticObjectsSpawnManager()
    {
        
        if (matrizFlowControl)        
            StaticObjectsSpawn(spawnPointsMatrizA);        
        else
            StaticObjectsSpawn(spawnPointsMatrizB);

        matrizFlowControl = !matrizFlowControl;
    }

    private void StaticObjectsSpawn(ObstacleSpawn[,] matriz)
    {
        System.Random rb = new System.Random();
        int lane = 0, row = 0;
        
        for (int i = 0; i < staticObstacleSpawnRate; i++)
        {
            row = rb.Next(0, matriz.GetLength(0));
            lane = rb.Next(0, matriz.GetLength(1));

            if (matriz[row, lane].isFreeForStaticObstacle)
            {
               
                Instantiate(staticObstacles[rb.Next(0, staticObstacles.Length)], matriz[row, lane].
                    transform.position, matriz[row, lane].transform.rotation);
                
                //Obstáculos pesados
                matriz[row, lane].isFreeForStaticObstacle = false;
                matriz[row, 0].isFreeForStaticObstacle = false;
                matriz[row, 1].isFreeForStaticObstacle = false;
                matriz[row, 2].isFreeForStaticObstacle = false;

                //Moedas
                matriz[row, lane].isFreeForCoins = false;

                //Obstáculos leves
                matriz[row, lane].isFreeForMovableObstacle = false;
            }
        }

        for (int i = 0; i < coinSpawnRate; i++)
        {
            row = rb.Next(0, matriz.GetLength(0));
            lane = rb.Next(0, matriz.GetLength(1));            

            if (matriz[row, lane].isFreeForCoins)
            {
                Instantiate(coinStacks[rb.Next(0, coinStacks.Length)], matriz[row, lane].
                    transform.position, matriz[row, lane].transform.rotation);

                matriz[row, lane].isFreeForCoins = false;
                if (row - 1 >= 0) matriz[row - 1, lane].isFreeForCoins = false;
                if (row + 1 < staticObstacles.Length) matriz[row + 1, lane].isFreeForCoins = false;
            }           

        }

        for (int i = 0; i < movableObstacleSpawnRate; i++)
        {
            row = rb.Next(0, matriz.GetLength(0));
            lane = rb.Next(0, matriz.GetLength(1));

            if (matriz[row, lane].isFreeForMovableObstacle)
            {
                Instantiate(movableObstacles[rb.Next(0, movableObstacles.Length)], matriz[row, lane].
                    transform.position, matriz[row, lane].transform.rotation);

                matriz[row, lane].isFreeForMovableObstacle = false;
                if (row - 1 >= 0) matriz[row - 1, lane].isFreeForMovableObstacle = false;
                if (row + 1 < staticObstacles.Length) matriz[row + 1, lane].isFreeForMovableObstacle = false;
            }

        }
    }
    
}
