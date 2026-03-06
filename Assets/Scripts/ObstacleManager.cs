using System;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    private Transform[] spawnPositions;
    public ObstacleSpawn[,] spawnPointsMatrizA = new ObstacleSpawn[9,3];
    public ObstacleSpawn[,] spawnPointsMatrizB = new ObstacleSpawn[9,3];
    private LevelManager levelManager;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameController.gameController.obstacleManager = this;
        levelManager = GameController.gameController.levelManager;

        Initialize();
    }

    public void Initialize()
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
