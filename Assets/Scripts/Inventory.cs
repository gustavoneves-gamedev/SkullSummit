using UnityEngine;

public class Inventory : MonoBehaviour
{
    [Header("Shield Charges")]
    [SerializeField] private ItemData shieldChargeData;
    //private ItemID shildChargeID = ItemID.ShieldCharges;
    private int shieldCharges;
    private int shieldUpgradeLevel = 0;
    private int shieldChargeUpgradeLevel = 0;
    private int shieldChargeUpgradeCoinCost; //substituir por shildData.coinChargeUpgradeCost[shieldChargeUpgrade]
    private int shieldChargeUpgradeRubyCost; //Passar para a HUD!!

    [Header("Shield Duration")]
    [SerializeField] private ItemData shieldDurationData;
    //private ItemID shildDurationID = ItemID.ShieldDuration;      
    private float shieldDuration;
    private int shieldDurationUpgradeLevel = 0;
    private int shieldDurationUpgradeCoinCost;
    private int shieldDurationUpgradeRubyCost;

    [Header("Stamina Potion")]
    [SerializeField] private ItemData staminaData;
    //private ItemID staminaPotionID = ItemID.StaminaPotion;
    private int basePotionRestauration = 10;
    private int potionRestauration;
    public int staminaPotionUpgradeLevel = 0;
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
        staminaPotionUpgradeCost = staminaData.coinChargeUpgradeCost[staminaPotionUpgradeLevel];
        UiStaminaPotionUpdate();
        StaminaPotionInitialization();

        //COIN MULTIPLIER
        CoinMultiplierInitilization();
    }

    #region Item Initialization

    private void ShieldInitialization()
    {
        shieldCharges = shieldChargeData.baseEffectCharges +
                    shieldChargeUpgradeLevel * shieldChargeData.levelFactorUpgrade;

        shieldDuration = shieldDurationData.baseEffectCharges +
                    shieldDurationUpgradeLevel * shieldDurationData.levelFactorUpgrade;

        GameController.gameController.playerPowers.
            InitializeShieldPower(shieldDuration, shieldCharges);
    }

    private void StaminaPotionInitialization()
    {
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

        shieldChargeUpgradeCoinCost = shieldChargeData.coinChargeUpgradeCost[shieldChargeUpgradeLevel];
        shieldChargeUpgradeRubyCost = shieldChargeData.rubyChargeUpgradeCost[shieldChargeUpgradeLevel];

        shieldChargeUpgradeLevel++;
        shieldUpgradeLevel++;

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

        shieldDurationUpgradeCoinCost = shieldDurationData.coinChargeUpgradeCost[shieldDurationUpgradeLevel];
        shieldDurationUpgradeRubyCost = shieldDurationData.rubyChargeUpgradeCost[shieldDurationUpgradeLevel];

        shieldDurationUpgradeLevel++;
        shieldUpgradeLevel++;

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

        if (staminaPotionUpgradeLevel < staminaData.coinChargeUpgradeCost.Length)
        {
            staminaPotionUpgradeCost = staminaData.coinChargeUpgradeCost[staminaPotionUpgradeLevel];
        }
        else
        {
            staminaPotionUpgradeCost = -1;
        }

            //staminaPotionUpgradeCost *= (1 + staminaPotionUpgrade);

            UiStaminaPotionUpdate();
        StaminaPotionInitialization();
        //GameController.gameController.playerPowers.InitializeStaminaPotion();
    }

    private void UiStaminaPotionUpdate()
    {
        GameController.gameController.uiController.
            UpdateStaminaPostionUpgradeUI((staminaPotionUpgradeLevel * staminaPotionUpgradeFactor),
            staminaPotionUpgradeLevel, staminaPotionUpgradeCost);
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
