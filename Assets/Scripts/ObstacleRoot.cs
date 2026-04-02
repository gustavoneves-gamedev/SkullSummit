using UnityEngine;

public class ObstacleRoot : MonoBehaviour
{
    [SerializeField] private GameObject obstacle;
    [SerializeField] private float damage;
    [SerializeField] private float movementSpeed = 10f;
    [SerializeField] private int obsctacleType = 0;

    [Header("Items")]
    [SerializeField] private GameObject[] items;
    private float itemSpawnCode;


    private float rotateSpeedA;
    private float rotateSpeedB;
    private float rotateSpeedC;
    private PlayerRoot player;
    //private Collider collider;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(gameObject, 30f);
        player = GameController.gameController.playerRoot;
        
        ItemToSpawn();

        rotateSpeedA = Random.Range(10f, 20f);
        rotateSpeedB = Random.Range(10f, 20f);
        rotateSpeedC = Random.Range(25f, 50f);
        

        if (obsctacleType == 1)
        {
            transform.position += Vector3.up * 3.2f;
        }
        if (obsctacleType == 2)
        {
            transform.position += Vector3.up * 1.2f;
        }
    }

    // Update is called once per frame
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

        if (itemSpawnCode >= 1 && itemSpawnCode < 3) return items[0];
        else if (itemSpawnCode == 3) return items[1];
        else if (itemSpawnCode == 4) return items[2];
        else return null;
    }

    public void ApplyDamage()
    {
        player.UpdateStamina(-damage);        
        Destroy(gameObject, 10f);
        obstacle.SetActive(false);
    }

    public void WasShot(GameObject bullet)
    {
        obstacle.SetActive(false);

        if (itemSpawnCode < 5 && ItemToSpawn() != null)
        {
            Instantiate(ItemToSpawn(), transform.position, transform.rotation);
        }
        

        Destroy(bullet);
        Destroy(gameObject, 10f);
        GameController.gameController.ObstaclesDestroyedCounter();
    }


}
