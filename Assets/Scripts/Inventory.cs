using UnityEngine;

public class Inventory : MonoBehaviour
{
    [Header("Shield")]
    [SerializeField] private float baseShieldDuration = 20f;
    private int shieldCharges = 1;    
    private float shieldDuration;    
    public int shieldDurationUpgrade = 0;
    public float shieldUpgradeFactor = 10f;
    public int shieldChargeUpgrade = 0;
    private int shieldChargeMaxLevel = 2;
    public int shieldChargeUpgradeCost = 20000;

    [Header("Stamina Potion")]
    [SerializeField] private int basePotionRestauration = 10;
    private int potionRestauration;
    public int staminaPotionUpgrade = 0;
    private int staminaPotionMaxLevel = 5;
    public int staminaPotionUpgradeFactor = 5;
    public int staminaPotionUpgradeCost = 1000;    

    [Header("Coin Multiplier")]
    public bool isCoinMultiplierOn;
    private int coinBaseMultiplier = 2;
    public float coinMultiplier;
    private float boostedCoinMultiplier;
    private float coinBaseMultiplierDuration = 16f;
    private float coinMultiplierDuration;   
    public int coinDurationUpgrade = 0;
    public float coinDurationUpgradeFactor = 8f;
    public int coinMultiplierUpgrade = 0;
    public float coinMultiplierUpgradeFactor = 1f;

    void Start()
    {
        GameController.gameController.inventory = this;
        Invoke("InitializeItemsUpgrades", .2f);
    }

    private void InitializeItemsUpgrades()
    {
        //SHIELD
        ShieldInitialization();
        UIShieldChargeUpgrade();

        //STAMINA POTION
        //Inserir aqui a puxada de informações do script onde estarão as informações da poção
        UiStaminaPotionUpdate();
        StaminaPotionInitialization();

        //COIN MULTIPLIER
        CoinMultiplierInitilization();
    }

    #region Item Initialization

    private void ShieldInitialization()
    {
        shieldDuration = baseShieldDuration +
                    shieldDurationUpgrade * shieldUpgradeFactor;

        shieldCharges = shieldChargeUpgrade + 1;

        GameController.gameController.playerPowers.
            InitializeShieldPower(shieldDuration, shieldCharges);
    }

    private void StaminaPotionInitialization()
    {
        potionRestauration = basePotionRestauration +
                    staminaPotionUpgrade * staminaPotionUpgradeFactor;

        GameController.gameController.playerPowers.
            InitializeStaminaPotion(potionRestauration);
    }

    private void CoinMultiplierInitilization()
    {
        coinMultiplier = 1;

        boostedCoinMultiplier = coinBaseMultiplier +
                    coinMultiplierUpgrade * coinMultiplierUpgradeFactor;

        coinMultiplierDuration = coinBaseMultiplierDuration +
            coinDurationUpgrade * coinDurationUpgradeFactor;

        GameController.gameController.playerPowers.
            InitializeCoinMultiplier(boostedCoinMultiplier, coinMultiplierDuration);

    }


    #endregion

    #region Item Upgrades

    #region Shield Upgrades
    public void UpgradeShieldCharges()
    {
        if (shieldChargeUpgrade >= shieldChargeMaxLevel) return;

        shieldChargeUpgrade++;

        shieldChargeUpgradeCost *= 5;

        UIShieldChargeUpgrade();
        ShieldInitialization();
    }

    private void UIShieldChargeUpgrade()
    {
        GameController.gameController.uiController.
            UpdateShieldChargeUpgradeUI((shieldChargeUpgrade),
            shieldChargeUpgrade, shieldChargeUpgradeCost);
    }

    public void UpgradeShieldDuration()
    {
        shieldDurationUpgrade++;        
    }


    #endregion

    #region Potion Upgrade

    public void PotionUpgrade()
    {
        if (staminaPotionUpgrade >= staminaPotionMaxLevel) return;
        //Também devo mudar o texto e a cor do botão neste caso, mas deixarei assim por enquanto
        
        staminaPotionUpgrade++;
        //staminaPotionUpgradeCost *= (1 + staminaPotionUpgrade);
        staminaPotionUpgradeCost *= (2);

        UiStaminaPotionUpdate();
        StaminaPotionInitialization();
        //GameController.gameController.playerPowers.InitializeStaminaPotion();
    }

    private void UiStaminaPotionUpdate()
    {
        GameController.gameController.uiController.
            UpdateStaminaPostionUpgradeUI((staminaPotionUpgrade * staminaPotionUpgradeFactor),
            staminaPotionUpgrade, staminaPotionUpgradeCost);
    }

    #endregion

    #region Coin Multiplier

    public void UpgradeCoinDuration()
    {
        coinDurationUpgrade++;
    }

    public void UpgradeCoinMultiplier()
    {
        coinMultiplierUpgrade++;
    }

    #endregion

    #endregion
}
