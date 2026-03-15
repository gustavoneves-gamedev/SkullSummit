using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{

    [Header("MainMenu")]
    [SerializeField] private GameObject mainMenu;//É tudo no Menu Principal
    //[SerializeField] private GameObject menu;
    [SerializeField] private GameObject tapToPlayText;

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
    [SerializeField] private Slider staminaSlider;

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
        levelSelectionMenu.SetActive(false);
        HUD.SetActive(false);

        Invoke("Initialize", .1f);
    }

    private void Initialize()
    {
        playerRoot = GameController.gameController.playerRoot;
    }

    public void BeginRun()
    {
        mainMenu.SetActive(false);
        statsMenu.SetActive(false);

        GameController.gameController.InitilizeLevelStatics();
        GameController.gameController.BeginRun();

        HUD.SetActive(true); //Colocar um efeito de fade in aqui 
    }


    //MUDAR ISSO PARA ALTERAR O TIME SCALE PARA ZERO. DO CONTRÁRIO ESTAREI PARANDO APENAS O PLAYER!!
    public void PauseMenu()
    {
        //Temporariamente a pause vai direto para o Menu principal        
        pauseMenu.SetActive(!pauseMenu.activeSelf);

        playerRoot.canRun = !playerRoot.canRun;

        //playerRoot.EndRun();
    }

    //Talvez eu possa criar um comando para encerrar a corrida antes do fim no modo infinito? Verificar pertinência
    //disso no futuro
    public void StaticsMenu(float height = 0, int coins = 0, int rubies = 0, int obstaclesDestroyed = 0)
    {
        pauseMenu.SetActive(false);
        HUD.SetActive(false);

        heightClimbed.text = "Altura Escalada: " + height;
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
        levelSelectionMenu.SetActive(false);

        //playerRoot.EndRun();
        GameController.gameController.isRunning = false;
        if (!isLevelSelecting)
        {
            GameController.gameController.InitilizeLevelStatics();
            GameController.gameController.ResetPlayerPosition();
        }

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
    }

    public void SelectSamurai()
    {
        GameController.gameController.playerRoot.selectedCharacter = characterID.Samurai;
        GameController.gameController.playerRoot.Initialize(characterID.Samurai);
    }

    public void SelectAlpinista()
    {
        GameController.gameController.playerRoot.selectedCharacter = characterID.Alpinista;
        GameController.gameController.playerRoot.Initialize(characterID.Alpinista);
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

    public void UpdateHUD(float stamina)
    {
        staminaSlider.value = stamina;
    }

    #endregion
}
