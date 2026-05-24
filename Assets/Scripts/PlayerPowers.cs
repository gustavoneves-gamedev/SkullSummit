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
    public ParticleSystem staminaUp;
    private float potionRestauration;

    [Header("Coin Multiplier")]
    public bool isCoinMultiplierOn;
    public float coinMultiplier;
    [SerializeField] private ParticleSystem coinMultiplierVFX;
    private float boostedCoinMultiplier;
    private float coinMultiplierDuration;
    private float defaultMultiplierDuration;

    [Header("Special")]
    public bool canUseSpecial;
    public bool isSpecialOn;
    public float currentSpecial;
    public float maxSpecial;

    [Header("Resurrection Amulet")]
    public bool hasResurrectionAmulet;
    public int ressurrectionAmuletQuantity;
    private int ressurrectionAmuletRestauration;

    [Header("Special Boost")] //AINDA NÃO IMPLEMENTADO
    private int specialBoostQuantity;
    private int alocatedspecialBoostQuantity;
    private int specialBoostRestauration;

    [Header("Adrenaline")]
    private int adrenalineQuantity;
    private int alocatedAdrenalineQuantity;
    private int adrenalineRestauration;


    private PlayerRoot player;
    private Inventory inventory;
    private UIController uiController;

    void Start()
    {
        GameController.gameController.playerPowers = this;
        player = GetComponent<PlayerRoot>();

        Invoke("InitilizeReferences", .2f);
        Invoke("ResetPowers", .2f);
    }

    void Update()
    {
        if (player.canRun == false || player.isGamePaused) return;

        CoinMultiplierCountdown();
        ShieldCountdown();

        if (player.currentStamina < (player.maxStamina / 5) && alocatedAdrenalineQuantity > 0)
        {
            ActivateAdrenaline();
        }

        if (isSpecialOn)
        {
            canUseSpecial = false;

            //Rever esse 2 aí com base em cada personagem
            currentSpecial -= 2 * Time.deltaTime;
            if (currentSpecial <= 0)
            {
                isSpecialOn = false;
                player.SpecialSpeed(isSpecialOn);
            }
        }
        else
        {
            currentSpecial += 1 * Time.deltaTime;

            if (currentSpecial >= maxSpecial) canUseSpecial = true;

            //if (currentSpecial >= maxSpecial) ActivateSpecial();
        }

    }

    
    private void InitilizeReferences()
    {
        inventory = GameController.gameController.inventory;
        uiController = GameController.gameController.uiController;
    }

    public void ResetPowers()
    {
        //Shield
        isShieldUp = false;
        shield.SetActive(false);
        shieldEffect.SetActive(false);
        shieldCharges = defaultShieldCharges;
        shieldDuration = defaultShieldDuration;
                       
        coinMultiplierDuration = defaultMultiplierDuration;        
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
            shieldCharges = defaultShieldCharges;
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

    public void ActivateSpecial()
    {
        isSpecialOn = true;
        player.SpecialSpeed(isSpecialOn);
        
    }

    public void AddToSpecial(int x)
    {
        currentSpecial += x;

        if (currentSpecial >= maxSpecial)
        {
            currentSpecial = maxSpecial;
            canUseSpecial = true;
        }
       
    }

    #endregion
        
    #region Special Boost

    public void InitializeSpecialBoost(int quantity = 0, int restauration = 0)
    {
        specialBoostQuantity = quantity;
        specialBoostRestauration = restauration;
        //alocatedspecialBoostQuantity = 2;
    }

    public void AlocateSpecialBoost()
    {
        if (specialBoostQuantity <= 0) return;

        alocatedspecialBoostQuantity++;
        specialBoostQuantity--;
        inventory.ConsumeSpecialBoost(specialBoostQuantity);
    }

    public void ActivateSpecialBoost()
    {

        AddToSpecial(specialBoostRestauration);
        alocatedspecialBoostQuantity--;
    }

    #endregion

    #region Resurrection Amulet

    public void InitializeResurrectionAmulet(int quantity = 0, int restauration = 0)
    {
        ressurrectionAmuletQuantity = quantity;
        ressurrectionAmuletRestauration = restauration;

        if (quantity > 0) hasResurrectionAmulet = true;
    }

    public void ActivateResurrectionAmulet()
    {
        player.UpdateStamina(ressurrectionAmuletRestauration);

        ressurrectionAmuletQuantity--;
        if (ressurrectionAmuletQuantity <= 0) hasResurrectionAmulet = false;

        inventory.ConsumeResurrectionAmulet(ressurrectionAmuletQuantity);
        uiController.ResumeButton();
    }

    #endregion

    #region Adrenaline

    public void InitializeAdrenaline(int quantity = 0, int restauration = 0)
    {
        adrenalineQuantity = quantity;
        adrenalineRestauration = restauration;
        alocatedAdrenalineQuantity = 2;
    }

    public void AlocateAdrenalineQuantity()
    {
        if (adrenalineQuantity <= 0) return;

        alocatedAdrenalineQuantity++;
        adrenalineQuantity--;
        inventory.ConsumeAdrenaline(adrenalineQuantity);
    }

    public void ActivateAdrenaline()
    {

        player.UpdateStamina(adrenalineRestauration);
        alocatedAdrenalineQuantity--;
    }

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
            if (staminaUp != null) staminaUp.Play();
            other.GetComponent<Items>().PlayFX();

        }

        if (other.CompareTag("CoinMultiplier"))
        {
            CoinMultiplier();
            //Tocar som de ativar multiplicador
            if (coinMultiplierVFX != null) coinMultiplierVFX.Play();
            other.GetComponent<Items>().PlayFX();

        }


    }

}
