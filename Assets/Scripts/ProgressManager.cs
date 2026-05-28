using UnityEngine;

public class ProgressManager : MonoBehaviour
{
    public static ProgressManager progressManager;

    [Header("Stat Upgrades")]
    public float staminaIncrement;
    public float movementSpeedIncrement;
    public float damageIncrement;
    public float cooldownIncrement;
    public int ammoIncrement;
    public float reloadIncrement;
    public float defenseIncrement;
    public float resistanceIncrement;

    [Header("Stat Upgrades Data")]
    [SerializeField] private ItemData[] staminaData;
    [SerializeField] private ItemData[] defenseData;
    [SerializeField] private ItemData[] resistanceData;
    [SerializeField] private ItemData[] attackData;
    //[SerializeField] private ItemData[] staminaData;

    //Depois irei tornar private os Factor para limpar o Inspector
    //MELHOR! DEPOIS IREI PUXAR DIRETO DO DATA EM VEZ DESTAS VARIÁVEIS
    [Header("Cowboy Stat Upgrades")]
    public int cowboyStaminaUpgradeFactor = 10;
    public int cowboyStaminaUpgrades;
    public int cowboyMaxStaminaUpgrades = 5;
    public float cowboyMovementSpeedUpgradeFactor = 1f;
    public int cowboyMovementSpeedUpgrades;
    public int cowboyAttackUpgrades;
    public float cowboyDamageUpgradeFactor = 1f;
    public int cowboyDamageUpgrades;
    public float cowboyCooldownUpgradeFactor = 0.2f;
    public int cowboyCooldownUpgrades;
    public int cowboyAmmoUpgradeFactor = 1;
    public int cowboyAmmoUpgrades;
    public float cowboyReloadUpgradeFactor = 0.25f;
    public int cowboyReloadUpgrades;
    public float cowboyDefenseUpgradeFactor = 5f;
    public int cowboyDefenseUpgrades;
    public float cowboyResistanceUpgradeFactor = 2f;
    public int cowboyResistanceUpgrades;

    [Header("Samurai Stat Upgrades")]
    public float samuraiStaminaUpgradeFactor = 5f;
    public int samuraiStaminaUpgrades;
    public float samuraiMovementSpeedUpgradeFactor = 1f;
    public int samuraiMovementSpeedUpgrades;
    public float samuraiDamageUpgradeFactor = 1f;
    public int samuraiDamageUpgrades;
    public float samuraiCooldownUpgradeFactor = 0.2f;
    public int samuraiCooldownUpgrades;
    public int samuraiAmmoUpgradeFactor = 1;
    public int samuraiAmmoUpgrades;
    public float samuraiReloadUpgradeFactor = 0.25f;
    public int samuraiReloadUpgrades;
    public float samuraiDefenseUpgradeFactor = 5f;
    public int samuraiDefenseUpgrades;
    public float samuraiResistanceUpgradeFactor = 2f;
    public int samuraiResistanceUpgrades;

    [Header("Alpinista Stat Upgrades")]
    public float alpinistaStaminaUpgradeFactor = 5f;
    public int alpinistaStaminaUpgrades;
    public float alpinistaMovementSpeedUpgradeFactor = 1f;
    public int alpinistaMovementSpeedUpgrades;
    public float alpinistaDamageUpgradeFactor = 1f;
    public int alpinistaDamageUpgrades;
    public float alpinistaCooldownUpgradeFactor = 0.2f;
    public int alpinistaCooldownUpgrades;
    public int alpinistaAmmoUpgradeFactor = 1;
    public int alpinistaAmmoUpgrades;
    public float alpinistaReloadUpgradeFactor = 0.25f;
    public int alpinistaReloadUpgrades;
    public float alpinistaDefenseUpgradeFactor = 5f;
    public int alpinistaDefenseUpgrades;
    public float alpinistaResistanceUpgradeFactor = 2f;
    public int alpinistaResistanceUpgrades;



    private void Awake()
    {
        if (progressManager == null)
            progressManager = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        Invoke("Initialize", .5f);
    }

    private void Initialize()
    {
        UpdateCowboyStaminaUI();
    }



    #region Upgrade Buttons
    public void UpgradeStamina(int charCode)
    {
        if (charCode == 1) UpgradeCharacter(characterID.Cowboy, statType.Stamina);
        else if (charCode == 2) UpgradeCharacter(characterID.Samurai, statType.Stamina);
        else if (charCode == 3) UpgradeCharacter(characterID.Alpinista, statType.Stamina);
    }

    public void UpgradeDefense(int charCode)
    {
        if (charCode == 1) UpgradeCharacter(characterID.Cowboy, statType.Defense);
        else if (charCode == 2) UpgradeCharacter(characterID.Samurai, statType.Defense);
        else if (charCode == 3) UpgradeCharacter(characterID.Alpinista, statType.Defense);
    }

    public void UpgradeResistance(int charCode)
    {
        if (charCode == 1) UpgradeCharacter(characterID.Cowboy, statType.Resistance);
        else if (charCode == 2) UpgradeCharacter(characterID.Samurai, statType.Resistance);
        else if (charCode == 3) UpgradeCharacter(characterID.Alpinista, statType.Resistance);
    }

    public void UpgradeAttack(int charCode)
    {
        if (charCode == 1) UpgradeCharacter(characterID.Cowboy, statType.Resistance);
        else if (charCode == 2) UpgradeCharacter(characterID.Samurai, statType.Resistance);
        else if (charCode == 3) UpgradeCharacter(characterID.Alpinista, statType.Resistance);
    }

    #endregion

    public void UpgradeCharacter(characterID character, statType stat)
    {
        if (character == characterID.Cowboy)
            UpgradeCowboy(stat);

        if (character == characterID.Samurai)
            UpgradeSamurai(stat);

        if (character == characterID.Alpinista)
            UpgradeAlpinista(stat);
    }

    private void UpgradeCowboy(statType stat)
    {
        if (stat == statType.Stamina)
        {
            if (cowboyStaminaUpgrades >= staminaData[0].maxLevel) return;

            cowboyStaminaUpgrades++;
            UpdateCowboyStaminaUI();
        }

        if (stat == statType.Defense)
        {
            if (cowboyDefenseUpgrades >= defenseData[0].maxLevel) return;

            cowboyDefenseUpgrades++;
            //UpdateCowboyStaminaUI();
        }

        if (stat == statType.Resistance)
        {
            if (cowboyResistanceUpgrades >= resistanceData[0].maxLevel) return;

            cowboyResistanceUpgrades++;            
            //UpdateCowboyStaminaUI();
        }

        if (stat == statType.Resistance)
        {
            if (cowboyAttackUpgrades >= attackData[0].maxLevel) return;

            cowboyDamageUpgrades++;
            cowboyCooldownUpgrades++;
            cowboyAmmoUpgrades++;
            cowboyReloadUpgrades++;
            //UpdateCowboyStaminaUI();
        }

        //if (stat == statType.MovementSpeed) cowboyMovementSpeedUpgrades++;
        //if (stat == statType.Damage) cowboyDamageUpgrades++;
        //if (stat == statType.Cooldown) cowboyCooldownUpgrades++;
        //if (stat == statType.Ammo) cowboyAmmoUpgrades++;
        //if (stat == statType.ReloadTime) cowboyReloadUpgrades++;

        //INSERIR AQUI EVENTUAL ESCALA DE MELHORIAS. EX: Upgrade 1 melhora speed em 1, upgrade 2 melhora em 2...)

    }

    private void UpgradeSamurai(statType stat)
    {
        if (stat == statType.Stamina) samuraiStaminaUpgrades++;
        if (stat == statType.MovementSpeed) samuraiMovementSpeedUpgrades++;
        if (stat == statType.Damage) samuraiDamageUpgrades++;
        if (stat == statType.Cooldown) samuraiCooldownUpgrades++;
        if (stat == statType.Ammo) samuraiAmmoUpgrades++;
        if (stat == statType.ReloadTime) samuraiReloadUpgrades++;
        if (stat == statType.Defense) samuraiDefenseUpgrades++;
        if (stat == statType.Resistance) samuraiResistanceUpgrades++;

        //INSERIR AQUI EVENTUAL ESCALA DE MELHORIAS. EX: Upgrade 1 melhora speed em 1, upgrade 2 melhora em 2...)

    }

    private void UpgradeAlpinista(statType stat)
    {
        if (stat == statType.Stamina) alpinistaStaminaUpgrades++;
        if (stat == statType.MovementSpeed) alpinistaMovementSpeedUpgrades++;
        if (stat == statType.Damage) alpinistaDamageUpgrades++;
        if (stat == statType.Cooldown) alpinistaCooldownUpgrades++;
        if (stat == statType.Ammo) alpinistaAmmoUpgrades++;
        if (stat == statType.ReloadTime) alpinistaReloadUpgrades++;
        if (stat == statType.Defense) alpinistaDefenseUpgrades++;
        if (stat == statType.Resistance) alpinistaResistanceUpgrades++;

        //INSERIR AQUI EVENTUAL ESCALA DE MELHORIAS. EX: Upgrade 1 melhora speed em 1, upgrade 2 melhora em 2...)

    }

    private void UpdateCowboyStaminaUI()
    {
        GameController.gameController.uiController.UpdateCowboyStaminaUI(
                cowboyStaminaUpgrades * cowboyStaminaUpgradeFactor,
                cowboyStaminaUpgrades,
                staminaData[0].coinChargeUpgradeCost[cowboyStaminaUpgrades],
                staminaData[0].rubyChargeUpgradeCost[cowboyStaminaUpgrades],
                staminaData[0].maxLevel);
    }


    //EU DEVERIA TER CRIADO STATUS DIFERENTES PARA CADA PERSONAGEM! SE SOBRAR TEMPO, OTIMIZAR!!!
    #region Run Initialization

    public void UpdateIncrement(characterID character)
    {
        if (character == characterID.Cowboy)
        {
            staminaIncrement = cowboyStaminaUpgrades * cowboyStaminaUpgradeFactor;
            movementSpeedIncrement = cowboyMovementSpeedUpgrades * cowboyMovementSpeedUpgradeFactor;
            damageIncrement = cowboyDamageUpgrades * cowboyDamageUpgradeFactor;
            cooldownIncrement = cowboyCooldownUpgrades * cowboyCooldownUpgradeFactor;
            ammoIncrement = cowboyAmmoUpgrades * cowboyAmmoUpgradeFactor;
            reloadIncrement = cowboyReloadUpgrades * cowboyReloadUpgradeFactor;
            defenseIncrement = cowboyDefenseUpgrades * cowboyDefenseUpgradeFactor;
            resistanceIncrement = cowboyResistanceUpgrades * cowboyResistanceUpgradeFactor;
        }

        if (character == characterID.Samurai)
        {
            staminaIncrement = samuraiStaminaUpgrades * samuraiStaminaUpgradeFactor;
            movementSpeedIncrement = samuraiMovementSpeedUpgrades * samuraiMovementSpeedUpgradeFactor;
            damageIncrement = samuraiDamageUpgrades * samuraiDamageUpgradeFactor;
            cooldownIncrement = samuraiCooldownUpgrades * samuraiCooldownUpgradeFactor;
            ammoIncrement = samuraiAmmoUpgrades * samuraiAmmoUpgradeFactor;
            reloadIncrement = samuraiReloadUpgrades * samuraiReloadUpgradeFactor;
            defenseIncrement = samuraiDefenseUpgrades * samuraiDefenseUpgradeFactor;
            resistanceIncrement = samuraiResistanceUpgrades * samuraiResistanceUpgradeFactor;
        }

        if (character == characterID.Alpinista)
        {
            staminaIncrement = alpinistaStaminaUpgrades * alpinistaStaminaUpgradeFactor;
            movementSpeedIncrement = alpinistaMovementSpeedUpgrades * alpinistaMovementSpeedUpgradeFactor;
            damageIncrement = alpinistaDamageUpgrades * alpinistaDamageUpgradeFactor;
            cooldownIncrement = alpinistaCooldownUpgrades * alpinistaCooldownUpgradeFactor;
            ammoIncrement = alpinistaAmmoUpgrades * alpinistaAmmoUpgradeFactor;
            reloadIncrement = alpinistaReloadUpgrades * alpinistaReloadUpgradeFactor;
            defenseIncrement = alpinistaDefenseUpgrades * alpinistaDefenseUpgradeFactor;
            resistanceIncrement = alpinistaResistanceUpgrades * alpinistaResistanceUpgradeFactor;
        }
    }

    #endregion

}
