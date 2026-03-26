using UnityEngine;

public class ObstacleSpawn : MonoBehaviour
{

    public bool isFreeForStaticObstacle;
    public bool isFreeForMovableObstacle;
    public bool isFreeForCoins;
    public int lane;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isFreeForStaticObstacle = true;
        isFreeForMovableObstacle = true;
        isFreeForCoins = true;
    }

    
}
