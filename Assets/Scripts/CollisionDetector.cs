using UnityEngine;

public class CollisionDetector : MonoBehaviour
{
    [SerializeField] private int index = 1;
    //1 = Obstáculo
    //2 = Moeda
    //3 = Itens
    private ObstacleRoot obstacleRoot;
    private PlayerRoot playerRoot;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (index == 1)
        {
            obstacleRoot = GetComponentInParent<ObstacleRoot>();
            playerRoot = GameController.gameController.playerRoot;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Debug.Log("Colidi com algo");

            if (index == 1)
            {
                obstacleRoot.ApplyDamage();
                gameObject.SetActive(false);
            }

        }

        if (other.CompareTag("Bullet"))
        {
            //Debug.Log("Colidi com a bala");

            if (index == 1)
            {
                if (obstacleRoot.obsctacleType == 0)
                {
                    Destroy(other.gameObject);
                }
                else
                {
                    obstacleRoot.WasHit(other.gameObject);
                    gameObject.SetActive(false);
                }
            }
        }

        if (other.CompareTag("MovableObstacle") && transform.position.z - playerRoot.transform.position.z <= 50f)
        {
             
            if (index == 1)
            {
                obstacleRoot.WasHit(other.gameObject);
                gameObject.SetActive(false);
            }

        }

        //Colisão com outro obstáculo
        //Determinar comportamento de troca de lane
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("MovableObstacle"))
        {
           // Debug.Log("Colidi com um obstáculo");

            if (index == 1)
            {
                obstacleRoot.WasHit(collision.gameObject.GetComponentInParent<GameObject>());
                gameObject.SetActive(false);
            }

        }
    }
}
