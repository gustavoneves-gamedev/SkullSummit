using UnityEngine;

public class ObstacleRoot : MonoBehaviour
{
    [SerializeField] private GameObject obstacle;
    [SerializeField] private float damage;
    [SerializeField] private float movementSpeed = 10f;
    [SerializeField] private int obsctacleType = 0;    

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
        rotateSpeedA = Random.Range(15f, 25f);
        rotateSpeedB = Random.Range(20f, 40f);
        rotateSpeedC = Random.Range(25f, 50f); ;

        if (obsctacleType == 1)
        {
            transform.position += Vector3.up * 4f;

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

    public void ApplyDamage()
    {
        player.UpdateStamina(-damage);
        player.SpeedReset();
        Destroy(gameObject, 10f);
        obstacle.SetActive(false);
    }

    public void WasShot(GameObject bullet)
    {

        Destroy(bullet);
        Destroy(gameObject, 10f);

    }


}
