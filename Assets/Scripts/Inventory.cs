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
        //SHIELD
        ShieldInitialization();
        UIShieldChargeUpgrade();
        UIShieldDurationUpgrade();

        //STAMINA POTION
        //Inserir aqui a puxada de informações do script onde estarão as informações da poção        
        UiStaminaPotionUpdate();
        StaminaPotionInitialization();

        //COIN MULTIPLIER
        CoinMultiplierInitialization();
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

    public void UpgradeCoinMultiplier()
    {
        if (coinMultiplierUpgradeLevel >= coinMultiplierData.maxLevel) return;

        coinMultiplierUpgradeLevel++;
        coinMultiplierLevel++;

        coinMultiplierUpgradeCoinCost = coinMultiplierData.coinChargeUpgradeCost[coinMultiplierUpgradeLevel];
        coinMultiplierUpgradeRubyCost = coinMultiplierData.rubyChargeUpgradeCost[coinMultiplierUpgradeLevel];

        //UIShieldChargeUpgrade(); - Criar função para atualizar a HUD posteriormente       
        CoinMultiplierInitialization();

    }

    public void UpgradeCoinDuration()
    {
        
        if (coinMultiplierDurationUpgradeLevel >= coinMultiplierDurationData.maxLevel) return;

        coinMultiplierDurationUpgradeLevel++;
        coinMultiplierLevel++;

        coinMultiplierDurationUpgradeCoinCost = coinMultiplierDurationData.coinChargeUpgradeCost[coinMultiplierDurationUpgradeLevel];
        coinMultiplierDurationUpgradeRubyCost = coinMultiplierDurationData.rubyChargeUpgradeCost[coinMultiplierDurationUpgradeLevel];

        //UIShieldChargeUpgrade(); - Criar função para atualizar a HUD posteriormente       
        CoinMultiplierInitialization();
    }

    

    #endregion

    #endregion
}
