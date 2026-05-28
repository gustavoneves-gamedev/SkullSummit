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
    public int cowboyDefenseUpgradeFactor = 3;
    public int cowboyDefenseUpgrades;
    public int cowboyResistanceUpgradeFactor = 2;
    public int cowboyResistanceUpgrades;
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


    [Header("Samurai Stat Upgrades")]
    public int samuraiStaminaUpgradeFactor = 5;
    public int samuraiStaminaUpgrades;
    public float samuraiMovementSpeedUpgradeFactor = 1f;
    public int samuraiMovementSpeedUpgrades;
    private int samuraiAttackUpgrades;
    public float samuraiDamageUpgradeFactor = 1f;
    public int samuraiDamageUpgrades;
    public float samuraiCooldownUpgradeFactor = 0.2f;
    public int samuraiCooldownUpgrades;
    public int samuraiAmmoUpgradeFactor = 1;
    public int samuraiAmmoUpgrades;
    public float samuraiReloadUpgradeFactor = 0.25f;
    public int samuraiReloadUpgrades;
    public int samuraiDefenseUpgradeFactor = 5;
    public int samuraiDefenseUpgrades;
    public int samuraiResistanceUpgradeFactor = 2;
    public int samuraiResistanceUpgrades;

    [Header("Alpinista Stat Upgrades")]
    public int alpinistaStaminaUpgradeFactor = 5;
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
    public int alpinistaDefenseUpgradeFactor = 5;
    public int alpinistaDefenseUpgrades;
    public int alpinistaResistanceUpgradeFactor = 2;
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
        UpdateCowboyDefenseUI();
        UpdateCowboyResistanceUI();
        UpdateCowboyAttackUI();
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
        if (charCode == 1) UpgradeCharacter(characterID.Cowboy, statType.Damage);
        else if (charCode == 2) UpgradeCharacter(characterID.Samurai, statType.Damage);
        else if (charCode == 3) UpgradeCharacter(characterID.Alpinista, statType.Damage);
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
            UpdateCowboyDefenseUI();
        }

        if (stat == statType.Resistance)
        {
            if (cowboyResistanceUpgrades >= resistanceData[0].maxLevel) return;

            cowboyResistanceUpgrades++;
            UpdateCowboyResistanceUI();
        }

        //O nome está incorreto uma vez que o ataque melhora vários aspectos
        if (stat == statType.Damage)
        {
            if (cowboyAttackUpgrades >= attackData[0].maxLevel) return;

            cowboyAttackUpgrades++;

            if (cowboyAttackUpgrades % 2 == 0) cowboyAmmoUpgrades++;

            cowboyReloadUpgrades++;
            UpdateCowboyAttackUI();
        }

        
    }

    private void UpgradeSamurai(statType stat)
    {
        //if (stat == statType.Stamina) samuraiStaminaUpgrades++;
        //if (stat == statType.MovementSpeed) samuraiMovementSpeedUpgrades++;
        //if (stat == statType.Damage) samuraiDamageUpgrades++;
        //if (stat == statType.Cooldown) samuraiCooldownUpgrades++;
        //if (stat == statType.Ammo) samuraiAmmoUpgrades++;
        //if (stat == statType.ReloadTime) samuraiReloadUpgrades++;
        //if (stat == statType.Defense) samuraiDefenseUpgrades++;
        //if (stat == statType.Resistance) samuraiResistanceUpgrades++;

        if (stat == statType.Stamina)
        {
            if (samuraiStaminaUpgrades >= staminaData[1].maxLevel) return;

            samuraiStaminaUpgrades++;
            UpdateSamuraiStaminaUI();
        }

        if (stat == statType.Defense)
        {
            if (samuraiDefenseUpgrades >= defenseData[1].maxLevel) return;

            samuraiDefenseUpgrades++;
            UpdateSamuraiDefenseUI();
        }

        if (stat == statType.Resistance)
        {
            if (samuraiResistanceUpgrades >= resistanceData[1].maxLevel) return;

            samuraiResistanceUpgrades++;
            UpdateSamuraiResistanceUI();
        }

        //O nome está incorreto uma vez que o ataque melhora vários aspectos
        if (stat == statType.Damage)
        {
            if (samuraiAttackUpgrades >= attackData[1].maxLevel) return;

            samuraiAttackUpgrades++;

            if (samuraiAttackUpgrades % 2 == 0) samuraiAmmoUpgrades++;            

            samuraiReloadUpgrades++;
            UpdateSamuraiAttackUI();
        }

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

    #region UI Update

    #region Cowboy Upgrade UI
    private void UpdateCowboyStaminaUI()
    {
        GameController.gameController.uiController.UpdateCowboyStaminaUI(
                cowboyStaminaUpgrades * cowboyStaminaUpgradeFactor,
                cowboyStaminaUpgrades,
                staminaData[0].coinChargeUpgradeCost[cowboyStaminaUpgrades],
                staminaData[0].rubyChargeUpgradeCost[cowboyStaminaUpgrades],
                staminaData[0].maxLevel);
    }

    private void UpdateCowboyDefenseUI()
    {
        GameController.gameController.uiController.UpdateCowboyDefenseUI(
                cowboyDefenseUpgrades * cowboyDefenseUpgradeFactor,
                cowboyDefenseUpgrades,
                defenseData[0].coinChargeUpgradeCost[cowboyDefenseUpgrades],
                defenseData[0].rubyChargeUpgradeCost[cowboyDefenseUpgrades],
                defenseData[0].maxLevel);
    }

    private void UpdateCowboyResistanceUI()
    {
        GameController.gameController.uiController.UpdateCowboyResistanceUI(
                cowboyResistanceUpgrades * cowboyResistanceUpgradeFactor,
                cowboyResistanceUpgrades,
                resistanceData[0].coinChargeUpgradeCost[cowboyResistanceUpgrades],
                resistanceData[0].rubyChargeUpgradeCost[cowboyResistanceUpgrades],
                resistanceData[0].maxLevel);
    }

    private void UpdateCowboyAttackUI()
    {
        GameController.gameController.uiController.UpdateCowboyAttackUI(
                cowboyAmmoUpgrades + cowboyReloadUpgrades,
                cowboyAttackUpgrades,
                attackData[0].coinChargeUpgradeCost[cowboyAttackUpgrades],
                attackData[0].rubyChargeUpgradeCost[cowboyAttackUpgrades],
                attackData[0].maxLevel);
    }

    #endregion

    #region Samurai Upgrade UI
    private void UpdateSamuraiStaminaUI()
    {
        GameController.gameController.uiController.UpdateSamuraiStaminaUI(
                samuraiStaminaUpgrades * samuraiStaminaUpgradeFactor,
                samuraiStaminaUpgrades,
                staminaData[1].coinChargeUpgradeCost[samuraiStaminaUpgrades],
                staminaData[1].rubyChargeUpgradeCost[samuraiStaminaUpgrades],
                staminaData[1].maxLevel);
    }

    private void UpdateSamuraiDefenseUI()
    {
        GameController.gameController.uiController.UpdateSamuraiDefenseUI(
                samuraiDefenseUpgrades * samuraiDefenseUpgradeFactor,
                samuraiDefenseUpgrades,
                defenseData[1].coinChargeUpgradeCost[samuraiDefenseUpgrades],
                defenseData[1].rubyChargeUpgradeCost[samuraiDefenseUpgrades],
                defenseData[1].maxLevel);
    }

    private void UpdateSamuraiResistanceUI()
    {
        GameController.gameController.uiController.UpdateSamuraiResistanceUI(
                samuraiResistanceUpgrades * samuraiResistanceUpgradeFactor,
                samuraiResistanceUpgrades,
                resistanceData[1].coinChargeUpgradeCost[samuraiResistanceUpgrades],
                resistanceData[1].rubyChargeUpgradeCost[samuraiResistanceUpgrades],
                resistanceData[1].maxLevel);
    }

    private void UpdateSamuraiAttackUI()
    {
        GameController.gameController.uiController.UpdateSamuraiAttackUI(
                samuraiAmmoUpgrades + samuraiReloadUpgrades,
                samuraiAttackUpgrades,
                attackData[1].coinChargeUpgradeCost[samuraiAttackUpgrades],
                attackData[1].rubyChargeUpgradeCost[samuraiAttackUpgrades],
                attackData[1].maxLevel);
    }

    #endregion

    #endregion


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
