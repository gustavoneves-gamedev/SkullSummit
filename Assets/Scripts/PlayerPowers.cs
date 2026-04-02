using UnityEngine;

public class PlayerPowers : MonoBehaviour
{
    [Header("Shield")]
    public bool isShieldUp;
    [SerializeField] private GameObject shield;
    private int shieldCharges = 1;
    private int defaultShieldCharges;
    private float baseShieldDuration = 20f;
    private float shieldDuration;
    private float defaultShieldDuration;

    [Header("Stamina Potion")]
    private float basePotionRestauration = 10f;
    private float potionRestauration;

    [Header("Coin Multiplier")]
    [SerializeField] private bool isCoinMultiplierOn;
    private int coinBaseMultiplier = 2;
    public float coinMultiplier;
    private float boostedCoinMultiplier;   
    private float coinBaseMultiplierDuration = 16f;
    private float coinMultiplierDuration;
    private float defaultMultiplierDuration;

    private PlayerRoot player;
    private Inventory inventory;


    void Start()
    {
        GameController.gameController.playerPowers = this;
        player = GetComponent<PlayerRoot>();        

        Invoke("InitilizePowers", .2f);        
        Invoke("ResetPowers", .2f);
    }

    void Update()
    {
        CoinMultiplierCountdown();
        ShieldCountdown();
    }

    //IPC: Acho que vou passar esse cálculo para o invetory e puxar só os valores finais
    private void InitilizePowers()
    {
        inventory = GameController.gameController.inventory;

        //Shield
        InitializeShield();

        //Potion        
        InitializeStaminaPotion();

        //Coin Multiplier
        InitializeCoinMultiplier();
    }

    public void ResetPowers()
    {
        //Escudo
        isShieldUp = false;
        shield.SetActive(false);
        shieldCharges = defaultShieldCharges;
        shieldDuration = defaultShieldDuration;

        //Multiplicador de moedas        
        coinMultiplierDuration = defaultMultiplierDuration;
        //player.normalCoinMultiplier = 2;
    }

    #region Shield

    private void InitializeShield()
    {
        shieldDuration = baseShieldDuration +
                    inventory.shieldDurationUpgrade * inventory.shieldUpgradeFactor;

        shieldCharges = inventory.shieldChargeUpgrade;

        defaultShieldCharges = shieldCharges;
        defaultShieldDuration = shieldDuration;
    }

    public void Shield(float x = 0)
    {
        if (shieldCharges > 1 && isShieldUp && x < 0)
        {
            shieldCharges--;
        }
        else if (isShieldUp && x >= 0)
        {
            shieldCharges = defaultShieldCharges;
            shieldDuration = defaultShieldDuration;
        }
        else
        {
            isShieldUp = !isShieldUp;
            shield.SetActive(!shield.activeSelf);
            shieldDuration = defaultShieldDuration;
        }
    }

    private void ShieldCountdown()
    {
        if (isShieldUp)
        {
            shieldDuration -= Time.deltaTime;

            if (shieldDuration <= 0)
            {
                isShieldUp = false;
                shieldDuration = defaultShieldDuration;
                shield.SetActive(false);
            }
        }
    }

    #endregion

    #region Stamina Potion
    private void InitializeStaminaPotion()
    {
        potionRestauration = basePotionRestauration +
                    inventory.staminaPotionUpgrade * inventory.staminaPotionUpgradeFactor;
    }

    #endregion

    #region Coin Multiplier
    private void InitializeCoinMultiplier()
    {
        coinMultiplier = 1;

        boostedCoinMultiplier = coinBaseMultiplier +
                    inventory.coinMultiplierUpgrade * inventory.coinMultiplierUpgradeFactor;

        coinMultiplierDuration = coinBaseMultiplierDuration +
            inventory.coinDurationUpgrade * inventory.coinDurationUpgradeFactor;

        defaultMultiplierDuration = coinMultiplierDuration;
    }

    private void CoinMultiplier()
    {
        if (isCoinMultiplierOn)
        {
            coinMultiplierDuration = defaultMultiplierDuration;            
        }
        else
        {
            isCoinMultiplierOn = true;
            coinMultiplier = boostedCoinMultiplier;
        }
    }

    private void CoinMultiplierCountdown()
    {
        if (isCoinMultiplierOn)
        {
            coinMultiplierDuration -= Time.deltaTime;

            if (coinMultiplierDuration <= 0)
            {
                isCoinMultiplierOn = false;
                coinMultiplierDuration = defaultMultiplierDuration;
                coinMultiplier = 1;
            }
        }
    }

    #endregion

    #region Special



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
            //Tocar som de ativar multiplicador
            Destroy(other.gameObject);
        }


    }

}
