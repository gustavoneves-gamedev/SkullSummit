using UnityEngine;


[CreateAssetMenu(menuName = "Game/Character Data", fileName = "CharacterData")]

public class CharacterData : ScriptableObject
{
    [Header("ID")]
    public characterID characterID;

    [Header("Atributes")]
    public float baseMaxStamina;
    public float baseMovementSpeed;
    public float baseDamage;
    public float baseCooldown;
    public int baseAmmo;
    public float reloadTime;
    public float baseDefense;
    public float baseResistance;

}

