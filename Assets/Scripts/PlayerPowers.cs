using UnityEngine;

public class PlayerPowers : MonoBehaviour
{
    [Header("Shield")]
    public bool isShieldUp;
    [SerializeField] private GameObject shield;
    private int shieldCharges = 1;
    private int defaultShieldCharges;

    [Header("Stamina Potion")]
    private float potionRestauration = 10f;

    [Header("Coin Multiplier")]
    [SerializeField] private bool isCoinMultiplierOn;
    private float coinMultiplier = 2f;
    private float multiplierDuration = 10f;
    private float defaultMultiplierDuration;

    private PlayerRoot player;


    void Start()
    {
        GameController.gameController.playerPowers = this;
        player = GetComponent<PlayerRoot>();
        InitilizePowers();
        ResetPowers();
    }

    void Update()
    {
        CoinMultiplierCountdown();
    }

    private void InitilizePowers()
    {
        //Shield
        //Shield Charges += Inventory...
        defaultShieldCharges = shieldCharges;

        //Potion
        //potionRestauration += Inventory...

        //Coin Multiplier
        //multiplierDuration += Inventory...
        //coinMultiplier += Inventory...
        defaultMultiplierDuration = multiplierDuration;
    }

    public void ResetPowers()
    {
        //Escudo
        isShieldUp = false;
        shield.SetActive(false);
        shieldCharges = defaultShieldCharges;

        //Multiplicador de moedas        
        multiplierDuration = defaultMultiplierDuration;
        player.normalCoinMultiplier = 1;
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
                player.normalCoinMultiplier = 1;
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
