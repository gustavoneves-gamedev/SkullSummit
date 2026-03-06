using UnityEngine;

public class LevelRoot : MonoBehaviour
{
    
    public Transform levelPrefabSpawnPoint;
    [SerializeField] private ObstacleSpawn[] obstaclesSpawnPoints;
    public ObstacleSpawn[,] obstaclesSpawnPointsMatriz = new ObstacleSpawn[9,3];

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SortingMatriz();
    }    

    private void SortingMatriz()
    {
       if (obstaclesSpawnPoints.Length <= 0) return;

        int positionLaneA = 0;
        int positionLaneB = 0;
        int positionLaneC = 0;

        for (int i = 0; i < obstaclesSpawnPoints.Length; i++)
        {
            if (obstaclesSpawnPoints[i].lane == 0)
            {
                obstaclesSpawnPointsMatriz[positionLaneA, 0] = obstaclesSpawnPoints[i];
                positionLaneA++;
            }
            else if (obstaclesSpawnPoints[i].lane == 1)
            {
                obstaclesSpawnPointsMatriz[positionLaneB, 1] = obstaclesSpawnPoints[i];
                positionLaneB++;
            }
            else if (obstaclesSpawnPoints[i].lane == 2)
            {
                obstaclesSpawnPointsMatriz[positionLaneC, 2] = obstaclesSpawnPoints[i];
                positionLaneC++;
            }
        }

        for (int i = 0; i < obstaclesSpawnPointsMatriz.GetLength(0); i++)
        {
            for (int j = 0; j < obstaclesSpawnPointsMatriz.GetLength(1); j++)
            {
                Debug.Log("Elemento [" + i + "," + j + "] = " + obstaclesSpawnPointsMatriz[i,j]);
            }
        }

    }
}
