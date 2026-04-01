using UnityEngine;

public class PlayerPowers : MonoBehaviour
{
    [Header("Shield")]
    public bool isShieldUp;
    [SerializeField] private GameObject shield;
    private int shieldCharges;

    [Header("Stamina Potion")]
    private float potionRestauration = 10f;

    [Header("Coin Multiplier")]
    private bool isCoinMultiplierOn;
    private float coinMultiplier = 2f;
    private float multiplierDuration = 10f;
    private float defaultMultiplierDuration;

    private PlayerRoot player;


    void Start()
    {
        GameController.gameController.playerPowers = this;
        player = GetComponent<PlayerRoot>();
        ResetPowers();
    }

    void Update()
    {
        CoinMultiplierCountdown();
    }

    public void ResetPowers()
    {
        //Escudo
        isShieldUp = false;
        shield.SetActive(false);

        //Multiplicador de moedas
        defaultMultiplierDuration = multiplierDuration;
    }

    #region Shield

    public void Shield()
    {
        if (shieldCharges > 1)
        {
            shieldCharges--;
        }
        else
        {
            isShieldUp = !isShieldUp;
            shield.SetActive(!shield.activeSelf);
        }
    }

    #endregion

    #region Coin Multiplier
    private void CoinMultiplier()
    {
        if (isCoinMultiplierOn)
        {
            multiplierDuration = defaultMultiplierDuration;
        }
        else
        {
            isCoinMultiplierOn = true;
            player.normalCoinMultiplier += coinMultiplier;
        }
    }

    private void CoinMultiplierCountdown()
    {
        if (isCoinMultiplierOn)
        {
            multiplierDuration -= Time.deltaTime;

            if (multiplierDuration <= 0)
            {
                isCoinMultiplierOn = false;
                multiplierDuration = defaultMultiplierDuration;
                player.normalCoinMultiplier -= coinMultiplier;
            }
        }
    }

    #endregion

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Shield"))
        {
            Shield();
            //Tocar som de escudo subindo
            Destroy(other.gameObject);
        }

        if (other.CompareTag("StaminaPotion"))
        {
            player.UpdateStamina(potionRestauration); 
            //Tocar som de stamina recuperando
            Destroy(other.gameObject);
        }

        if (other.CompareTag("CoinMultiplier"))
        {
            CoinMultiplier();
            //Ativar multiplicador
        }


    }

}
