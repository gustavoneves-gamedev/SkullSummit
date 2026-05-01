using UnityEngine;

public class Inventory : MonoBehaviour
{
    [Header("Shield Charges")]
    [SerializeField] private ItemData shieldChargeData;
    //private ItemID shildChargeID = ItemID.ShieldCharges;
    private int shieldUpgradeLevel = 0; //Mede o Level geral do escudo: Charge + Duration
    private int shieldCharges;
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
    [SerializeField] private ItemData coinMultiplierData;
    private int coinMultiplierLevel = 0;    
    private float boostedCoinMultiplier;
    private int coinMultiplierUpgradeLevel = 0;
    private int coinMultiplierUpgradeCoinCost;
    private int coinMultiplierUpgradeRubyCost;


    [Header("Coin Multiplier Duration")]
    [SerializeField] private ItemData coinMultiplierDurationData;
    public bool isCoinMultiplierOn;    
    private float coinMultiplierDuration;
    private int coinMultiplierDurationUpgradeLevel = 0;    
    private int coinMultiplierDurationUpgradeCoinCost;
    private int coinMultiplierDurationUpgradeRubyCost;

    void Start()
    {
        GameController.gameController.inventory = this;
        Invoke("InitializeItemsUpgrades", .2f);
    }

    private void InitializeItemsUpgrades()
    {
        //STAMINA POTION
        //Inserir aqui a puxada de informações do script onde estarão as informações da poção        
        StaminaPotionInitialization();
        UiStaminaPotionUpdate();

        //SHIELD
        ShieldInitialization();
        UIShieldChargeUpgrade();
        UIShieldDurationUpgrade();

        //COIN MULTIPLIER
        CoinMultiplierInitialization();
        UICoinMultiplierChargeUpgrade();
        UICoinMultiplierDurationUpgrade();
    }

    #region Item Initialization

    private void StaminaPotionInitialization()
    {
        staminaPotionUpgradeCoinCost = staminaData.coinChargeUpgradeCost[staminaPotionUpgradeLevel];
        staminaPotionUpgradeRubyCost = staminaData.rubyChargeUpgradeCost[staminaPotionUpgradeLevel];

        potionRestauration = staminaData.baseEffectCharges +
                    staminaPotionUpgradeLevel * staminaData.levelFactorUpgrade;

        GameController.gameController.playerPowers.
            InitializeStaminaPotion(potionRestauration);
    }

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

    private void CoinMultiplierInitialization()
    {
        
        coinMultiplierUpgradeCoinCost = coinMultiplierData.coinChargeUpgradeCost[coinMultiplierUpgradeLevel];
        coinMultiplierUpgradeRubyCost = coinMultiplierData.rubyChargeUpgradeCost[coinMultiplierUpgradeLevel];

        boostedCoinMultiplier = coinMultiplierData.baseEffectCharges +
                    coinMultiplierUpgradeLevel * coinMultiplierData.levelFactorUpgrade;

        coinMultiplierDurationUpgradeCoinCost = coinMultiplierDurationData.coinChargeUpgradeCost[coinMultiplierDurationUpgradeLevel];
        coinMultiplierDurationUpgradeRubyCost = coinMultiplierDurationData.rubyChargeUpgradeCost[coinMultiplierDurationUpgradeLevel];

        coinMultiplierDuration = coinMultiplierDurationData.baseEffectCharges +
            coinMultiplierDurationUpgradeLevel * coinMultiplierDurationData.levelFactorUpgrade;

        GameController.gameController.playerPowers.
            InitializeCoinMultiplier(boostedCoinMultiplier, coinMultiplierDuration);

    }


    #endregion

    #region Item Upgrades

    public void ItemUpgrade(int itemCode = 0)
    {
        if (itemCode == 1) PotionUpgrade();
        else if (itemCode == 2) UpgradeShieldCharges();
        else if (itemCode == 3) UpgradeShieldDuration();
        else if (itemCode == 4) UpgradeCoinMultiplier();
        else if (itemCode == 5) UpgradeCoinMultiplierDuration();
    }


    #region Potion Upgrade
    private void PotionUpgrade()
    {
        if (staminaPotionUpgradeLevel >= staminaData.maxLevel) return;
        //Também devo mudar o texto e a cor do botão neste caso, mas deixarei assim por enquanto

        staminaPotionUpgradeLevel++;

        staminaPotionUpgradeCoinCost = staminaData.coinChargeUpgradeCost[staminaPotionUpgradeLevel];
        staminaPotionUpgradeRubyCost = staminaData.rubyChargeUpgradeCost[staminaPotionUpgradeLevel];
        

        StaminaPotionInitialization();
        UiStaminaPotionUpdate();        
    }

    private void UiStaminaPotionUpdate()
    {
        GameController.gameController.uiController.
            UpdateStaminaPostionUpgradeUI((staminaPotionUpgradeLevel * staminaData.levelFactorUpgrade),
            staminaPotionUpgradeLevel, staminaPotionUpgradeCoinCost, staminaPotionUpgradeRubyCost);
    }

    #endregion
    
    #region Shield Upgrades
    private void UpgradeShieldCharges()
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
            shieldChargeUpgradeLevel, shieldChargeUpgradeCoinCost, shieldChargeUpgradeRubyCost, shieldUpgradeLevel);
    }

    private void UpgradeShieldDuration()
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
            shieldDurationUpgradeLevel, shieldDurationUpgradeCoinCost, shieldDurationUpgradeRubyCost, shieldUpgradeLevel);
    }


    #endregion

    #region Coin Multiplier

    private void UpgradeCoinMultiplier()
    {
        if (coinMultiplierUpgradeLevel >= coinMultiplierData.maxLevel) return;

        coinMultiplierUpgradeLevel++;
        coinMultiplierLevel++;

        coinMultiplierUpgradeCoinCost = coinMultiplierData.coinChargeUpgradeCost[coinMultiplierUpgradeLevel];
        coinMultiplierUpgradeRubyCost = coinMultiplierData.rubyChargeUpgradeCost[coinMultiplierUpgradeLevel];

        UICoinMultiplierChargeUpgrade();      
        CoinMultiplierInitialization();

    }

    private void UICoinMultiplierChargeUpgrade()
    {
        GameController.gameController.uiController.
            UpdateCoinMultiplierUpgradeUI((coinMultiplierUpgradeLevel * coinMultiplierData.levelFactorUpgrade),
            coinMultiplierUpgradeLevel, coinMultiplierUpgradeCoinCost, coinMultiplierUpgradeRubyCost, coinMultiplierLevel);
    }

    private void UpgradeCoinMultiplierDuration()
    {
        
        if (coinMultiplierDurationUpgradeLevel >= coinMultiplierDurationData.maxLevel) return;

        coinMultiplierDurationUpgradeLevel++;
        coinMultiplierLevel++;

        coinMultiplierDurationUpgradeCoinCost = coinMultiplierDurationData.coinChargeUpgradeCost[coinMultiplierDurationUpgradeLevel];
        coinMultiplierDurationUpgradeRubyCost = coinMultiplierDurationData.rubyChargeUpgradeCost[coinMultiplierDurationUpgradeLevel];

        UICoinMultiplierDurationUpgrade();      
        CoinMultiplierInitialization();
    }

    private void UICoinMultiplierDurationUpgrade()
    {
        GameController.gameController.uiController.
            UpdateCoinMultiplierDurationUpgradeUI((coinMultiplierDurationUpgradeLevel * 
                coinMultiplierDurationData.levelFactorUpgrade), coinMultiplierDurationUpgradeLevel,
                    coinMultiplierDurationUpgradeCoinCost, coinMultiplierDurationUpgradeRubyCost, coinMultiplierLevel);
    }


    #endregion

    #endregion
}
