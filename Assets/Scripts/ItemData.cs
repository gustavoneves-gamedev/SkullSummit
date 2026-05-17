using UnityEngine;

[CreateAssetMenu(menuName = "Game/Item Data", fileName = "ItemData")]

public class ItemData : ScriptableObject
{
    [Header("ID")]
    public ItemID characterID;

    [Header("ID")]
    public int baseCoinCost;

    [Header("Charges")]
    public int baseEffectCharges;    
    public int levelFactorUpgrade;
    public int maxLevel;
    public int[] coinChargeUpgradeCost;
    public int[] rubyChargeUpgradeCost;   

}
