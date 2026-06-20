using UnityEngine;

public class ObstacleRoot : MonoBehaviour
{
    [Header("Main")]
    [SerializeField] private GameObject[] obstacles;
    [SerializeField] private float damage;
    [SerializeField] private float movementSpeed = 10f;
    private float defaultMovementSpeed = 0;
    public int obsctacleType = 0;// 1 - Obstáculo móvel grande
                                 // 2 - Obstáculo móvel pequeno

    [Header("Items")]
    [SerializeField] private GameObject[] items; //0 - Stamina; 1 - Shield; 2 - CoinMultiplier
    [SerializeField] private GameObject[] obstacleGlow;
    [SerializeField] private Transform itemSpawnTransform;
    [SerializeField] private GameObject itemToSpawn;
    private GameObject itemSpawned;
    [SerializeField] private float itemSpawnCode;
    [SerializeField] private int staminaSpawnUpperRate = 6;
    [SerializeField] private int shieldSpawnUpperRate = 10;
    [SerializeField] private int coinSpawnUpperRate = 18;

    [Header("Pool Conexion")]
    private float timer;
    private float lifetime = 50f;

    [Header("Collision")]
    [SerializeField] private ParticleSystem destroyVFX;
    [SerializeField] private GameObject collisionDetector;

    private float rotateSpeedA;
    private float rotateSpeedB;
    private float rotateSpeedC;
    private PlayerRoot player;
    //private Collider collider;

    private void OnEnable()
    {
        //Destroy(gameObject, 30f);
        if (GameController.gameController == null) return;

        if (GameController.gameController.isRunning == false) return;

        player = GameController.gameController.playerRoot;

        if(defaultMovementSpeed != 0) movementSpeed = defaultMovementSpeed;
        if(collisionDetector != null) collisionDetector.SetActive(true);

        rotateSpeedA = Random.Range(10f, 20f);
        rotateSpeedB = Random.Range(10f, 20f);
        rotateSpeedC = Random.Range(25f, 50f);

        if (GameController.gameController.playerRoot.heightClimbed >= 1500)
        {
            staminaSpawnUpperRate--;
            shieldSpawnUpperRate--;
            coinSpawnUpperRate--;
        }
        else if (GameController.gameController.playerRoot.heightClimbed >= 4000)
        {
            staminaSpawnUpperRate -= 2;
            shieldSpawnUpperRate -= 2;
            coinSpawnUpperRate -= 2;
        }
        else if (GameController.gameController.playerRoot.heightClimbed >= 10000)
        {
            staminaSpawnUpperRate -= 4;
            shieldSpawnUpperRate -= 4;
            coinSpawnUpperRate -= 4;
        }

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

        EnableGameObjects();
    }  

    void Update()
    {
        if (obsctacleType == 1 || obsctacleType == 2)
        {
            if (obstacles.Length < 1) return;

            transform.position += Vector3.back * movementSpeed * Time.deltaTime;

            obstacles[0].transform.Rotate(Vector3.right, rotateSpeedA * Time.deltaTime);
            obstacles[0].transform.Rotate(Vector3.up, rotateSpeedB * Time.deltaTime);
            obstacles[0].transform.Rotate(Vector3.forward, rotateSpeedC * Time.deltaTime);

        }

        Timer();

        if (GameController.gameController.isRunning) return;
        //Destroy(gameObject, 1f);
        Deactivate();
    }

    private GameObject ItemToSpawn()
    {
        itemSpawnCode = Random.Range(1, 31);

        if (itemSpawnCode >= 1 && itemSpawnCode < staminaSpawnUpperRate)
        {
            obstacleGlow[0].SetActive(true);
            return items[0];
        }
        else if (itemSpawnCode >= 6 && itemSpawnCode < shieldSpawnUpperRate)
        {
            obstacleGlow[1].SetActive(true);
            return items[1];
        }
        else if (itemSpawnCode >= 10 && itemSpawnCode < coinSpawnUpperRate)
        {
            obstacleGlow[2].SetActive(true);
            return items[2];
        }
        else return null;
    }

    private void EnableGameObjects()
    {

        for (int i = 0; i < obstacles.Length; i++)
        {
            obstacles[i].SetActive(true);
        }

    }

    private void DisableGameObjects()
    {
        if (obstacles.Length == 1) obstacles[0].SetActive(false);
        else
        {
            for (int i = 0; i < obstacles.Length - 1; i++)
            {
                obstacles[i].SetActive(false);
            }
        }

        if (destroyVFX != null) destroyVFX.Play();

        if (itemToSpawn != null)
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
        Invoke("Deactivate", 2f);
        DisableGameObjects();
    }

    public void WasHit(GameObject hit)
    {
        DisableGameObjects();

        if (itemToSpawn != null)
        {
            itemSpawned = Instantiate(itemToSpawn,
                itemSpawnTransform.position, itemSpawnTransform.rotation);

            Destroy(itemSpawned, 5f);
        }

        //ISSO ACONTECE QUANDO UM OBSTÁCULO BATE NO OUTRO
        ObstacleRoot root = hit.GetComponentInParent<ObstacleRoot>();
        if (root != null) root.Deactivate();

        //Destruirá o objeto apenas se for a bala
        if (hit.CompareTag("Bullet"))
        {
            hit.GetComponent<Bullet>().Deactivate();
            // Destroy(hit);
        }

        defaultMovementSpeed = movementSpeed;
        movementSpeed = 0;
        Invoke("Deactivate", 10f);
        GameController.gameController.ObstaclesDestroyedCounter();
    }

    private void Timer()
    {
        timer += Time.deltaTime;

        if (timer >= lifetime)
        {
            Deactivate();
        }
    }

    public void Deactivate()
    {
        gameObject.SetActive(false);
        DisableGameObjects();
        timer = 0;
    }

}
