using System;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    [Header("Spawn Management")]
    [SerializeField] private int staticObstacleSpawnRate = 5;
    public ObstacleSpawn[,] spawnPointsMatrizA = new ObstacleSpawn[9,3];
    public ObstacleSpawn[,] spawnPointsMatrizB = new ObstacleSpawn[9,3];
    private bool matrizFlowControl = true;

    [Header("Obstacles")]
    [SerializeField] private GameObject[] staticObstacles;
    [SerializeField] private GameObject[] movableObstacles;

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
    }

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

            if (matriz[row, lane].isFree)
            {
                //Debug.Log("Spawnei obstáculo fixo na [lane, row]: [" + lane + ", " + row + "]");

                Instantiate(staticObstacles[rb.Next(0, staticObstacles.Length)], matriz[row, lane].
                    transform.position, matriz[row, lane].transform.rotation);
                
                matriz[row, lane].isFree = false;
                matriz[row, 0].isFree = false;
                matriz[row, 1].isFree = false;
                matriz[row, 2].isFree = false;
            }
            
        }
    }
    
}
