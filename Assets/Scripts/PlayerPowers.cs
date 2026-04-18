using UnityEngine;

public class PlayerPowers : MonoBehaviour
{
    [Header("Shield")]
    public bool isShieldUp;
    [SerializeField] private GameObject shield;
    [SerializeField] private GameObject shieldEffect;
    private int shieldCharges = 1;
    private int defaultShieldCharges;    
    private float shieldDuration;
    private float defaultShieldDuration;

    [Header("Stamina Potion")]    
    private float potionRestauration;

    [Header("Coin Multiplier")]
    public bool isCoinMultiplierOn;
    public float coinMultiplier;
    private float boostedCoinMultiplier;
    private float coinMultiplierDuration;
    private float defaultMultiplierDuration;

    private PlayerRoot player;
    private Inventory inventory; //Está inutilizada por enquanto


    void Start()
    {
        GameController.gameController.playerPowers = this;
        player = GetComponent<PlayerRoot>();        

        //Invoke("InitilizePowers", .2f);        
        Invoke("ResetPowers", .2f);
    }

    void Update()
    {
        CoinMultiplierCountdown();
        ShieldCountdown();
    }
    
    //Inutilizada temporariamente esta função
    private void InitilizePowers()
    {
        inventory = GameController.gameController.inventory;
    }

    public void ResetPowers()
    {
        //Escudo
        isShieldUp = false;
        shield.SetActive(false);
        shieldEffect.SetActive(false);
        shieldCharges = defaultShieldCharges;
        shieldDuration = defaultShieldDuration;

        //Multiplicador de moedas        
        coinMultiplierDuration = defaultMultiplierDuration;
        //player.normalCoinMultiplier = 2;
    }

    #region Shield

    public void InitializeShieldPower(float duration = 0, int charges = 0)
    {
        
        shieldDuration = duration;
        shieldCharges = charges;

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
            shieldEffect.SetActive(!shieldEffect.activeSelf);
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
                shieldEffect.SetActive(false);
            }
        }
    }

    #endregion

    #region Stamina Potion
    public void InitializeStaminaPotion(int restauration = 0)
    {
        potionRestauration = restauration;
    }

    #endregion

    #region Coin Multiplier
    public void InitializeCoinMultiplier(float boosted = 0, float duration = 0)
    {
        coinMultiplier = 1;

        boostedCoinMultiplier = boosted;
        coinMultiplierDuration = duration;

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
            other.GetComponent<Items>().PlayFX();
            
            
        }

        if (other.CompareTag("StaminaPotion"))
        {
            player.UpdateStamina(potionRestauration);
            //Tocar som de stamina recuperando
            //other.GetComponent<ParticleSystem>()?.Play();
            other.GetComponent<Items>().PlayFX();
            
        }

        if (other.CompareTag("CoinMultiplier"))
        {
            CoinMultiplier();
            //Tocar som de ativar multiplicador
            //other.GetComponent<ParticleSystem>()?.Play();
            other.GetComponent<Items>().PlayFX();
            
        }


    }

}
