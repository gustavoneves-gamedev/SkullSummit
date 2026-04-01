using UnityEngine;

public class Inventory : MonoBehaviour
{
    [Header("Stamina Potion")]
    public int staminaPotionUpgrade;
    public float staminaPotionUpgradeFactor = 10f;


    void Start()
    {
        GameController.gameController.inventory = this;
        InitializeItemsUpgrades();
    }

    private void InitializeItemsUpgrades()
    {
        
    }
    

    #region Item Upgrades


    #endregion
}
