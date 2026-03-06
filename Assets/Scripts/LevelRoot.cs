using UnityEngine;

public class LevelRoot : MonoBehaviour
{
    
    public Transform levelPrefabSpawnPoint;
    [SerializeField] private ObstacleSpawn[] obstaclesSpawnPoints;
    public ObstacleSpawn[,] obstaclesSpawnPointsMatriz = new ObstacleSpawn[8,3];

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        Debug.Log("" + obstaclesSpawnPointsMatriz[0, 0]);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
