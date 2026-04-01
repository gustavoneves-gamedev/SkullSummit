using UnityEngine;
using UnityEngine.LowLevel;

public class GameController : MonoBehaviour
{
    public static GameController gameController;

    [Header("Menu")]
    public int coins;
    public int rubies;
    
    [Header("Run")]
    public bool isRunning;
    public int runNormalCoins;
    public int runRubies;
    
    [Header("Levels")]
    [SerializeField] private LevelData[] levelArray;
    public levelID currentLevelID = levelID.CowboyLevel;
    //public levelID lastLevelID;
    //public LevelData currentLevelData;
    public int currentLevelCheckpoint = 0;
    public float currentLevelCheckpointDistance;
    public float currentLevelHeight;
    private bool isStartingOnCheckpoint;

    [Header("Cowboy Level")]
    public int cowboyLevelCheckpoint;
    public float cowboyLevelBestHeight;

    [Header("Samurai Level")]
    public int samuraiLevelCheckpoint;
    public float samuraiLevelBestHeight;

    [Header("Alpinista Level")]
    public int alpinistaLevelCheckpoint;
    public float alpinistaLevelBestHeight;    

    [Header("References")]
    public PlayerRoot playerRoot;
    public PlayerPowers playerPowers;
    public Inventory inventory;
    public UIController uiController;
    public LevelManager levelManager;
    public ObstacleManager obstacleManager;
    
    
    void Awake()
    {
        if (gameController == null)
        {
            gameController = this;
        }
        else
        {
            Destroy(gameObject);
        }

#if UNITY_ANDROID
        Application.targetFrameRate = 60;
        QualitySettings.vSyncCount = 0;
#endif
    }

    private void Start()
    {
        //currentLevelID = lastLevel;
        InitilizeLevelStatics();
    }

    //IPC: ANOTAÇÃO IMPORTANTE LOGO ABAIXO!!!
    public void BeginRun()
    {
        runNormalCoins = 0;
        runRubies = 0;
        
        //Isto servirá para o jogador escolher se quer começar do checkpoint ou do zero REVISAR!!!
        if (!isStartingOnCheckpoint)
            currentLevelCheckpoint = 0;        

        ResetPlayerPosition();

        playerRoot.BeginRunAnimation();
        isRunning = true;
    }

    public void ResetPlayerPosition()
    {
        //Reseta a posição do jogador (Esse valor é o Y do Player na cena, estou colocando aqui em vez de zerar para
        //evitar que o jogador dê uma flicada)
        Vector3 worldPos = Vector3.up * 0.79f;

        playerRoot.ResetPosition(worldPos);
    }

    private void Update()
    {
        
    }

    #region Main Menu



    #endregion

    #region Run

    public void UpdateRunCoins(int normalCoins = 0, int rubies = 0)
    {
       
        runNormalCoins += Mathf.RoundToInt(normalCoins * playerRoot.normalCoinMultiplier);

        runRubies += rubies;
    }


    #endregion

    #region Level
        
    public void InitilizeLevelStatics()
    {
        
        for (int i = 0; i < levelArray.Length; i++)
        {
            if (currentLevelID == levelArray[i].levelId)
            {
                currentLevelCheckpointDistance = levelArray[i].checkpointDistance;
                
            }
        }

        levelManager.InitializeLevel(currentLevelID, currentLevelCheckpointDistance, currentLevelHeight);

    }

    //Esta função apenas atualiza o próximo conjunto de prefabs e não o checkpoint em si, por isso a informação passada
    //é o checkpoint + 1
    //Talvez seja melhor colocar isso no Level Manager diretamente depois
    public void UpdatePrefab()
    {
        //currentLevelCheckpoint++;        

        if (currentLevelID == levelID.CowboyLevel)
        {
            //cowboyLevelCheckpoint++;
            //currentLevelCheckpoint++;
            levelManager.UpdateLevelPrefabs(cowboyLevelCheckpoint + 1);
            return;
        }

        if (currentLevelID == levelID.SamuraiLevel)
        {
            //samuraiLevelCheckpoint++;
            //currentLevelCheckpoint++;
            return;
        }

        if (currentLevelID == levelID.AlpinistaLevel)
        {
            //alpinistaLevelCheckpoint++;
            return;
        }

    }

    public void UpdateCheckpoint()
    {
        currentLevelCheckpoint++;

        if (currentLevelID == levelID.CowboyLevel)
        {
            cowboyLevelCheckpoint++;
            //currentLevelCheckpoint++;
            //levelManager.UpdateLevelPrefabCheckpoint(cowboyLevelCheckpoint);
            levelManager.SpawnCheckpoint(currentLevelCheckpointDistance, cowboyLevelCheckpoint);
            //ACHO QUE É POSSÍVEL OTIMIZAR ESSA QUESTÃO DO CURRENTLEVELCHECKPOINTDISTANCE EM VEZ DE TER QUE FICAR
            //PASSANDO ESSA INFORMAÇÃO TODA HORA
            return;
        }

        if (currentLevelID == levelID.SamuraiLevel)
        {
            samuraiLevelCheckpoint++;
            //currentLevelCheckpoint++;
            return;
        }

        if (currentLevelID == levelID.AlpinistaLevel)
        {
            alpinistaLevelCheckpoint++;
            return;
        }

    }

    public void UpdateBestHeight(float totalHeight)
    {
        if (currentLevelID == levelID.CowboyLevel && totalHeight > cowboyLevelBestHeight)
        {
            cowboyLevelBestHeight = totalHeight;
            //currentLevelCheckpoint++;
            return;
        }

        if (currentLevelID == levelID.SamuraiLevel && totalHeight > samuraiLevelBestHeight)
        {
            samuraiLevelBestHeight = totalHeight;
            //currentLevelCheckpoint++;
            return;
        }

        if (currentLevelID == levelID.AlpinistaLevel && totalHeight > alpinistaLevelBestHeight)
        {
            alpinistaLevelBestHeight = totalHeight;
            return;
        }
    }

    #endregion

    #region Store


    #endregion

}
