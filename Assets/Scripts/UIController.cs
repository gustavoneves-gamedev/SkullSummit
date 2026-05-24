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

    [Header("LevelMenu")]
    [SerializeField] private GameObject levelSelectionMenu;
    [SerializeField] private GameObject[] levelMenuArray;
    private int levelCode = 0;
    private bool isLevelSelecting;

    [Header("ShopMenu")]
    [SerializeField] private GameObject shopMenu;
    [SerializeField] private GameObject purchaseMenu;
    [SerializeField] private GameObject itemUpgradeMenu;

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
    [SerializeField] private TextMeshProUGUI coinMultiplierName;
    [SerializeField] private TextMeshProUGUI coinMultiplierLevel;
    [SerializeField] private TextMeshProUGUI coinMultiplierChargeUpgradedIndicator;
    [SerializeField] private Slider coinMultiplierChargeVisualUpgrade;
    [SerializeField] private TextMeshProUGUI coinMultiplierChargeUpgradeCost;
    [SerializeField] private TextMeshProUGUI coinMultiplierDurationUpgradedIndicator;
    [SerializeField] private Slider coinMultiplierDurationVisualUpgrade;
    [SerializeField] private TextMeshProUGUI coinMultiplierDurationUpgradeCoinCost;
    [SerializeField] private TextMeshProUGUI coinMultiplierDurationUpgradeRubyCost;

    [Header("ShopMenu - Resurrection Amulet")]
    [SerializeField] private TextMeshProUGUI resurrectionAmuletName;
    [SerializeField] private TextMeshProUGUI resurrectionAmuletLevel;
    [SerializeField] private TextMeshProUGUI resurrectionAmuletUpgradedIndicator;
    [SerializeField] private Slider resurrectionAmuletVisualUpgrade;
    [SerializeField] private TextMeshProUGUI resurrectionAmuletUpgradeCoinCost;
    [SerializeField] private TextMeshProUGUI resurrectionAmuletUpgradeRubyCost;

    [Header("ShopMenu - Special Boost")]
    [SerializeField] private TextMeshProUGUI specialBoostName;
    [SerializeField] private TextMeshProUGUI specialBoostLevel;
    [SerializeField] private TextMeshProUGUI specialBoostUpgradedIndicator;
    [SerializeField] private Slider specialBoostVisualUpgrade;
    [SerializeField] private TextMeshProUGUI specialBoostUpgradeCoinCost;
    [SerializeField] private TextMeshProUGUI specialBoostUpgradeRubyCost;

    [Header("ShopMenu - Adrenaline")]
    [SerializeField] private TextMeshProUGUI adrenalineName;
    [SerializeField] private TextMeshProUGUI adrenalineLevel;
    [SerializeField] private TextMeshProUGUI adrenalineUpgradedIndicator;
    [SerializeField] private Slider adrenalineVisualUpgrade;
    [SerializeField] private TextMeshProUGUI adrenalineUpgradeCoinCost;
    [SerializeField] private TextMeshProUGUI adrenalineUpgradeRubyCost;

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

    #region Character Selection

    public void CharacterSelection()
    {
        characterSelectionMenu.SetActive(true);
        isCharSelecting = true;
    }

    public void SelectCowboy()
    {
        GameController.gameController.playerRoot.selectedCharacter = characterID.Cowboy;
        GameController.gameController.playerRoot.Initialize(characterID.Cowboy);
        BackToMainMenu();
    }

    public void SelectSamurai()
    {
        GameController.gameController.playerRoot.selectedCharacter = characterID.Samurai;
        GameController.gameController.playerRoot.Initialize(characterID.Samurai);
        BackToMainMenu();
    }

    public void SelectAlpinista()
    {
        GameController.gameController.playerRoot.selectedCharacter = characterID.Alpinista;
        GameController.gameController.playerRoot.Initialize(characterID.Alpinista);
        BackToMainMenu();
    }


    #endregion

    #region Level Selection

    public void LevelSelection()
    {
        levelSelectionMenu.SetActive(true);
        isLevelSelecting = true;
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

    public void UpdateCoinMultiplierUpgradeUI(int upgradeBonus = 0, int chargeLevel = 0,
                                                int coinCost = 1000, int rubyCost = 0, int level = 0)
    {
        coinMultiplierName.text = "Coin Multiplier (" + (1 + upgradeBonus) + ")";
        coinMultiplierLevel.text = "Lv. " + (level);
        coinMultiplierChargeUpgradedIndicator.text = "Multiplier (1+" + (chargeLevel) + ")";
        coinMultiplierChargeVisualUpgrade.value = chargeLevel;
        coinMultiplierChargeUpgradeCost.text = coinCost.ToString();
    }

    public void UpdateCoinMultiplierDurationUpgradeUI(int upgradeBonus = 0, int durationLevel = 0,
                                                int coinCost = 1000, int rubyCost = 0, int level = 0)
    {
        coinMultiplierLevel.text = "Lv. " + (level);
        coinMultiplierDurationUpgradedIndicator.text = "Multiplier Duration (16+" + (durationLevel * 4) + ")";
        coinMultiplierDurationVisualUpgrade.value = durationLevel;
        coinMultiplierDurationUpgradeCoinCost.text = coinCost.ToString();
    }

    #endregion

    #region Resurrection Amulet

    public void UpdateResurrectionAmuletUpgradeUI(int upgradeBonus = 0, int chargeLevel = 0,
                                                int coinCost = 1000, int rubyCost = 0, int level = 0)
    {
        if (resurrectionAmuletName == null) return;

        resurrectionAmuletName.text = "Resurrection Amulet (" + (10 + upgradeBonus) + ")";
        resurrectionAmuletLevel.text = "Lv. " + (level);
        resurrectionAmuletUpgradedIndicator.text = "Stamina Restored (10+" + (chargeLevel) + ")";
        resurrectionAmuletVisualUpgrade.value = chargeLevel;
        resurrectionAmuletUpgradeCoinCost.text = coinCost.ToString();
    }


    #endregion

    #region Special Boost

    public void UpdateSpecialBoostUpgradeUI(int upgradeBonus = 0, int chargeLevel = 0,
                                                int coinCost = 1000, int rubyCost = 0, int level = 0)
    {

        if (specialBoostName == null) return;

        specialBoostName.text = "Special Boost (" + (10 + upgradeBonus) + ")";
        specialBoostLevel.text = "Lv. " + (level);
        specialBoostUpgradedIndicator.text = "Special Bar Restored (10+" + (chargeLevel) + ")";
        specialBoostVisualUpgrade.value = chargeLevel;
        specialBoostUpgradeCoinCost.text = coinCost.ToString();
    }

    #endregion

    #region Adrenaline

    public void UpdateAdrenalineUpgradeUI(int upgradeBonus = 0, int chargeLevel = 0,
                                                int coinCost = 1000, int rubyCost = 0, int level = 0)
    {

        if (adrenalineName == null) return;

        adrenalineName.text = "Adrenaline (" + (10 + upgradeBonus) + ")";
        adrenalineLevel.text = "Lv. " + (level);
        adrenalineUpgradedIndicator.text = "Stamina Restored (10+" + (chargeLevel) + ")";
        adrenalineVisualUpgrade.value = chargeLevel;
        adrenalineUpgradeCoinCost.text = coinCost.ToString();
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
