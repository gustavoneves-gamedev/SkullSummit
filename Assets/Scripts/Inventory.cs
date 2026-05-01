using UnityEngine;

public class Inventory : MonoBehaviour
{
    [Header("Shield Charges")]
    [SerializeField] private ItemData shieldChargeData;
    //private ItemID shildChargeID = ItemID.ShieldCharges;
    private int shieldCharges;
    private int shieldUpgradeLevel = 0;
    private int shieldChargeUpgradeLevel = 0;
    private int shieldChargeUpgradeCoinCost;
    private int shieldChargeUpgradeRubyCost; //Passar para a HUD!!

    [Header("Shield Duration")]
    [SerializeField] private ItemData shieldDurationData;
    //private ItemID shildDurationID = ItemID.ShieldDuration;      
    private float shieldDuration;
    private int shieldDurationUpgradeLevel = 0;
    private int shieldDurationUpgradeCoinCost;
    private int shieldDurationUpgradeRubyCost; //Passar para a HUD!!

    [Header("Stamina Potion")]
    [SerializeField] private ItemData staminaData;
    //private ItemID staminaPotionID = ItemID.StaminaPotion;    
    private int potionRestauration;
    private int staminaPotionUpgradeLevel = 0;
    private int staminaPotionUpgradeCoinCost;
    private int staminaPotionUpgradeRubyCost; //Passar para a HUD!!

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
        //Essas duas linhas servem para pegar o valor atual do upgrade antes de fazer compras
        shieldChargeUpgradeCoinCost = shieldChargeData.coinChargeUpgradeCost[shieldChargeUpgradeLevel];
        shieldChargeUpgradeRubyCost = shieldChargeData.rubyChargeUpgradeCost[shieldChargeUpgradeLevel];

        shieldCharges = shieldChargeData.baseEffectCharges +
                    shieldChargeUpgradeLevel * shieldChargeData.levelFactorUpgrade;

        shieldDurationUpgradeCoinCost = shieldDurationData.coinChargeUpgradeCost[shieldDurationUpgradeLevel];
        shieldChargeUpgradeRubyCost = shieldDurationData.rubyChargeUpgradeCost[shieldDurationUpgradeLevel];

        shieldDuration = shieldDurationData.baseEffectCharges +
                    shieldDurationUpgradeLevel * shieldDurationData.levelFactorUpgrade;

        GameController.gameController.playerPowers.
            InitializeShieldPower(shieldDuration, shieldCharges);
    }

    private void StaminaPotionInitialization()
    {
        staminaPotionUpgradeCoinCost = staminaData.coinChargeUpgradeCost[staminaPotionUpgradeLevel];
        staminaPotionUpgradeRubyCost = staminaData.rubyChargeUpgradeCost[staminaPotionUpgradeLevel];

        potionRestauration = staminaData.baseEffectCharges +
                    staminaPotionUpgradeLevel * staminaData.levelFactorUpgrade;

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
        if (shieldChargeUpgradeLevel >= shieldChargeData.maxLevel) return;

        shieldChargeUpgradeLevel++;
        shieldUpgradeLevel++;

        shieldChargeUpgradeCoinCost = shieldChargeData.coinChargeUpgradeCost[shieldChargeUpgradeLevel];
        shieldChargeUpgradeRubyCost = shieldChargeData.rubyChargeUpgradeCost[shieldChargeUpgradeLevel];

        UIShieldChargeUpgrade();
        ShieldInitialization();
    }

    private void UIShieldChargeUpgrade()
    {
        GameController.gameController.uiController.
            UpdateShieldChargeUpgradeUI((shieldChargeUpgradeLevel * shieldChargeData.levelFactorUpgrade),
            shieldChargeUpgradeLevel, shieldChargeUpgradeCoinCost, shieldUpgradeLevel);
    }

    public void UpgradeShieldDuration()
    {
        if (shieldDurationUpgradeLevel >= shieldDurationData.maxLevel) return;

        shieldDurationUpgradeLevel++;
        shieldUpgradeLevel++;

        shieldDurationUpgradeCoinCost = shieldDurationData.coinChargeUpgradeCost[shieldDurationUpgradeLevel];
        shieldDurationUpgradeRubyCost = shieldDurationData.rubyChargeUpgradeCost[shieldDurationUpgradeLevel];

        UIShieldDurationUpgrade();
        ShieldInitialization();
    }

    private void UIShieldDurationUpgrade()
    {
        GameController.gameController.uiController.
            UpdateShieldDurationUpgradeUI((shieldDurationUpgradeLevel * shieldDurationData.levelFactorUpgrade),
            shieldDurationUpgradeLevel, shieldDurationUpgradeCoinCost, shieldUpgradeLevel);
    }


    #endregion

    #region Potion Upgrade

    public void PotionUpgrade()
    {
        if (staminaPotionUpgradeLevel >= staminaData.maxLevel) return;
        //Também devo mudar o texto e a cor do botão neste caso, mas deixarei assim por enquanto

        staminaPotionUpgradeLevel++;

        staminaPotionUpgradeCoinCost = staminaData.coinChargeUpgradeCost[staminaPotionUpgradeLevel];
        staminaPotionUpgradeRubyCost = staminaData.rubyChargeUpgradeCost[staminaPotionUpgradeLevel];
        

        UiStaminaPotionUpdate();
        StaminaPotionInitialization();
        //GameController.gameController.playerPowers.InitializeStaminaPotion();
    }

    private void UiStaminaPotionUpdate()
    {
        GameController.gameController.uiController.
            UpdateStaminaPostionUpgradeUI((staminaPotionUpgradeLevel * staminaData.levelFactorUpgrade),
            staminaPotionUpgradeLevel, staminaPotionUpgradeCoinCost);
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
