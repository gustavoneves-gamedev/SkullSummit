using UnityEngine;

public class ObstacleRoot : MonoBehaviour
{
    [SerializeField] private GameObject obstacle;
    [SerializeField] private float damage;
    [SerializeField] private float movementSpeed = 10f;
    [SerializeField] private int obsctacleType = 0;

    private PlayerRoot player;
    //private Collider collider;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(gameObject, 30f);
        player = GameController.gameController.playerRoot;
    }

    // Update is called once per frame
    void Update()
    {
        if(obsctacleType == 1) transform.position += Vector3.back * movementSpeed * Time.deltaTime;

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
