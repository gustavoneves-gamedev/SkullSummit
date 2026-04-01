using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{

    [Header("MainMenu")]
    [SerializeField] private GameObject mainMenu;//É tudo no Menu Principal
    //[SerializeField] private GameObject menu;
    [SerializeField] private GameObject tapToPlayText;

    [Header("TopMenu")]
    [SerializeField] private TextMeshProUGUI coins;
    [SerializeField] private TextMeshProUGUI rubies;

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

    [Header("Run")]
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject HUD;
    [SerializeField] private TextMeshProUGUI runCoins;

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
        pauseMenu.SetActive(false);
        pauseMenu.SetActive(false);
        characterSelectionMenu.SetActive(false);
        levelSelectionMenu.SetActive(false);
        HUD.SetActive(false);

        Invoke("Initialize", .1f);
    }

    void Update()
    {
        if(!GameController.gameController.isRunning) return;
        
        UpdateStaminaHUD(playerRoot.currentStamina / playerRoot.maxStamina);
    }

    private void Initialize()
    {
        playerRoot = GameController.gameController.playerRoot;
    }

    #region General Menu

    public void BeginRun()
    {
        mainMenu.SetActive(false);
        statsMenu.SetActive(false);

        GameController.gameController.InitilizeLevelStatics();
        GameController.gameController.BeginRun();

        HUD.SetActive(true); //Colocar um efeito de fade in aqui 
    }

    public void TopMenu()
    {
        //Criar método para atualizar script das currencies
    }

    //MUDAR ISSO PARA ALTERAR O TIME SCALE PARA ZERO. DO CONTRÁRIO ESTAREI PARANDO APENAS O PLAYER!!
    public void PauseMenu()
    {
        pauseMenu.SetActive(!pauseMenu.activeSelf);

        playerRoot.canRun = !playerRoot.canRun;

        //TEMPORÁRIO - REVISAR!! 28/03/2026
        if(playerRoot.canRun) Time.timeScale = 1;
        else Time.timeScale = 0;

        //playerRoot.EndRun();
    }

    
    public void StaticsMenu(float height = 0, int coins = 0, int rubies = 0, int obstaclesDestroyed = 0)
    {
        pauseMenu.SetActive(false);
        HUD.SetActive(false);

        heightClimbed.text = "Altura Escalada: " + height.ToString("F0");
        coinsCollected.text = "Moedas: " + coins;
        rubiesCollected.text = "Rubis: " + rubies;
        obstacles.text = "Obstáculos: " + obstaclesDestroyed;

        statsMenu.SetActive(true);
    }

    public void BackToMainMenu()
    {
        statsMenu.SetActive(false);
        pauseMenu.SetActive(false);
        HUD.SetActive(false);
        characterSelectionMenu.SetActive(false);
        levelSelectionMenu.SetActive(false);

        //playerRoot.EndRun();
        GameController.gameController.isRunning = false;

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

    #endregion
}
