using UnityEngine;

public class CollisionDetector : MonoBehaviour
{
    [SerializeField] private int index = 1;
    //1 = Obstáculo
    //2 = Moeda
    //3 = Itens
    private ObstacleRoot obstacleRoot;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (index == 1)
        {
            obstacleRoot = GetComponentInParent<ObstacleRoot>();
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
                    obstacleRoot.WasShot(other.gameObject);
                    gameObject.SetActive(false);
                }
            }
        }

        //Colisão com outro obstáculo
        //Determinar comportamento de troca de lane
    }
}
