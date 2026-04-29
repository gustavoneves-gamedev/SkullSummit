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

    [Header("ShopMenu - Stamina Potion")]
    [SerializeField] private TextMeshProUGUI staminaPotionName;
    [SerializeField] private TextMeshProUGUI staminaPotionLevel;
    [SerializeField] private TextMeshProUGUI staminaPotionUpgradedIndicator;
    [SerializeField] private Slider staminaPotionVisualUpgrade;
    [SerializeField] private TextMeshProUGUI staminaPotionUpgradeCost;

    [Header("ShopMenu - Shield")]
    [SerializeField] private TextMeshProUGUI shieldName;
    [SerializeField] private TextMeshProUGUI shieldLevel;
    [SerializeField] private TextMeshProUGUI shieldChargeUpgradedIndicator;
    [SerializeField] private Slider shieldChargeVisualUpgrade;
    [SerializeField] private TextMeshProUGUI shieldChargeUpgradeCost;
    [SerializeField] private TextMeshProUGUI shieldDurationUpgradedIndicator;
    [SerializeField] private Slider shieldDurationVisualUpgrade;
    [SerializeField] private TextMeshProUGUI shieldDurationUpgradeCost;

    [Header("Run")]
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject HUD;
    [SerializeField] private TextMeshProUGUI runCoins;
    [SerializeField] private TextMeshProUGUI runHeightClimbed;

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

        UpdateStaminaHUD(playerRoot.currentStamina / playerRoot.maxStamina);
        UpdateHeightClimbed();
    }

    private void Initialize()
    {
        playerRoot = GameController.gameController.playerRoot;
        //coins.text = puxar informação do local de salvamento
        //InitializeStore(); //puxar informações do local de salvamento
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

    public void TopMenu()
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

    #endregion

    //MUDAR ISSO PARA ALTERAR O TIME SCALE PARA ZERO. DO CONTRÁRIO ESTAREI PARANDO APENAS O PLAYER!!
    public void PauseMenu()
    {
        pauseMenu.SetActive(!pauseMenu.activeSelf);

        playerRoot.canRun = !playerRoot.canRun;

        //TEMPORÁRIO - REVISAR!! 28/03/2026
        if (playerRoot.canRun) Time.timeScale = 1;
        else Time.timeScale = 0;

        //playerRoot.EndRun();
    }


    public void StaticsMenu(float height = 0, int coins = 0, int rubies = 0, int obstaclesDestroyed = 0)
    {
        pauseMenu.SetActive(false);
        HUD.SetActive(false);

        heightClimbed.text = height.ToString("F0");
        coinsCollected.text = coins.ToString("F0");
        rubiesCollected.text = rubies.ToString("F0");
        obstacles.text = obstaclesDestroyed.ToString("F0");

        statsMenu.SetActive(true);
    }

    public void BackToMainMenu()
    {
        statsMenu.SetActive(false);
        pauseMenu.SetActive(false);
        HUD.SetActive(false);
        leaderboard.SetActive(false);
        characterSelectionMenu.SetActive(false);
        levelSelectionMenu.SetActive(false);
        shopMenu.SetActive(false);
        optionsMenu.SetActive(false);
        topMenu.SetActive(true);

        //playerRoot.EndRun();
        GameController.gameController.isRunning = false;
        AudioController.audioController.SwitchMusic(0);

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
        if (height > 0) playerNames[index].text = name + ":";
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

    //Não usarei este método por enquanto, deixarei no script de inventário
    private void InitializeStore()
    {
        UpdateStaminaPostionUpgradeUI();
    }

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

    #region Stamina Potion Upgrade

    public void StaminaPotionUpgrade()
    {
        GameController.gameController.inventory.PotionUpgrade();
    }

    public void UpdateStaminaPostionUpgradeUI(int upgradeBonus = 0, int level = 0, int cost = 1000)
    {
        staminaPotionName.text = "Stamina Potion (" + (10 + upgradeBonus) + ")";
        staminaPotionLevel.text = "Lv. " + (level);
        staminaPotionUpgradedIndicator.text = "Stamina Recover (10+" + (level * 5) + ")";
        staminaPotionVisualUpgrade.value = level;
        staminaPotionUpgradeCost.text = cost.ToString();
    }

    #endregion

    #region Shield Updgrade

    public void ShieldChargeUpgrade()
    {
        GameController.gameController.inventory.UpgradeShieldCharges();
    }

    public void UpdateShieldChargeUpgradeUI(int upgradeBonus = 0, int level = 0, int cost = 1000)
    {
        shieldName.text = "Shield (" + (1 + upgradeBonus) + ")";
        shieldLevel.text = "Lv. " + (level);
        shieldChargeUpgradedIndicator.text = "Shield Recover (10+" + (level * 5) + ")";
        shieldChargeVisualUpgrade.value = level;
        shieldChargeUpgradeCost.text = cost.ToString();
    }

    public void UpdateShieldDurationUpgradeUI(int upgradeBonus = 0, int level = 0, int cost = 1000)
    {
        //shieldName.text = "Shield (" + (1 + upgradeBonus) + ")";
        shieldLevel.text = "Lv. " + (level);
        shieldDurationUpgradedIndicator.text = "Shield Recover (10+" + (level * 5) + ")";
        shieldDurationVisualUpgrade.value = level;
        shieldDurationUpgradeCost.text = cost.ToString();
    }

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
