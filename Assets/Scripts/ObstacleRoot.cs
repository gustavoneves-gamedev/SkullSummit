using UnityEngine;

public class ObstacleRoot : MonoBehaviour
{
    [Header("Main")]
    [SerializeField] private GameObject obstacle;    
    [SerializeField] private float damage;
    [SerializeField] private float movementSpeed = 10f;
    [SerializeField] private int obsctacleType = 0;// 1 - Obstáculo móvel grande
                                                   // 2 - Obstáculo móvel pequeno

    [Header("Items")]
    [SerializeField] private GameObject[] items;
    [SerializeField] private GameObject[] obstacleGlow;
    [SerializeField] private Transform itemSpawnTransform;
    private GameObject itemToSpawn;
    private GameObject itemSpawned;
    private float itemSpawnCode;

    private float rotateSpeedA;
    private float rotateSpeedB;
    private float rotateSpeedC;
    private PlayerRoot player;
    //private Collider collider;


    void Start()
    {
        Destroy(gameObject, 30f);
        player = GameController.gameController.playerRoot; 

        rotateSpeedA = Random.Range(10f, 20f);
        rotateSpeedB = Random.Range(10f, 20f);
        rotateSpeedC = Random.Range(25f, 50f);
        

        if (obsctacleType == 1)
        {
            transform.position += Vector3.up * 3.2f;
            itemToSpawn = ItemToSpawn();
        }
        if (obsctacleType == 2)
        {
            transform.position += Vector3.up * 1.2f;
            itemToSpawn = ItemToSpawn();
        }
    }

    
    void Update()
    {
        if (obsctacleType == 1 || obsctacleType == 2)
        {
            transform.position += Vector3.back * movementSpeed * Time.deltaTime;

            obstacle.transform.Rotate(Vector3.right, rotateSpeedA * Time.deltaTime);
            obstacle.transform.Rotate(Vector3.up, rotateSpeedB * Time.deltaTime);
            obstacle.transform.Rotate(Vector3.forward, rotateSpeedC * Time.deltaTime);

        }

        if (GameController.gameController.isRunning) return;
        Destroy(gameObject, 1f);
    }

    private GameObject ItemToSpawn()
    {
        itemSpawnCode = Random.Range(1, 11);

        if (itemSpawnCode >= 1 && itemSpawnCode < 3)
        {
            obstacleGlow[0].SetActive(true);
            return items[0];
        }
        else if (itemSpawnCode == 3)
        {
            obstacleGlow[1].SetActive(true);
            return items[1];
        }
        else if (itemSpawnCode == 4)
        {
            obstacleGlow[2].SetActive(true);
            return items[2];
        }
        else return null;
    }

    private void DisableGameObjects()
    {
        obstacle.SetActive(false);

        if (itemSpawnCode < 5 && itemToSpawn != null)
        {
            for (int i = 0; i < obstacleGlow.Length; i++)
            {
                obstacleGlow[i].SetActive(false);
            }
        }
    }

    public void ApplyDamage()
    {
        player.UpdateStamina(-damage);        
        Destroy(gameObject, 10f);
        DisableGameObjects();
    }

    public void WasShot(GameObject bullet)
    {
        DisableGameObjects();

        if (itemSpawnCode < 5 && itemToSpawn != null)
        {
            itemSpawned = Instantiate(itemToSpawn, 
                itemSpawnTransform.position, itemSpawnTransform.rotation);

            Destroy(itemSpawned, 5f);
        }
        

        Destroy(bullet);
        Destroy(gameObject, 10f);
        GameController.gameController.ObstaclesDestroyedCounter();
    }


}
