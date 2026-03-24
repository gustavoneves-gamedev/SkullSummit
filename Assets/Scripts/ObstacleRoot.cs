using UnityEngine;

public class ObstacleRoot : MonoBehaviour
{
    [SerializeField] private GameObject obstacle;
    [SerializeField] private float damage;
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
        if(GameController.gameController.isRunning) return;
        Destroy(gameObject, 1f);
    }

    public void ApplyDamage()
    {
        Debug.Log("Colidi com algo");
        player.UpdateStamina(-damage);
        player.SpeedReset();
        Destroy(gameObject, 10f);
        obstacle.SetActive(false);
    }

    public void WasShot()
    {
        Debug.Log("Colidi com a bala");
        Destroy(gameObject, 10f);
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Colidi com algo");
            player.UpdateStamina(-damage);
            player.SpeedReset();
            Destroy(gameObject, 10f);
            obstacle.SetActive(false);
        }

        if (other.CompareTag("Bullet"))
        {
            Debug.Log("Colidi com a bala");            
            Destroy(gameObject, 10f);
            obstacle.SetActive(false);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        
    }
}
