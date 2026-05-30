using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{

    [Header("MainMenu")]
    [SerializeField] private GameObject mainMenu;//É tudo no Menu Principal
    //[SerializeField] private GameObject menu;
    [SerializeField] private GameObject tapToPlayText;

    [Header("Options")]
    [SerializeField] private GameObject optionsMenu;
    [SerializeField] private GameObject volumeMenu;


    [Header("TopMenu")]
    [SerializeField] private GameObject topMenu;
    [SerializeField] private TextMeshProUGUI coins;
    [SerializeField] private TextMeshProUGUI rubies;

    [Header("Leaderboard")]
    [SerializeField] private GameObject leaderboard;
    [SerializeField] private TextMeshProUGUI[] playerNames;
    [SerializeField] private TextMeshProUGUI[] playerHeights;

    [Header("CharacterMenu")]
    [SerializeField] private GameObject characterSelectionMenu;
    [SerializeField] private GameObject[] characterMenuArray;
    private int charCode = 0;
    private bool isCharSelecting;

    [Header("CharacterUpgradeMenu")]
    #region Character Upgrades
    #region Cowboy Upgrades
    [Header("Cowboy Stamina")]
    [SerializeField] private TextMeshProUGUI cowboyStaminaIndicator;
    [SerializeField] private TextMeshProUGUI cowboyStaminaLevel;    
    [SerializeField] private Slider cowboyStaminaVisualUpgrade;
    [SerializeField] private GameObject cowboyStaminaCost;
    [SerializeField] private TextMeshProUGUI cowboyStaminaUpgradeCoinCost;
    [SerializeField] private GameObject cowboyStaminaRubyCost;
    [SerializeField] private TextMeshProUGUI cowboyStaminaUpgradeRubyCost;
    [SerializeField] private GameObject cowboyStaminaMaxLevelImage;

    [Header("Cowboy Defense")]
    [SerializeField] private TextMeshProUGUI cowboyDefenseIndicator;
    [SerializeField] private TextMeshProUGUI cowboyDefenseLevel;
    [SerializeField] private Slider cowboyDefenseVisualUpgrade;
    [SerializeField] private GameObject cowboyDefenseCost;
    [SerializeField] private TextMeshProUGUI cowboyDefenseUpgradeCoinCost;
    [SerializeField] private GameObject cowboyDefenseRubyCost;
    [SerializeField] private TextMeshProUGUI cowboyDefenseUpgradeRubyCost;
    [SerializeField] private GameObject cowboyDefenseMaxLevelImage;

    [Header("Cowboy Resistance")]
    [SerializeField] private TextMeshProUGUI cowboyResistanceIndicator;
    [SerializeField] private TextMeshProUGUI cowboyResistanceLevel;
    [SerializeField] private Slider cowboyResistanceVisualUpgrade;
    [SerializeField] private GameObject cowboyResistanceCost;
    [SerializeField] private TextMeshProUGUI cowboyResistanceUpgradeCoinCost;
    [SerializeField] private GameObject cowboyResistanceRubyCost;
    [SerializeField] private TextMeshProUGUI cowboyResistanceUpgradeRubyCost;
    [SerializeField] private GameObject cowboyResistanceMaxLevelImage;

    [Header("Cowboy Attack")]
    [SerializeField] private TextMeshProUGUI cowboyAttackIndicator;
    [SerializeField] private TextMeshProUGUI cowboyAttackLevel;
    [SerializeField] private Slider cowboyAttackVisualUpgrade;
    [SerializeField] private GameObject cowboyAttackCost;
    [SerializeField] private TextMeshProUGUI cowboyAttackUpgradeCoinCost;
    [SerializeField] private GameObject cowboyAttackRubyCost;
    [SerializeField] private TextMeshProUGUI cowboyAttackUpgradeRubyCost;
    [SerializeField] private GameObject cowboyAttackMaxLevelImage;
    #endregion

    #region Samurai Upgrades
    [Header("Samurai Stamina")]
    [SerializeField] private TextMeshProUGUI samuraiStaminaIndicator;
    [SerializeField] private TextMeshProUGUI samuraiStaminaLevel;
    [SerializeField] private Slider samuraiStaminaVisualUpgrade;
    [SerializeField] private GameObject samuraiStaminaCost;
    [SerializeField] private TextMeshProUGUI samuraiStaminaUpgradeCoinCost;
    [SerializeField] private GameObject samuraiStaminaRubyCost;
    [SerializeField] private TextMeshProUGUI samuraiStaminaUpgradeRubyCost;
    [SerializeField] private GameObject samuraiStaminaMaxLevelImage;

    [Header("Samurai Defense")]
    [SerializeField] private TextMeshProUGUI samuraiDefenseIndicator;
    [SerializeField] private TextMeshProUGUI samuraiDefenseLevel;
    [SerializeField] private Slider samuraiDefenseVisualUpgrade;
    [SerializeField] private GameObject samuraiDefenseCost;
    [SerializeField] private TextMeshProUGUI samuraiDefenseUpgradeCoinCost;
    [SerializeField] private GameObject samuraiDefenseRubyCost;
    [SerializeField] private TextMeshProUGUI samuraiDefenseUpgradeRubyCost;
    [SerializeField] private GameObject samuraiDefenseMaxLevelImage;

    [Header("Samurai Resistance")]
    [SerializeField] private TextMeshProUGUI samuraiResistanceIndicator;
    [SerializeField] private TextMeshProUGUI samuraiResistanceLevel;
    [SerializeField] private Slider samuraiResistanceVisualUpgrade;
    [SerializeField] private GameObject samuraiResistanceCost;
    [SerializeField] private TextMeshProUGUI samuraiResistanceUpgradeCoinCost;
    [SerializeField] private GameObject samuraiResistanceRubyCost;
    [SerializeField] private TextMeshProUGUI samuraiResistanceUpgradeRubyCost;
    [SerializeField] private GameObject samuraiResistanceMaxLevelImage;

    [Header("Samurai Attack")]
    [SerializeField] private TextMeshProUGUI samuraiAttackIndicator;
    [SerializeField] private TextMeshProUGUI samuraiAttackLevel;
    [SerializeField] private Slider samuraiAttackVisualUpgrade;
    [SerializeField] private GameObject samuraiAttackCost;
    [SerializeField] private TextMeshProUGUI samuraiAttackUpgradeCoinCost;
    [SerializeField] private GameObject samuraiAttackRubyCost;
    [SerializeField] private TextMeshProUGUI samuraiAttackUpgradeRubyCost;
    [SerializeField] private GameObject samuraiAttackMaxLevelImage;
    #endregion

    #region Dullahan Upgrades
    [Header("Dullahan Stamina")]
    [SerializeField] private TextMeshProUGUI dullahanStaminaIndicator;
    [SerializeField] private TextMeshProUGUI dullahanStaminaLevel;
    [SerializeField] private Slider dullahanStaminaVisualUpgrade;
    [SerializeField] private GameObject dullahanStaminaCost;
    [SerializeField] private TextMeshProUGUI dullahanStaminaUpgradeCoinCost;
    [SerializeField] private GameObject dullahanStaminaRubyCost;
    [SerializeField] private TextMeshProUGUI dullahanStaminaUpgradeRubyCost;
    [SerializeField] private GameObject dullahanStaminaMaxLevelImage;

    [Header("Dullahan Defense")]
    [SerializeField] private TextMeshProUGUI dullahanDefenseIndicator;
    [SerializeField] private TextMeshProUGUI dullahanDefenseLevel;
    [SerializeField] private Slider dullahanDefenseVisualUpgrade;
    [SerializeField] private GameObject dullahanDefenseCost;
    [SerializeField] private TextMeshProUGUI dullahanDefenseUpgradeCoinCost;
    [SerializeField] private GameObject dullahanDefenseRubyCost;
    [SerializeField] private TextMeshProUGUI dullahanDefenseUpgradeRubyCost;
    [SerializeField] private GameObject dullahanDefenseMaxLevelImage;

    [Header("Dullahan Resistance")]
    [SerializeField] private TextMeshProUGUI dullahanResistanceIndicator;
    [SerializeField] private TextMeshProUGUI dullahanResistanceLevel;
    [SerializeField] private Slider dullahanResistanceVisualUpgrade;
    [SerializeField] private GameObject dullahanResistanceCost;
    [SerializeField] private TextMeshProUGUI dullahanResistanceUpgradeCoinCost;
    [SerializeField] private GameObject dullahanResistanceRubyCost;
    [SerializeField] private TextMeshProUGUI dullahanResistanceUpgradeRubyCost;
    [SerializeField] private GameObject dullahanResistanceMaxLevelImage;

    [Header("Dullahan Attack")]
    [SerializeField] private TextMeshProUGUI dullahanAttackIndicator;
    [SerializeField] private TextMeshProUGUI dullahanAttackLevel;
    [SerializeField] private Slider dullahanAttackVisualUpgrade;
    [SerializeField] private GameObject dullahanAttackCost;
    [SerializeField] private TextMeshProUGUI dullahanAttackUpgradeCoinCost;
    [SerializeField] private GameObject dullahanAttackRubyCost;
    [SerializeField] private TextMeshProUGUI dullahanAttackUpgradeRubyCost;
    [SerializeField] private GameObject dullahanAttackMaxLevelImage;
    #endregion

    #endregion


    [Header("LevelMenu")]
    [SerializeField] private GameObject levelSelectionMenu;
    [SerializeField] private GameObject[] levelMenuArray;
    private int levelCode = 0;
    private bool isLevelSelecting;

    [Header("ShopMenu")]
    [SerializeField] private GameObject shopMenu;
    [SerializeField] private GameObject purchaseMenu;
    [SerializeField] private GameObject itemUpgradeMenu;

    [Header("Items Purchases")]
    #region Items Purchases
    [Header("ShopMenu - Resurrection Amulet")]
    [SerializeField] private TextMeshProUGUI resurrectionAmuletPurchaseDescription;
    [SerializeField] private GameObject resurrectionAmuletPurchaseCost;
    [SerializeField] private TextMeshProUGUI resurrectionAmuletPurchaseCoinCost;
    [SerializeField] private GameObject resurrectionAmuletPurchaseRubyCost;
    [SerializeField] private TextMeshProUGUI resurrectionAmuletPurchaseRubyCostValue;
    [SerializeField] private GameObject resurrectionAmuletMaxQuantityImage;

    [Header("ShopMenu - Special Boost")]
    [SerializeField] private TextMeshProUGUI specialBoostPurchaseDescription;
    [SerializeField] private GameObject specialBoostPurchaseCost;
    [SerializeField] private TextMeshProUGUI specialBoostPurchaseCoinCost;
    [SerializeField] private GameObject specialBoostPurchaseRubyCost;
    [SerializeField] private TextMeshProUGUI specialBoostPurchaseRubyCostValue;
    [SerializeField] private GameObject specialBoostMaxQuantityImage;

    [Header("ShopMenu - Adrenaline")]
    [SerializeField] private TextMeshProUGUI adrenalinePurchaseDescription;
    [SerializeField] private GameObject adrenalinePurchaseCost;
    [SerializeField] private TextMeshProUGUI adrenalinePurchaseCoinCost;
    [SerializeField] private GameObject adrenalinePurchaseRubyCost;
    [SerializeField] private TextMeshProUGUI adrenalinePurchaseRubyCostValue;
    [SerializeField] private GameObject adrenalineMaxQuantityImage;
    #endregion

    [Header("Items Upgrade")]
    #region Items Upgrade
    [Header("ShopMenu - Stamina Potion")]
    [SerializeField] private TextMeshProUGUI staminaPotionName;
    [SerializeField] private TextMeshProUGUI staminaPotionLevel;
    [SerializeField] private TextMeshProUGUI staminaPotionUpgradedIndicator;
    [SerializeField] private Slider staminaPotionVisualUpgrade;
    [SerializeField] private GameObject staminaPotionCost;
    [SerializeField] private TextMeshProUGUI staminaPotionUpgradeCoinCost;
    [SerializeField] private GameObject staminaPotionRubyCost;
    [SerializeField] private TextMeshProUGUI staminaPotionUpgradeRubyCost;
    [SerializeField] private GameObject staminaPotionMaxLevelImage;

    [Header("ShopMenu - Shield Charges")]
    [SerializeField] private TextMeshProUGUI shieldChargeName;
    [SerializeField] private TextMeshProUGUI shieldChargeLevel;
    [SerializeField] private TextMeshProUGUI shieldChargeUpgradedIndicator;
    [SerializeField] private Slider shieldChargeVisualUpgrade;
    [SerializeField] private GameObject shieldChargeCost;
    [SerializeField] private TextMeshProUGUI shieldChargeUpgradeCoinCost;
    [SerializeField] private GameObject shieldChargeRubyCost;
    [SerializeField] private TextMeshProUGUI shieldChargeUpgradeRubyCost;
    [SerializeField] private GameObject shieldChargeMaxLevelImage;

    [Header("ShopMenu - Shield Duration")]
    [SerializeField] private TextMeshProUGUI shieldDurationName;
    [SerializeField] private TextMeshProUGUI shieldDurationLevel;
    [SerializeField] private TextMeshProUGUI shieldDurationUpgradedIndicator;
    [SerializeField] private Slider shieldDurationVisualUpgrade;
    [SerializeField] private GameObject shieldDurationCost;
    [SerializeField] private TextMeshProUGUI shieldDurationUpgradeCoinCost;
    [SerializeField] private GameObject shieldDurationRubyCost;
    [SerializeField] private TextMeshProUGUI shieldDurationUpgradeRubyCost;
    [SerializeField] private GameObject shieldDurationMaxLevelImage;

    [Header("ShopMenu - Coin Multiplier")]
    [SerializeField] private TextMeshProUGUI coinMultiplierChargeName;
    [SerializeField] private TextMeshProUGUI coinMultiplierChargeLevel;
    [SerializeField] private TextMeshProUGUI coinMultiplierChargeUpgradedIndicator;
    [SerializeField] private Slider coinMultiplierChargeVisualUpgrade;
    [SerializeField] private GameObject coinMultiplierChargeCost;
    [SerializeField] private TextMeshProUGUI coinMultiplierChargeUpgradeCoinCost;
    [SerializeField] private GameObject coinMultiplierChargeRubyCost;
    [SerializeField] private TextMeshProUGUI coinMultiplierChargeUpgradeRubyCost;
    [SerializeField] private GameObject coinMultiplierChargeMaxLevelImage;

    [Header("ShopMenu - Coin Multiplier Duration")]
    [SerializeField] private TextMeshProUGUI coinMultiplierDurationName;
    [SerializeField] private TextMeshProUGUI coinMultiplierDurationLevel;
    [SerializeField] private TextMeshProUGUI coinMultiplierDurationUpgradedIndicator;
    [SerializeField] private Slider coinMultiplierDurationVisualUpgrade;
    [SerializeField] private GameObject coinMultiplierDurationCost;
    [SerializeField] private TextMeshProUGUI coinMultiplierDurationUpgradeCoinCost;
    [SerializeField] private GameObject coinMultiplierDurationRubyCost;
    [SerializeField] private TextMeshProUGUI coinMultiplierDurationUpgradeRubyCost;
    [SerializeField] private GameObject coinMultiplierDurationMaxLevelImage;

    [Header("ShopMenu - Resurrection Amulet")]
    [SerializeField] private TextMeshProUGUI resurrectionAmuletName;    
    [SerializeField] private TextMeshProUGUI resurrectionAmuletLevel;
    [SerializeField] private TextMeshProUGUI resurrectionAmuletUpgradedIndicator;
    [SerializeField] private Slider resurrectionAmuletVisualUpgrade;
    [SerializeField] private GameObject resurrectionAmuletCost;
    [SerializeField] private TextMeshProUGUI resurrectionAmuletUpgradeCoinCost;
    [SerializeField] private GameObject resurrectionAmuletRubyCost;
    [SerializeField] private TextMeshProUGUI resurrectionAmuletUpgradeRubyCost;
    [SerializeField] private GameObject resurrectionAmuletMaxLevelImage;

    [Header("ShopMenu - Special Boost")]
    [SerializeField] private TextMeshProUGUI specialBoostName;
    [SerializeField] private TextMeshProUGUI specialBoostLevel;
    [SerializeField] private TextMeshProUGUI specialBoostUpgradedIndicator;
    [SerializeField] private Slider specialBoostVisualUpgrade;
    [SerializeField] private GameObject specialBoostCost;
    [SerializeField] private TextMeshProUGUI specialBoostUpgradeCoinCost;
    [SerializeField] private GameObject specialBoostRubyCost;
    [SerializeField] private TextMeshProUGUI specialBoostUpgradeRubyCost;
    [SerializeField] private GameObject specialBoostMaxLevelImage;

    [Header("ShopMenu - Adrenaline")]
    [SerializeField] private TextMeshProUGUI adrenalineName;
    [SerializeField] private TextMeshProUGUI adrenalineLevel;
    [SerializeField] private TextMeshProUGUI adrenalineUpgradedIndicator;
    [SerializeField] private Slider adrenalineVisualUpgrade;
    [SerializeField] private GameObject adrenalineCost;
    [SerializeField] private TextMeshProUGUI adrenalineUpgradeCoinCost;
    [SerializeField] private GameObject adrenalineRubyCost;
    [SerializeField] private TextMeshProUGUI adrenalineUpgradeRubyCost;
    [SerializeField] private GameObject adrenalineMaxLevelImage;

    #endregion

    [Header("Run")]
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject HUD;
    [SerializeField] private TextMeshProUGUI runCoins;
    [SerializeField] private TextMeshProUGUI runHeightClimbed;

    [Header("Death Menu")]
    [SerializeField] private GameObject resurrectionMenu;
    [SerializeField] private Button resurrectionButton;
    [SerializeField] private TextMeshProUGUI amuletQuantity;

    [Header("Ammo")]
    [SerializeField] private GameObject[] ammoType;
    [SerializeField] private Slider[] cowboyAmmo;
    [SerializeField] private Slider[] samuraiAmmo;
    [SerializeField] private Slider[] mummyAmmo;
    private Slider[] activeSliderArray;

    [Header("Stamina")]
    [SerializeField] private Slider staminaSlider;
    [SerializeField] private Image staminaBackground;
    [SerializeField] private Image staminaFill;
    [SerializeField] private Color green;
    [SerializeField] private Color darkGreen;
    [SerializeField] private Color greenYellow;
    [SerializeField] private Color darkGreenYellow;
    [SerializeField] private Color yellow;
    [SerializeField] private Color darkYellow;
    [SerializeField] private Color orange;
    [SerializeField] private Color darkOrange;
    [SerializeField] private Color red;
    [SerializeField] private Color darkRed;

    [Header("Special")]
    [SerializeField] private Slider specialSlider;
    [SerializeField] private Image specialBackground;
    [SerializeField] private Image specialFill;


    [Header("Stats Menu")]
    [SerializeField] private GameObject statsMenu;
    [SerializeField] private TextMeshProUGUI result;
    [SerializeField] private TextMeshProUGUI heightClimbed;
    [SerializeField] private TextMeshProUGUI coinsCollected;
    [SerializeField] private TextMeshProUGUI rubiesCollected;
    [SerializeField] private TextMeshProUGUI obstacles;


    [Header("Reference")]
    public PlayerRoot playerRoot;

    void Start()
    {
        GameController.gameController.uiController = this;
        mainMenu.SetActive(true);
        topMenu.SetActive(true);
        pauseMenu.SetActive(false);
        resurrectionMenu.SetActive(false);
        characterSelectionMenu.SetActive(false);
        levelSelectionMenu.SetActive(false);
        HUD.SetActive(false);
        leaderboard.SetActive(false);
        shopMenu.SetActive(false);

        GameController.gameController.UpdateLeaderboarUI();

        Invoke("Initialize", .1f);
    }

    void Update()
    {
        if (!GameController.gameController.isRunning) return;

        UpdateHeightClimbed();
        UpdateStaminaHUD(playerRoot.currentStamina / playerRoot.maxStamina);
        UpdateSpecialHUD(playerRoot.playerPowers.currentSpecial / playerRoot.playerPowers.maxSpecial);
    }

    private void Initialize()//COMENTÁRIOS IMPORTANTES AQUI
    {
        playerRoot = GameController.gameController.playerRoot;
        //coins.text = puxar informação do local de salvamento
        //InitializeStore(); //puxar informações do local de salvamento
        TopMainMenuUpdate();
    }

    #region General Menu

    public void BeginRun()
    {
        mainMenu.SetActive(false);
        statsMenu.SetActive(false);
        topMenu.SetActive(false);

        GameController.gameController.InitilizeLevelStatics();
        GameController.gameController.BeginRun();

        HUD.SetActive(true); //Colocar um efeito de fade in aqui 
        AudioController.audioController.SwitchMusic(1);
    }

    public void TopMainMenuUpdate()
    {
        coins.text = GameController.gameController.coins.ToString();
    }

    #region Options Menu
    public void ActivateOptionsMenu()
    {
        optionsMenu.SetActive(true);
    }

    public void ReturnToOptionsMenu()
    {
        volumeMenu.SetActive(false);
    }

    public void VolumeMenu()
    {
        volumeMenu.SetActive(true);
    }

    public void DeactivateOptionsMenu()
    {
        optionsMenu.SetActive(false);
    }

    #endregion

    #region Pause Menu
    public void PauseMenu()
    {
        pauseMenu.SetActive(true);

        playerRoot.canRun = false;
        playerRoot.isGamePaused = true;

        Time.timeScale = 0;
    }

    public void EndRunChoiceMenu(int quantity = 0)
    {
        resurrectionMenu.SetActive(true);
        amuletQuantity.text = "x" + quantity;

        if (playerRoot.playerPowers.hasResurrectionAmulet)
        {
            resurrectionButton.interactable = true;
        }
        else
        {
            resurrectionButton.interactable = false;
        }

        playerRoot.canRun = false;
        playerRoot.isGamePaused = true;

        Time.timeScale = 0;
    }

    public void ContinueRunChoice(bool willContinue)
    {
        playerRoot.ContinueRunChoice(willContinue);
    }

    public void ResumeButton()
    {
        pauseMenu.SetActive(false);
        resurrectionMenu.SetActive(false);

        playerRoot.isGamePaused = false;
        playerRoot.canRun = true;

        Time.timeScale = 1f;
    }
    #endregion

    public void StaticsMenu(float height = 0, int coins = 0, int rubies = 0, int obstaclesDestroyed = 0)
    {
        pauseMenu.SetActive(false);
        HUD.SetActive(false);

        heightClimbed.text = height.ToString("F0");
        coinsCollected.text = coins.ToString("F0");
        //rubiesCollected.text = rubies.ToString("F0");
        obstacles.text = obstaclesDestroyed.ToString("F0");

        statsMenu.SetActive(true);
    }

    public void BackToMainMenu()
    {
        statsMenu.SetActive(false);
        pauseMenu.SetActive(false);
        resurrectionMenu.SetActive(false);
        HUD.SetActive(false);
        leaderboard.SetActive(false);
        characterSelectionMenu.SetActive(false);
        levelSelectionMenu.SetActive(false);
        shopMenu.SetActive(false);
        optionsMenu.SetActive(false);
        topMenu.SetActive(true);

        //playerRoot.EndRun();
        GameController.gameController.isRunning = false;
        if (AudioController.audioController.currentMusicCode != 0) AudioController.audioController.SwitchMusic(0);

        //playerRoot.isGamePaused = false;        
        Time.timeScale = 1;

        if (!isLevelSelecting && !isCharSelecting)
        {
            GameController.gameController.InitilizeLevelStatics();
            GameController.gameController.ResetPlayerPosition();
        }

        isCharSelecting = false;
        isLevelSelecting = false;
        mainMenu.SetActive(true);
    }

    #endregion

    #region LeaderBoard

    public void ShowLeaderboard()
    {
        leaderboard.SetActive(true);
    }

    public void UpdateLeaderboardDisplay(int index, string name = "", float height = 0)
    {
        if (height > 0) playerNames[index].text = name;
        else playerNames[index].text = "";

        playerHeights[index].text = height.ToString("F0") + "m";
    }

    #endregion

    #region Character Info

    #region Character Selection
    public void CharacterSelection()
    {
        characterSelectionMenu.SetActive(true);
        
        characterMenuArray[charCode].SetActive(true);

        isCharSelecting = true;
    }

    public void NextCharacter()
    {

        if (isCharSelecting && charCode + 1 < characterMenuArray.Length)
        {
            characterMenuArray[charCode + 1].SetActive(true);
            characterMenuArray[charCode].SetActive(false);
            charCode++;
        }
    }

    public void PreviousCharacter()
    {

        if (isCharSelecting && charCode - 1 >= 0)
        {
            characterMenuArray[charCode - 1].SetActive(true);
            characterMenuArray[charCode].SetActive(false);
            charCode--;
        }
    }

    public void SelectCowboy()
    {
        GameController.gameController.playerRoot.selectedCharacter = characterID.Cowboy;
        GameController.gameController.playerRoot.Initialize(characterID.Cowboy);
        charCode = 0;
        BackToMainMenu();
    }

    public void SelectSamurai()
    {
        GameController.gameController.playerRoot.selectedCharacter = characterID.Samurai;
        GameController.gameController.playerRoot.Initialize(characterID.Samurai);
        charCode = 1;
        BackToMainMenu();
    }

    public void SelectAlpinista()
    {
        GameController.gameController.playerRoot.selectedCharacter = characterID.Alpinista;
        GameController.gameController.playerRoot.Initialize(characterID.Alpinista);
        charCode = 2;
        BackToMainMenu();
    }
    #endregion

    #region Character Upgrades

    #region Character Upgrades Buttons
    public void UpgradeCharacterStamina(int charCode)
    {
        ProgressManager.progressManager.UpgradeStamina(charCode);
    }

    public void UpgradeCharacterDefense(int charCode)
    {
        ProgressManager.progressManager.UpgradeDefense(charCode);
    }

    public void UpgradeCharacterResistance(int charCode)
    {
        ProgressManager.progressManager.UpgradeResistance(charCode);
    }

    public void UpgradeCharacterAttack(int charCode)
    {
        ProgressManager.progressManager.UpgradeAttack(charCode);
    }

    #endregion

    #region Cowboy Upgrade UI
    public void UpdateCowboyStaminaUI(int upgradeBonus = 0, int level = 0,
                                                int coinCost = 1000, int rubyCost = 0, int maxLevel = 0)
    {
        cowboyStaminaIndicator.text = "Stamina (" + (100 + upgradeBonus) + ")";

        if (level >= maxLevel)
        {
            cowboyStaminaCost.SetActive(false);
            cowboyStaminaMaxLevelImage.SetActive(true);
            cowboyStaminaLevel.text = "Lv. MAX";
        }
        else
        {
            cowboyStaminaLevel.text = "Lv. " + (level);
        }

        //cowboyStaminaUpgradedIndicator.text = "Stamina Restored (20+" + (level * 20) + ")";
        cowboyStaminaVisualUpgrade.value = level;

        if (coinCost >= 10000)
        {
            cowboyStaminaUpgradeCoinCost.text = coinCost / 1000 + "k";
        }
        else
        {
            cowboyStaminaUpgradeCoinCost.text = coinCost.ToString();
        }

        if (rubyCost > 0)
        {
            cowboyStaminaRubyCost.SetActive(true);
            cowboyStaminaUpgradeRubyCost.text = rubyCost.ToString();

        }
    }

    public void UpdateCowboyDefenseUI(int upgradeBonus = 0, int level = 0,
                                                int coinCost = 1000, int rubyCost = 0, int maxLevel = 0)
    {
        cowboyDefenseIndicator.text = "Defense (" + (5 + upgradeBonus) + ")";

        if (level >= maxLevel)
        {
            cowboyDefenseCost.SetActive(false);
            cowboyDefenseMaxLevelImage.SetActive(true);
            cowboyDefenseLevel.text = "Lv. MAX";
        }
        else
        {
            cowboyDefenseLevel.text = "Lv. " + (level);
        }

        //cowboyStaminaUpgradedIndicator.text = "Stamina Restored (20+" + (level * 20) + ")";
        cowboyDefenseVisualUpgrade.value = level;

        if (coinCost >= 10000)
        {
            cowboyDefenseUpgradeCoinCost.text = coinCost / 1000 + "k";
        }
        else
        {
            cowboyDefenseUpgradeCoinCost.text = coinCost.ToString();
        }

        if (rubyCost > 0)
        {
            cowboyDefenseRubyCost.SetActive(true);
            cowboyDefenseUpgradeRubyCost.text = rubyCost.ToString();

        }
    }

    public void UpdateCowboyResistanceUI(int upgradeBonus = 0, int level = 0,
                                                int coinCost = 1000, int rubyCost = 0, int maxLevel = 0)
    {
        cowboyResistanceIndicator.text = "Resistance (" + (1 + upgradeBonus) + ")";

        if (level >= maxLevel)
        {
            cowboyResistanceCost.SetActive(false);
            cowboyResistanceMaxLevelImage.SetActive(true);
            cowboyResistanceLevel.text = "Lv. MAX";
        }
        else
        {
            cowboyResistanceLevel.text = "Lv. " + (level);
        }

        //cowboyStaminaUpgradedIndicator.text = "Stamina Restored (20+" + (level * 20) + ")";
        cowboyResistanceVisualUpgrade.value = level;

        if (coinCost >= 10000)
        {
            cowboyResistanceUpgradeCoinCost.text = coinCost / 1000 + "k";
        }
        else
        {
            cowboyResistanceUpgradeCoinCost.text = coinCost.ToString();
        }

        if (rubyCost > 0)
        {
            cowboyResistanceRubyCost.SetActive(true);
            cowboyResistanceUpgradeRubyCost.text = rubyCost.ToString();

        }
    }

    public void UpdateCowboyAttackUI(int upgradeBonus = 0, int level = 0,
                                                int coinCost = 1000, int rubyCost = 0, int maxLevel = 0)
    {
        cowboyAttackIndicator.text = "Attack (" + (5 + upgradeBonus) + ")";

        if (level >= maxLevel)
        {
            cowboyAttackCost.SetActive(false);
            cowboyAttackMaxLevelImage.SetActive(true);
            cowboyAttackLevel.text = "Lv. MAX";
        }
        else
        {
            cowboyAttackLevel.text = "Lv. " + (level);
        }

        //cowboyStaminaUpgradedIndicator.text = "Stamina Restored (20+" + (level * 20) + ")";
        cowboyAttackVisualUpgrade.value = level;

        if (coinCost >= 10000)
        {
            cowboyAttackUpgradeCoinCost.text = coinCost / 1000 + "k";
        }
        else
        {
            cowboyAttackUpgradeCoinCost.text = coinCost.ToString();
        }

        if (rubyCost > 0)
        {
            cowboyAttackRubyCost.SetActive(true);
            cowboyAttackUpgradeRubyCost.text = rubyCost.ToString();

        }
    }

    #endregion

    #region Samurai Upgrade UI
    public void UpdateSamuraiStaminaUI(int upgradeBonus = 0, int level = 0,
                                                int coinCost = 1000, int rubyCost = 0, int maxLevel = 0)
    {
        samuraiStaminaIndicator.text = "Stamina (" + (120 + upgradeBonus) + ")";

        if (level >= maxLevel)
        {
            samuraiStaminaCost.SetActive(false);
            samuraiStaminaMaxLevelImage.SetActive(true);
            samuraiStaminaLevel.text = "Lv. MAX";
        }
        else
        {
            samuraiStaminaLevel.text = "Lv. " + (level);
        }

        //cowboyStaminaUpgradedIndicator.text = "Stamina Restored (20+" + (level * 20) + ")";
        samuraiStaminaVisualUpgrade.value = level;

        if (coinCost >= 10000)
        {
            samuraiStaminaUpgradeCoinCost.text = coinCost / 1000 + "k";
        }
        else
        {
            samuraiStaminaUpgradeCoinCost.text = coinCost.ToString();
        }

        if (rubyCost > 0)
        {
            samuraiStaminaRubyCost.SetActive(true);
            samuraiStaminaUpgradeRubyCost.text = rubyCost.ToString();

        }
    }

    public void UpdateSamuraiDefenseUI(int upgradeBonus = 0, int level = 0,
                                                int coinCost = 1000, int rubyCost = 0, int maxLevel = 0)
    {
        samuraiDefenseIndicator.text = "Defense (" + (5 + upgradeBonus) + ")";

        if (level >= maxLevel)
        {
            samuraiDefenseCost.SetActive(false);
            samuraiDefenseMaxLevelImage.SetActive(true);
            samuraiDefenseLevel.text = "Lv. MAX";
        }
        else
        {
            samuraiDefenseLevel.text = "Lv. " + (level);
        }

        //cowboyStaminaUpgradedIndicator.text = "Stamina Restored (20+" + (level * 20) + ")";
        samuraiDefenseVisualUpgrade.value = level;

        if (coinCost >= 10000)
        {
            samuraiDefenseUpgradeCoinCost.text = coinCost / 1000 + "k";
        }
        else
        {
            samuraiDefenseUpgradeCoinCost.text = coinCost.ToString();
        }

        if (rubyCost > 0)
        {
            samuraiDefenseRubyCost.SetActive(true);
            samuraiDefenseUpgradeRubyCost.text = rubyCost.ToString();

        }
    }

    public void UpdateSamuraiResistanceUI(int upgradeBonus = 0, int level = 0,
                                                int coinCost = 1000, int rubyCost = 0, int maxLevel = 0)
    {
        samuraiResistanceIndicator.text = "Resistance (" + (1 + upgradeBonus) + ")";

        if (level >= maxLevel)
        {
            samuraiResistanceCost.SetActive(false);
            samuraiResistanceMaxLevelImage.SetActive(true);
            samuraiResistanceLevel.text = "Lv. MAX";
        }
        else
        {
            samuraiResistanceLevel.text = "Lv. " + (level);
        }

        //cowboyStaminaUpgradedIndicator.text = "Stamina Restored (20+" + (level * 20) + ")";
        samuraiResistanceVisualUpgrade.value = level;

        if (coinCost >= 10000)
        {
            samuraiResistanceUpgradeCoinCost.text = coinCost / 1000 + "k";
        }
        else
        {
            samuraiResistanceUpgradeCoinCost.text = coinCost.ToString();
        }

        if (rubyCost > 0)
        {
            samuraiResistanceRubyCost.SetActive(true);
            samuraiResistanceUpgradeRubyCost.text = rubyCost.ToString();

        }
    }

    public void UpdateSamuraiAttackUI(int upgradeBonus = 0, int level = 0,
                                                int coinCost = 1000, int rubyCost = 0, int maxLevel = 0)
    {
        samuraiAttackIndicator.text = "Attack (" + (5 + upgradeBonus) + ")";

        if (level >= maxLevel)
        {
            samuraiAttackCost.SetActive(false);
            samuraiAttackMaxLevelImage.SetActive(true);
            samuraiAttackLevel.text = "Lv. MAX";
        }
        else
        {
            samuraiAttackLevel.text = "Lv. " + (level);
        }

        //cowboyStaminaUpgradedIndicator.text = "Stamina Restored (20+" + (level * 20) + ")";
        samuraiAttackVisualUpgrade.value = level;

        if (coinCost >= 10000)
        {
            samuraiAttackUpgradeCoinCost.text = coinCost / 1000 + "k";
        }
        else
        {
            samuraiAttackUpgradeCoinCost.text = coinCost.ToString();
        }

        if (rubyCost > 0)
        {
            samuraiAttackRubyCost.SetActive(true);
            samuraiAttackUpgradeRubyCost.text = rubyCost.ToString();

        }
    }

    #endregion

    #region Dullahan Upgrade UI
    public void UpdateDullahanStaminaUI(int upgradeBonus = 0, int level = 0,
                                                int coinCost = 1000, int rubyCost = 0, int maxLevel = 0)
    {
        dullahanStaminaIndicator.text = "Stamina (" + (80 + upgradeBonus) + ")";

        if (level >= maxLevel)
        {
            dullahanStaminaCost.SetActive(false);
            dullahanStaminaMaxLevelImage.SetActive(true);
            dullahanStaminaLevel.text = "Lv. MAX";
        }
        else
        {
            dullahanStaminaLevel.text = "Lv. " + (level);
        }

        //cowboyStaminaUpgradedIndicator.text = "Stamina Restored (20+" + (level * 20) + ")";
        dullahanStaminaVisualUpgrade.value = level;

        if (coinCost >= 10000)
        {
            dullahanStaminaUpgradeCoinCost.text = coinCost / 1000 + "k";
        }
        else
        {
            dullahanStaminaUpgradeCoinCost.text = coinCost.ToString();
        }

        if (rubyCost > 0)
        {
            dullahanStaminaRubyCost.SetActive(true);
            dullahanStaminaUpgradeRubyCost.text = rubyCost.ToString();

        }
    }

    public void UpdateDullahanDefenseUI(int upgradeBonus = 0, int level = 0,
                                                int coinCost = 1000, int rubyCost = 0, int maxLevel = 0)
    {
        dullahanDefenseIndicator.text = "Defense (" + (5 + upgradeBonus) + ")";

        if (level >= maxLevel)
        {
            dullahanDefenseCost.SetActive(false);
            dullahanDefenseMaxLevelImage.SetActive(true);
            dullahanDefenseLevel.text = "Lv. MAX";
        }
        else
        {
            dullahanDefenseLevel.text = "Lv. " + (level);
        }

        //cowboyStaminaUpgradedIndicator.text = "Stamina Restored (20+" + (level * 20) + ")";
        dullahanDefenseVisualUpgrade.value = level;

        if (coinCost >= 10000)
        {
            dullahanDefenseUpgradeCoinCost.text = coinCost / 1000 + "k";
        }
        else
        {
            dullahanDefenseUpgradeCoinCost.text = coinCost.ToString();
        }

        if (rubyCost > 0)
        {
            dullahanDefenseRubyCost.SetActive(true);
            dullahanDefenseUpgradeRubyCost.text = rubyCost.ToString();

        }
    }

    public void UpdateDullahanResistanceUI(int upgradeBonus = 0, int level = 0,
                                                int coinCost = 1000, int rubyCost = 0, int maxLevel = 0)
    {
        dullahanResistanceIndicator.text = "Resistance (" + (1 + upgradeBonus) + ")";

        if (level >= maxLevel)
        {
            dullahanResistanceCost.SetActive(false);
            dullahanResistanceMaxLevelImage.SetActive(true);
            dullahanResistanceLevel.text = "Lv. MAX";
        }
        else
        {
            dullahanResistanceLevel.text = "Lv. " + (level);
        }

        //cowboyStaminaUpgradedIndicator.text = "Stamina Restored (20+" + (level * 20) + ")";
        dullahanResistanceVisualUpgrade.value = level;

        if (coinCost >= 10000)
        {
            dullahanResistanceUpgradeCoinCost.text = coinCost / 1000 + "k";
        }
        else
        {
            dullahanResistanceUpgradeCoinCost.text = coinCost.ToString();
        }

        if (rubyCost > 0)
        {
            dullahanResistanceRubyCost.SetActive(true);
            dullahanResistanceUpgradeRubyCost.text = rubyCost.ToString();

        }
    }

    public void UpdateDullahanAttackUI(int upgradeBonus = 0, int level = 0,
                                                int coinCost = 1000, int rubyCost = 0, int maxLevel = 0)
    {
        dullahanAttackIndicator.text = "Attack (" + (5 + upgradeBonus) + ")";

        if (level >= maxLevel)
        {
            dullahanAttackCost.SetActive(false);
            dullahanAttackMaxLevelImage.SetActive(true);
            dullahanAttackLevel.text = "Lv. MAX";
        }
        else
        {
            dullahanAttackLevel.text = "Lv. " + (level);
        }

        //cowboyStaminaUpgradedIndicator.text = "Stamina Restored (20+" + (level * 20) + ")";
        dullahanAttackVisualUpgrade.value = level;

        if (coinCost >= 10000)
        {
            dullahanAttackUpgradeCoinCost.text = coinCost / 1000 + "k";
        }
        else
        {
            dullahanAttackUpgradeCoinCost.text = coinCost.ToString();
        }

        if (rubyCost > 0)
        {
            dullahanAttackRubyCost.SetActive(true);
            dullahanAttackUpgradeRubyCost.text = rubyCost.ToString();

        }
    }

    #endregion

    #endregion

    #endregion

    #region Level Selection

    public void LevelSelection()
    {
        levelSelectionMenu.SetActive(true);
        isLevelSelecting = true;
    }

    public void NextLevel()
    {

        if (isLevelSelecting && levelCode + 1 < levelMenuArray.Length)
        {
            levelMenuArray[levelCode + 1].SetActive(true);
            levelMenuArray[levelCode].SetActive(false);
            levelCode++;
        }
        else if (isCharSelecting && charCode + 1 < characterMenuArray.Length)
        {
            characterMenuArray[charCode + 1].SetActive(true);
            characterMenuArray[charCode].SetActive(false);
            charCode++;
        }
    }

    public void PreviousLevel()
    {

        if (isLevelSelecting && levelCode - 1 >= 0)
        {
            levelMenuArray[levelCode - 1].SetActive(true);
            levelMenuArray[levelCode].SetActive(false);
            levelCode--;
        }
        else if (isCharSelecting && charCode - 1 >= 0)
        {
            characterMenuArray[charCode - 1].SetActive(true);
            characterMenuArray[charCode].SetActive(false);
            charCode--;
        }
    }

    public void SelectCowboyLevel()
    {
        GameController.gameController.currentLevelID = levelID.CowboyLevel;
        GameController.gameController.currentLevelCheckpoint = GameController.gameController.cowboyLevelCheckpoint;
        GameController.gameController.InitilizeLevelStatics();
        GameController.gameController.ResetPlayerPosition();
        BackToMainMenu();
    }

    public void SelectSamuraiLevel()
    {
        GameController.gameController.currentLevelID = levelID.SamuraiLevel;
        GameController.gameController.currentLevelCheckpoint = GameController.gameController.samuraiLevelCheckpoint;
        GameController.gameController.InitilizeLevelStatics();
        GameController.gameController.ResetPlayerPosition();
        BackToMainMenu();
    }

    public void SelectAlpinistaLevel()
    {
        GameController.gameController.currentLevelID = levelID.AlpinistaLevel;
        GameController.gameController.currentLevelCheckpoint = GameController.gameController.alpinistaLevelCheckpoint;
        GameController.gameController.InitilizeLevelStatics();
        GameController.gameController.ResetPlayerPosition();
        BackToMainMenu();
    }

    #endregion

    #region Store

    #region Store Navigation
    public void ShopMenu()
    {
        shopMenu.SetActive(true);
    }

    public void PurchaseMenu()
    {
        //purchaseMenu.SetActive(true);
        itemUpgradeMenu.SetActive(false);
    }

    public void ItemUpgradeMenu()
    {
        itemUpgradeMenu.SetActive(true);
        //purchaseMenu.SetActive(false);
    }
    #endregion

    #region Purchase

    public void PurchaseItem(int itemCode = 0)
    {
        GameController.gameController.inventory.ItemPurchase(itemCode);
    }

    #region Resurrection Amulet
    public void UpdateResurrectionAmuletPurchaseUI(int quantity = 0, int coinCost = 1000, int rubyCost = 0)
    {
        
        resurrectionAmuletPurchaseDescription.text = "In Inventory: " + quantity;

        if (coinCost >= 1000000)
        {
            resurrectionAmuletPurchaseCoinCost.text = coinCost / 1000000 + "M";
        }
        else if (coinCost >= 10000)
        {
            resurrectionAmuletPurchaseCoinCost.text = coinCost / 1000 + "k";
        }
        else
        {
            resurrectionAmuletPurchaseCoinCost.text = coinCost.ToString();
        }


        if (rubyCost > 0)
        {
            resurrectionAmuletPurchaseRubyCost.SetActive(true);
            resurrectionAmuletPurchaseRubyCostValue.text = rubyCost.ToString();

        }
    }
    #endregion

    #region Special Boost
    public void UpdateSpecialBoostPurchaseUI(int quantity = 0, int coinCost = 1000, int rubyCost = 0)
    {

        specialBoostPurchaseDescription.text = "In Inventory: " + quantity;

        if (coinCost >= 1000000)
        {
            specialBoostPurchaseCoinCost.text = coinCost / 1000000 + "M";
        }
        else if (coinCost >= 10000)
        {
            specialBoostPurchaseCoinCost.text = coinCost / 1000 + "k";
        }
        else
        {
            specialBoostPurchaseCoinCost.text = coinCost.ToString();
        }


        if (rubyCost > 0)
        {
            specialBoostPurchaseRubyCost.SetActive(true);
            specialBoostPurchaseRubyCostValue.text = rubyCost.ToString();

        }
    }
    #endregion

    #region Adrenaline
    public void UpdateAdrenalinePurchaseUI(int quantity = 0, int coinCost = 1000, int rubyCost = 0)
    {

        adrenalinePurchaseDescription.text = "In Inventory: " + quantity;

        if (coinCost >= 1000000)
        {
            adrenalinePurchaseCoinCost.text = coinCost / 1000000 + "M";
        }
        else if (coinCost >= 10000)
        {
            adrenalinePurchaseCoinCost.text = coinCost / 1000 + "k";
        }
        else
        {
            adrenalinePurchaseCoinCost.text = coinCost.ToString();
        }


        if (rubyCost > 0)
        {
            adrenalinePurchaseRubyCost.SetActive(true);
            adrenalinePurchaseRubyCostValue.text = rubyCost.ToString();

        }
    }
    #endregion

    #endregion

    #region Upgrades    
    public void UpgradeItem(int itemCode = 0)
    {
        GameController.gameController.inventory.ItemUpgrade(itemCode);
    }

    #region Stamina Potion Upgrade

    public void UpdateStaminaPostionUpgradeUI(int upgradeBonus = 0, int level = 0, int coinCost = 1000, int rubyCost = 0, int maxLevel = 10)
    {
        staminaPotionName.text = "Stamina Potion (" + (10 + upgradeBonus) + ")";

        if (level >= maxLevel)
        {
            staminaPotionCost.SetActive(false);
            staminaPotionMaxLevelImage.SetActive(true);
            staminaPotionLevel.text = "Lv. MAX";
        }
        else
        {
            staminaPotionLevel.text = "Lv. " + (level);
        }
            
        staminaPotionUpgradedIndicator.text = "Stamina Recover (10+" + (level * 5) + ")";
        staminaPotionVisualUpgrade.value = level;

        if (coinCost >= 10000)
        {
            staminaPotionUpgradeCoinCost.text = coinCost/1000 + "k";
        }
        else
        {
            staminaPotionUpgradeCoinCost.text = coinCost.ToString();
        }


        if (rubyCost > 0)
        {
            staminaPotionRubyCost.SetActive(true);
            staminaPotionUpgradeRubyCost.text = rubyCost.ToString();

        }
    }

    #endregion

    #region Shield Updgrade

    public void UpdateShieldChargeUpgradeUI(int upgradeBonus = 0, int level = 0,
                                                int coinCost = 1000, int rubyCost = 0, int maxLevel = 10)
    {
        shieldChargeName.text = "Shield Charges (" + (1 + upgradeBonus) + ")";

        if (level >= maxLevel)
        {
            shieldChargeCost.SetActive(false);
            shieldChargeMaxLevelImage.SetActive(true);
            shieldChargeLevel.text = "Lv. MAX";
        }
        else
        {
            shieldChargeLevel.text = "Lv. " + (level);
        }
                
        shieldChargeUpgradedIndicator.text = "Shield Recover (1+" + (level) + ")";
        shieldChargeVisualUpgrade.value = level;

        if (coinCost >= 10000)
        {
            shieldChargeUpgradeCoinCost.text = coinCost / 1000 + "k";
        }
        else
        {
            shieldChargeUpgradeCoinCost.text = coinCost.ToString();
        }


        if (rubyCost > 0)
        {
            shieldChargeRubyCost.SetActive(true);
            shieldChargeUpgradeRubyCost.text = rubyCost.ToString();

        }
        
    }

    public void UpdateShieldDurationUpgradeUI(int upgradeBonus = 0, int level = 0,
                                                int coinCost = 1000, int rubyCost = 0,  int maxLevel = 10)
    {
        shieldDurationName.text = "Shield Duration (" + (20 + upgradeBonus) + ")";

        if (level >= maxLevel)
        {
            shieldDurationCost.SetActive(false);
            shieldDurationMaxLevelImage.SetActive(true);
            shieldDurationLevel.text = "Lv. MAX";
        }
        else
        {
            shieldDurationLevel.text = "Lv. " + (level);
        }
        //shieldDurationLevel.text = "Lv. " + (level);

        shieldDurationUpgradedIndicator.text = "Shield Recover (20+" + (level * 3) + ")";
        shieldDurationVisualUpgrade.value = level;

        if (coinCost >= 10000)
        {
            shieldDurationUpgradeCoinCost.text = coinCost / 1000 + "k";
        }
        else
        {
            shieldDurationUpgradeCoinCost.text = coinCost.ToString();
        }


        if (rubyCost > 0)
        {
            shieldDurationRubyCost.SetActive(true);
            shieldDurationUpgradeRubyCost.text = rubyCost.ToString();

        }
        //shieldDurationUpgradeCoinCost.text = coinCost.ToString();
    }

    #endregion

    #region CoinMultiplier Upgrade

    public void UpdateCoinMultiplierUpgradeUI(int upgradeBonus = 0, int level = 0,
                                                int coinCost = 1000, int rubyCost = 0, int maxLevel = 0)
    {
        coinMultiplierChargeName.text = "Coin Multiplier (" + (1 + upgradeBonus) + ")";

        if (level >= maxLevel)
        {
            coinMultiplierChargeCost.SetActive(false);
            coinMultiplierChargeMaxLevelImage.SetActive(true);
            coinMultiplierChargeLevel.text = "Lv. MAX";
        }
        else
        {
            coinMultiplierChargeLevel.text = "Lv. " + (level);
        }

        coinMultiplierChargeUpgradedIndicator.text = "Multiplier (1+" + (level) + ")";
        coinMultiplierChargeVisualUpgrade.value = level;

        if (coinCost >= 10000)
        {
            coinMultiplierChargeUpgradeCoinCost.text = coinCost / 1000 + "k";
        }
        else
        {
            coinMultiplierChargeUpgradeCoinCost.text = coinCost.ToString();
        }


        if (rubyCost > 0)
        {
            coinMultiplierChargeRubyCost.SetActive(true);
            coinMultiplierChargeUpgradeRubyCost.text = rubyCost.ToString();

        }
    }

    public void UpdateCoinMultiplierDurationUpgradeUI(int upgradeBonus = 0, int level = 0,
                                                int coinCost = 1000, int rubyCost = 0, int maxLevel = 0)
    {
        
        
        coinMultiplierDurationName.text = "Coin Multiplier Duration (" + (16 + upgradeBonus) + ")";

        if (level >= maxLevel)
        {
            coinMultiplierDurationCost.SetActive(false);
            coinMultiplierDurationMaxLevelImage.SetActive(true);
            coinMultiplierDurationLevel.text = "Lv. MAX";
        }
        else
        {
            coinMultiplierDurationLevel.text = "Lv. " + (level);
        }

        coinMultiplierDurationUpgradedIndicator.text = "Multiplier Duration (16+" + (level * 4) + ")";
        coinMultiplierDurationVisualUpgrade.value = level;

        if (coinCost >= 10000)
        {
            coinMultiplierDurationUpgradeCoinCost.text = coinCost / 1000 + "k";
        }
        else
        {
            coinMultiplierDurationUpgradeCoinCost.text = coinCost.ToString();
        }


        if (rubyCost > 0)
        {
            coinMultiplierDurationRubyCost.SetActive(true);
            coinMultiplierDurationUpgradeRubyCost.text = rubyCost.ToString();

        }
    }

    #endregion

    #region Resurrection Amulet

    public void UpdateResurrectionAmuletUpgradeUI(int upgradeBonus = 0, int level = 0,
                                                int coinCost = 1000, int rubyCost = 0, int maxLevel = 0)
    {
        resurrectionAmuletName.text = "Resurrection Amulet (" + (20 + upgradeBonus) + ")";        

        if (level >= maxLevel)
        {
            resurrectionAmuletCost.SetActive(false);
            resurrectionAmuletMaxLevelImage.SetActive(true);
            resurrectionAmuletLevel.text = "Lv. MAX";
        }
        else
        {
            resurrectionAmuletLevel.text = "Lv. " + (level);
        }

        resurrectionAmuletUpgradedIndicator.text = "Stamina Restored (20+" + (level*20) + ")";
        resurrectionAmuletVisualUpgrade.value = level;

        if (coinCost >= 10000)
        {
            resurrectionAmuletUpgradeCoinCost.text = coinCost / 1000 + "k";
        }
        else
        {
            resurrectionAmuletUpgradeCoinCost.text = coinCost.ToString();
        }

        if (rubyCost > 0)
        {
            resurrectionAmuletRubyCost.SetActive(true);
            resurrectionAmuletUpgradeRubyCost.text = rubyCost.ToString();

        }
    }


    #endregion

    #region Special Boost

    public void UpdateSpecialBoostUpgradeUI(int upgradeBonus = 0, int level = 0,
                                                int coinCost = 1000, int rubyCost = 0, int maxLevel = 0)
    {

        specialBoostName.text = "Special Boost (" + (10 + upgradeBonus) + ")";

        if (level >= maxLevel)
        {
            specialBoostCost.SetActive(false);
            specialBoostMaxLevelImage.SetActive(true);
            specialBoostLevel.text = "Lv. MAX";
        }
        else
        {
            specialBoostLevel.text = "Lv. " + (level);
        }
                
        specialBoostUpgradedIndicator.text = "Special Restored (10+" + (level*5) + ")";
        specialBoostVisualUpgrade.value = level;

        if (coinCost >= 10000)
        {
            specialBoostUpgradeCoinCost.text = coinCost / 1000 + "k";
        }
        else
        {
            specialBoostUpgradeCoinCost.text = coinCost.ToString();
        }

        if (rubyCost > 0)
        {
            specialBoostRubyCost.SetActive(true);
            specialBoostUpgradeRubyCost.text = rubyCost.ToString();

        }
    }

    #endregion

    #region Adrenaline

    public void UpdateAdrenalineUpgradeUI(int upgradeBonus = 0, int level = 0,
                                                int coinCost = 1000, int rubyCost = 0, int maxLevel = 0)
    {
        
        adrenalineName.text = "Adrenaline (" + (10 + upgradeBonus) + ")";

        if (level >= maxLevel)
        {
            adrenalineCost.SetActive(false);
            adrenalineMaxLevelImage.SetActive(true);
            adrenalineLevel.text = "Lv. MAX";
        }
        else
        {
            adrenalineLevel.text = "Lv. " + (level);
        }
        
        adrenalineUpgradedIndicator.text = "Stamina Restored (10+" + (level*5) + ")";
        adrenalineVisualUpgrade.value = level;

        if (coinCost >= 10000)
        {
            adrenalineUpgradeCoinCost.text = coinCost / 1000 + "k";
        }
        else
        {
            adrenalineUpgradeCoinCost.text = coinCost.ToString();
        }

        if (rubyCost > 0)
        {
            adrenalineRubyCost.SetActive(true);
            adrenalineUpgradeRubyCost.text = rubyCost.ToString();
        }

    }

    #endregion

    #endregion

    #endregion

    #region Run HUD

    private void UpdateStaminaHUD(float stamina)
    {
        staminaSlider.value = stamina;

        if (staminaSlider.value >= 0.8f)
        {
            staminaFill.color = green;
            staminaBackground.color = darkGreen;
        }
        else if (staminaSlider.value >= 0.6f && staminaSlider.value < 0.8f)
        {
            staminaFill.color = greenYellow;
            staminaBackground.color = darkGreenYellow;
        }
        else if (staminaSlider.value >= 0.3f && staminaSlider.value < 0.6f)
        {
            staminaFill.color = yellow;
            staminaBackground.color = darkYellow;
        }
        else if (staminaSlider.value >= 0.15f && staminaSlider.value < 0.3f)
        {
            staminaFill.color = orange;
            staminaBackground.color = darkOrange;
        }
        else
        {
            staminaFill.color = red;
            staminaBackground.color = darkRed;
        }

    }

    public void UpdateCoinHUD(int normalCoins = 0, int rubies = 0)
    {
        runCoins.text = "x " + normalCoins;
    }

    private void UpdateHeightClimbed()
    {
        runHeightClimbed.text = playerRoot.heightClimbed.ToString("F0") + "m";
    }

    private void UpdateSpecialHUD(float special)
    {
        specialSlider.value = special;
    }

    #region Ammo
    public void InitializeAmmoUI(int characterCode = 0, int maxAmmo = 2)
    {

        for (int i = 0; i < ammoType.Length; i++)
        {
            ammoType[i].SetActive(false);
        }


        for (int i = 0; i < cowboyAmmo.Length; i++)
        {
            cowboyAmmo[i].gameObject.SetActive(false);
        }

        for (int i = 0; i < samuraiAmmo.Length; i++)
        {
            samuraiAmmo[i].gameObject.SetActive(false);
        }

        for (int i = 0; i < mummyAmmo.Length; i++)
        {
            mummyAmmo[i].gameObject.SetActive(false);
        }


        ammoType[characterCode].SetActive(true);

        if (characterCode == 0)
        {
            for (int i = 0; i < maxAmmo; i++)
            {
                cowboyAmmo[i].gameObject.SetActive(true);
            }

            activeSliderArray = cowboyAmmo;
        }
        else if (characterCode == 1)
        {
            for (int i = 0; i < maxAmmo; i++)
            {
                samuraiAmmo[i].gameObject.SetActive(true);
            }

            activeSliderArray = samuraiAmmo;
        }
        else if (characterCode == 2)
        {
            for (int i = 0; i < maxAmmo; i++)
            {
                mummyAmmo[i].gameObject.SetActive(true);
            }

            activeSliderArray = mummyAmmo;
        }

        for (int i = 0; i < activeSliderArray.Length; i++)
        {
            activeSliderArray[i].value = 1;
        }

    }

    public void UpdateAmmoQuantity(int currentAmmo)
    {
        //activeSliderArray[currentAmmo].value = 0;
        for (int i = activeSliderArray.Length - 1; i >= currentAmmo; i--)
        {
            activeSliderArray[i].value = 0;
        }

    }

    public void UpdateAmmoReload(int currentAmmo, float reloadTime)
    {
        if (currentAmmo >= activeSliderArray.Length) return;

        activeSliderArray[currentAmmo].value = reloadTime;
    }

    #endregion

    #endregion


}
