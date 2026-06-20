using UnityEngine;

public class CoinStack : MonoBehaviour
{
    [SerializeField] private float lifetime = 50f;
    private float timer;
    [SerializeField] private GameObject[] coins;

    private void OnEnable()
    {
        timer = 0;

        for (int i = 0; i < coins.Length; i++)
        {
            coins[i].SetActive(true);
        }
    }
    
    void Update()
    {
        if (!GameController.gameController.isRunning)
        {
            Deactivate();
            return;
        }

        Timer();

        if (GameController.gameController.playerRoot.gameObject.transform.position.z > transform.position.z + 10f)
        {
            Deactivate();
        }
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
        timer = 0;
    }

}
