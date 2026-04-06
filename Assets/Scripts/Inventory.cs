using UnityEngine;

public class Inventory : MonoBehaviour
{
    [Header("Shield")]
    [SerializeField] private float baseShieldDuration = 20f;
    private int shieldCharges = 1;    
    private float shieldDuration;    
    public int shieldDurationUpgrade = 0;
    public float shieldUpgradeFactor = 10f;
    public int shieldChargeUpgrade = 1;
    
    [Header("Stamina Potion")]
    [SerializeField] private float basePotionRestauration = 10f;
    private float potionRestauration;
    public int staminaPotionUpgrade = 0;
    public int staminaPotionUpgradeFactor = 5;
    public int staminaPotionUpgradeCost = 1000;    

    [Header("Coin Multiplier")]
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
        //STAMINA POTION
        //Inserir aqui a puxada de informações do script onde estarão as informações da poção
        UIStaminaPotionUpdate();
        StaminaPotionInitialization();

        //SHIELD
        ShieldInitialization();
    }

    #region Item Initialization

    private void ShieldInitialization()
    {
        shieldDuration = baseShieldDuration +
                    shieldDurationUpgrade * shieldUpgradeFactor;

        shieldCharges = shieldChargeUpgrade;

        GameController.gameController.playerPowers.
            InitializeShieldPower(shieldDuration, shieldCharges);
    }

    private void StaminaPotionInitialization()
    {
        potionRestauration = basePotionRestauration +
                    staminaPotionUpgrade * staminaPotionUpgradeFactor;

        GameController.gameController.playerPowers.
            InitializeShieldPower(shieldDuration, shieldCharges);
    }

    #endregion


    #region Item Upgrades

    #region Shield Upgrades

    public void UpgradeShieldDuration()
    {
        shieldDurationUpgrade++;        
    }

    public void UpgradeShieldCharges()
    {
        shieldChargeUpgrade++;
    }

    #endregion

    #region Potion Upgrade

    public void PotionUpgrade()
    {
        staminaPotionUpgrade++;
        //staminaPotionUpgradeCost *= (1 + staminaPotionUpgrade);
        staminaPotionUpgradeCost *= (2);

        UIStaminaPotionUpdate();
        GameController.gameController.playerPowers.InitializeStaminaPotion();
    }

    private void UIStaminaPotionUpdate()
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
