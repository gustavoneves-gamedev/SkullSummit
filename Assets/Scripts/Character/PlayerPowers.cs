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
    public int specialBoostQuantity {  get; private set; }
    private int alocatedspecialBoostQuantity;
    private int specialBoostRestauration;

    [Header("Adrenaline")]
    public int adrenalineQuantity { get; private set; }
    private int alocatedAdrenalineQuantity;
    private int adrenalineRestauration;


    private PlayerRoot player;
    private Inventory inventory;
    private UIController uiController;

    void Start()
    {
        GameController.gameController.playerPowers = this;
        player = GetComponent<PlayerRoot>();

        Invoke("ResetPowers", .2f);
        Invoke("InitilizeReferences", .2f);
    }

    void Update()
    {
        if (player.canRun == false || player.isGamePaused || 
            !GameController.gameController.isRunning) return;

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
                uiController.SpecialReady(canUseSpecial);
                player.SpecialSpeed(isSpecialOn);
                ActivateSpecialBoost();
            }
        }
        else if (!canUseSpecial)
        {
            currentSpecial += 1 * Time.deltaTime;

            if (currentSpecial >= maxSpecial)
            {
                canUseSpecial = true;
                currentSpecial = maxSpecial;

                if (GameController.gameController.isTutorialIncomplete) uiController.SpecialTutorial();
            }                
                
            if(currentSpecial >= maxSpecial*0.95f && currentSpecial >= 18) uiController.SpecialReady(true);


            //if (currentSpecial >= maxSpecial) ActivateSpecial();
        }

    }

    
    private void InitilizeReferences()
    {
        inventory = GameController.gameController.inventory;
        uiController = GameController.gameController.uiController;
        uiController.SpecialReady(false);
    }

    public void ResetPowers()
    {
        //Special
        currentSpecial = 0;
        canUseSpecial = false;

        //Shield
        isShieldUp = false;
        shield.SetActive(false);
        shieldEffect.SetActive(false);
        shieldCharges = defaultShieldCharges;
        shieldDuration = defaultShieldDuration;
        
        //Coin Multiplier
        coinMultiplierDuration = defaultMultiplierDuration;

        //Consumables
        alocatedspecialBoostQuantity = 0;
        alocatedAdrenalineQuantity = 0;
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

    public void AlocateSpecialBoost(int quantity = 0)
    {
        //if (specialBoostQuantity <= 0) return;
        
        if (specialBoostQuantity > 0 && quantity > 0) alocatedspecialBoostQuantity += quantity;
        else if (quantity < 0) alocatedspecialBoostQuantity += quantity;

        if(alocatedspecialBoostQuantity + 0.5f > 0 && quantity < 0) specialBoostQuantity -= quantity;
        else if (quantity > 0) specialBoostQuantity -= quantity;

        if (alocatedspecialBoostQuantity <= 0) alocatedspecialBoostQuantity = 0;
        if (specialBoostQuantity <= 0) specialBoostQuantity = 0;

        uiController.SpecialBoostIndicator(alocatedspecialBoostQuantity);
        inventory.ConsumeSpecialBoost(specialBoostQuantity);
        uiController.SpecialBoostAlocation(alocatedspecialBoostQuantity, specialBoostQuantity);
    }

    public void ActivateSpecialBoost()
    {
        if (alocatedspecialBoostQuantity <= 0) return;

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
        //alocatedAdrenalineQuantity = 2;
    }

    public void AlocateAdrenalineQuantity(int quantity = 0)
    {
        
        //if (adrenalineQuantity <= 0) return;
        if(adrenalineQuantity > 0 && quantity > 0) alocatedAdrenalineQuantity += quantity;
        else if (quantity < 0) alocatedAdrenalineQuantity += quantity;

        if (alocatedAdrenalineQuantity + 0.5f > 0 && quantity < 0) adrenalineQuantity -= quantity;
        else if (quantity > 0) adrenalineQuantity -= quantity;

        if (alocatedAdrenalineQuantity <= 0) alocatedAdrenalineQuantity = 0;
        if (adrenalineQuantity <= 0) adrenalineQuantity = 0;

        uiController.AdrenalineIndicator(alocatedAdrenalineQuantity);
        inventory.ConsumeAdrenaline(adrenalineQuantity);
        uiController.AdrenalineAlocation(alocatedAdrenalineQuantity, adrenalineQuantity);
    }

    public void ActivateAdrenaline()
    {
        if (alocatedAdrenalineQuantity <= 0) return;

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
