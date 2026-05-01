using UnityEngine;

public class Inventory : MonoBehaviour
{
    [Header("Shield")]
    private ItemID shildChargeID = ItemID.ShieldCharges;
    [SerializeField] private ItemData shieldChargeData;
    private ItemID shildDurationID = ItemID.ShieldDuration;
    [SerializeField] private ItemData shieldDurationData;
    [SerializeField] private float baseShieldDuration = 20f; //substituir por shildData.baseDuration
    private int shieldCharges = 1; //Igualar a shildData.baseEffectCharges
    private float shieldDuration;
    private int shieldUpgradeLevel = 0;
    public int shieldDurationUpgradeLevel = 0; 
    public int shieldDurationUpgradeFactor = 10; //substituir por shildData.durationFactorUpgrade
    public int shieldDurationUpgradeCost = 1000; //substituir por shildData.coinDurationUpgradeCost[shieldDurationUpgradeLevel]
    private int shieldDurationMaxLevel = 8;  //substituir por shildData.durationMaxLevel
    public int shieldChargeUpgrade = 0;
    private int shieldChargeMaxLevel = 2; //substituir por shildData.baseEffectMaxLevel
    public int shieldChargeUpgradeCost = 20000; //substituir por shildData.coinChargeUpgradeCost[shieldChargeUpgrade]

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
        UIShieldDurationUpgrade();

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
                    shieldDurationUpgradeLevel * shieldDurationUpgradeFactor;

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
        shieldUpgradeLevel++;

        shieldChargeUpgradeCost *= 5;

        UIShieldChargeUpgrade();
        ShieldInitialization();
    }

    private void UIShieldChargeUpgrade()
    {
        GameController.gameController.uiController.
            UpdateShieldChargeUpgradeUI((shieldChargeUpgrade),
            shieldChargeUpgrade, shieldChargeUpgradeCost, shieldUpgradeLevel);
    }

    public void UpgradeShieldDuration()
    {
        if (shieldDurationUpgradeLevel >= shieldDurationMaxLevel) return;

        shieldDurationUpgradeLevel++;
        shieldUpgradeLevel++;

        shieldDurationUpgradeCost *= 2;

        UIShieldDurationUpgrade();
        ShieldInitialization();
    }

    private void UIShieldDurationUpgrade()
    {
        GameController.gameController.uiController.
            UpdateShieldDurationUpgradeUI((shieldDurationUpgradeLevel * shieldDurationUpgradeFactor),
            shieldDurationUpgradeLevel, shieldDurationUpgradeCost, shieldUpgradeLevel);
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
