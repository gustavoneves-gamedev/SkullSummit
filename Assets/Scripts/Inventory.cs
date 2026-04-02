using UnityEngine;

public class Inventory : MonoBehaviour
{
    [Header("Shield")]
    public int shieldDurationUpgrade = 0;
    public float shieldUpgradeFactor = 10f;
    public int shieldChargeUpgrade = 1;
    
    [Header("Stamina Potion")]
    public int staminaPotionUpgrade = 0;
    public float staminaPotionUpgradeFactor = 10f;

    [Header("Coin Multiplier")]
    public int coinDurationUpgrade = 0;
    public float coinDurationUpgradeFactor = 8f;
    public int coinMultiplierUpgrade = 0;
    public float coinMultiplierUpgradeFactor = 0.5f;

    void Start()
    {
        GameController.gameController.inventory = this;
        InitializeItemsUpgrades();
    }

    private void InitializeItemsUpgrades()
    {
        
    }

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
