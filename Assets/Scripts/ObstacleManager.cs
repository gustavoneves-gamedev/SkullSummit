using System;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    private Transform[] spawnPositions;
    public ObstacleSpawn[,] spawnPointsMatrizA = new ObstacleSpawn[9,3];
    public ObstacleSpawn[,] spawnPointsMatrizB = new ObstacleSpawn[9,3];
    private bool matrizFlowControl = true;
    private LevelManager levelManager;
    [SerializeField] private GameObject[] staticObstacles;
    [SerializeField] private GameObject[] movableObstacles;


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
            StaticObjectsSpawn(spawnPointsMatrizA);
        }
        else
        {
            spawnPointsMatrizB = matriz;
            StaticObjectsSpawn(spawnPointsMatrizB);
        }

        matrizFlowControl = !matrizFlowControl;
    }

    private void StaticObjectsSpawn(ObstacleSpawn[,] matriz)
    {
        System.Random rb = new System.Random();
        int lane = 0, row = 0;
        
        for (int i = 0; i < matriz.GetLength(0); i++)
        {
            row = rb.Next(0, matriz.GetLength(0));
            lane = rb.Next(0, matriz.GetLength(1));

            if (matriz[row, lane].isFree)
            {
                Debug.Log("Spawnei obstáculo fixo na [lane, row]: [" + lane + ", " + row + "]");
                matriz[row, lane].isFree = false;
                matriz[row, 0].isFree = false;
                matriz[row, 1].isFree = false;
                matriz[row, 2].isFree = false;
            }
        }
    }
    
}
