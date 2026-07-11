using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController gameController;

    [Header("Menu")]
    public float coins;
    public float rubies;
    public string playerName = "Player";
    public bool isFirstPlay = true;
    public bool isTutorialIncomplete = true;

    [Header("Leaderboard")]
    private float[] bestHeigths = new float[5];

    [Header("Run Results")]
    public bool isRunning;
    private float x = 0; //Serve para ser somado no menu de estatísticas
    private bool isSfxOn;
    private bool canProccedFirst;
    private bool canProccedSecond;
    private bool canProccedThird;
    private bool canProccedFourth;
    private float height;
    private bool hasBeganCalculating;
    private bool isCalculatingStatistics;
    private bool isCalculatingCoinRewards;
    private bool isCalculatingRubyRewards;
    public float runNormalCoins;
    public float runRubies;
    public float obstaclesDestroyed;
    [SerializeField] private GameObject skipButton;
    private bool canSkipFirst = true;
    private bool canSkipSecond = true;


    [Header("Levels")]
    [SerializeField] private LevelData[] levelArray;
    public levelID currentLevelID = levelID.CowboyLevel;
    //public levelID lastLevelID;
    //public LevelData currentLevelData;
    public int currentLevelCheckpoint = 0;
    public float currentLevelCheckpointDistance;
    public float currentLevelHeight;
    private bool isStartingOnCheckpoint;
    public int activeLevelCode = 0;
    [SerializeField] private GameObject[] startRocks;

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

    private void Update()
    {

        if (!hasBeganCalculating) return;

        if (isCalculatingStatistics)
        {
            CalculateHeight();
            CalculateCoinsCollected();
            CalculateObstaclesDestroyed();
            ProccedToRewardCoins();
        }
        else if (isCalculatingCoinRewards)
        {
            CalulateBaseCoins();
            CalulateHeightMultiplier();
            CalulateObstacleBonus();
            ProccedToRewardRubies();
        }
        else if (isCalculatingRubyRewards)
        {
            CalulateHeightRubies();
            CalulateObstacleRubies();
        }

    }

    public void SkipStatistics()
    {
        if (!hasBeganCalculating) return;

        if (isCalculatingStatistics && canSkipFirst)
        {
            //isCalculatingStatistics = false;
            canSkipFirst = false;

            canProccedFirst = false;
            canProccedSecond = false;
            x = 0;
            canProccedThird = true;
            canProccedFourth = false;

            AudioController.audioController.StopVFXPlay();

            uiController.StaticsMenu(height, runNormalCoins, obstaclesDestroyed, false, true);



            //isCalculatingCoinRewards = true;            

        }
        else if ((isCalculatingCoinRewards || isCalculatingRubyRewards) && canSkipSecond)
        {
            canSkipSecond = false;
            canSkipFirst = true;

            isCalculatingCoinRewards = false;
            isCalculatingRubyRewards = false;

            AudioController.audioController.StopVFXPlay();

            int y = (int)(runNormalCoins * (1 + (height / 10000f)));
            y += (int)(obstaclesDestroyed * 10f);

            uiController.RewardsCoinMenu(runNormalCoins, height / 10000, obstaclesDestroyed * 10f, y, true);

            y = 0;
            if (height / 500 > 1) y = (int)height / 500;
            else y = -1;
            uiController.RewardsRubyMenu(y, 0, 0, true);

            y = 0;
            if (obstaclesDestroyed / 10 > 1) y = (int)obstaclesDestroyed / 10;
            else y = -1;
            uiController.RewardsRubyMenu(0, y, 0, true);

            y = (int)height / 500 + (int)obstaclesDestroyed / 10;
            if (y <= 0) y = -1;
            uiController.RewardsRubyMenu(0, 0, y, true);

            canProccedFirst = false;
            canProccedSecond = false;
            canProccedThird = false;
            canProccedFourth = false;

            skipButton.SetActive(false);
            canSkipSecond = true;
        }


    }

    #region Begin Run Event
    //IPC: ANOTAÇÃO IMPORTANTE LOGO ABAIXO!!!
    public void BeginRun()
    {
        runNormalCoins = 0;
        runRubies = 0;
        uiController.UpdateCoinHUD();
        obstaclesDestroyed = 0;

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
    #endregion

    #region End Run

    public void EndRun(float height)
    {
        isRunning = false;

        this.height = height;

        skipButton.SetActive(true);
        hasBeganCalculating = true;
        isCalculatingStatistics = true;

        AudioController.audioController.StopMusicPlay();

        uiController.StaticsMenu(-1, -1, -1, true, false);

        //Soma das moedas
        //float y = ((runNormalCoins * (1 + (height / 10000f))) + obstaclesDestroyed * 10f);
        int y = (int)(runNormalCoins * (1 + (this.height / 10000f)));
        y += (int)(obstaclesDestroyed * 10f);
        coins += y;

        //Soma dos Rubis
        y = (int)height / 500 + (int)obstaclesDestroyed / 10;
        rubies += y;

        uiController.TopMainMenuUpdate();

        //uiController.StaticsMenu(height, runNormalCoins, runRubies, obstaclesDestroyed);

        UpdateBestHeight(height);
        UpdateLeaderboard(height);
    }

    #region Statistics
    private void CalculateHeight()
    {
        if (canProccedFirst || canProccedSecond || canProccedThird || !isCalculatingStatistics) return;

        if (!isSfxOn)
        {
            AudioController.audioController.SwitchVFXPlay(1, 0);
            isSfxOn = true;
        }

        if (x < height) x += (height / 2f) * Time.deltaTime;
        else x = height;

        uiController.StaticsMenu(x);

        if (x >= height)
        {
            x = height;
            uiController.StaticsMenu(x, 0, 0, false, true);
            x = 0;
            canProccedFirst = true;

            AudioController.audioController.StopVFXPlay();
            isSfxOn = false;
        }
    }

    private void CalculateCoinsCollected()
    {
        if (!canProccedFirst) return;

        if (!isSfxOn)
        {
            AudioController.audioController.SwitchVFXPlay(1, 1);
            isSfxOn = true;
        }

        if (x < runNormalCoins) x += (runNormalCoins / 2f) * Time.deltaTime;
        else x = runNormalCoins;

        uiController.StaticsMenu(0, x);

        if (x >= runNormalCoins)
        {
            x = runNormalCoins;
            uiController.StaticsMenu(0, x, 0, false, true);
            x = 0;
            canProccedSecond = true;
            canProccedFirst = false;

            AudioController.audioController.StopVFXPlay();
            isSfxOn = false;
        }
    }

    private void CalculateObstaclesDestroyed()
    {
        if (!canProccedSecond) return;

        if (!isSfxOn)
        {
            AudioController.audioController.SwitchVFXPlay(1, 2);
            isSfxOn = true;
        }

        if (x < obstaclesDestroyed) x += (obstaclesDestroyed / 2f) * Time.deltaTime;
        else x = obstaclesDestroyed;

        uiController.StaticsMenu(0, 0, x);

        if (x >= obstaclesDestroyed)
        {
            x = obstaclesDestroyed;
            uiController.StaticsMenu(0, 0, x, false, true);
            x = 0;
            canProccedSecond = false;
            canProccedThird = true;

            AudioController.audioController.StopVFXPlay();
            isSfxOn = false;
        }
    }

    private void ProccedToRewardCoins()
    {
        if (!canProccedThird) return;

        if (x < .3f) x += 1 * Time.deltaTime;
        else x = .3f;

        if (x >= .3f)
        {
            x = 1;
            canProccedSecond = false;
            canProccedFirst = false;
            canProccedThird = false;

            isCalculatingStatistics = false;
            isCalculatingCoinRewards = true;
        }

    }

    #endregion

    #region Rewards Coins

    private void CalulateBaseCoins()
    {
        if (canProccedFirst || canProccedSecond || canProccedThird || canProccedFourth) return;

        //if (x < 1) x += 1 * Time.deltaTime;
        //else x = 1;

        //if (x >= 1)
        //{
        //    uiController.RewardsCoinMenu(runNormalCoins, 0, 0, 0, true);
        //}
        //else
        //    uiController.RewardsCoinMenu(runNormalCoins, 0, 0, 0);

        if (x >= 1)
        {
            x = 0;
            uiController.RewardsCoinMenu(runNormalCoins, 0, 0, 0, true);
            AudioController.audioController.SwitchVFXPlay(1, 3);
            canProccedFirst = true;
        }
    }

    private void CalulateHeightMultiplier()
    {
        if (!canProccedFirst) return;

        //float y = runNormalCoins * (1 + (height / 10000f));

        if (x < .3f) x += 1 * Time.deltaTime;
        else x = .3f;

        //uiController.RewardsCoinMenu(0, height / 10000, 0, 0);

        if (x >= .3f)
        {
            x = 0;
            uiController.RewardsCoinMenu(0, height / 10000, 0, 0, true);
            AudioController.audioController.SwitchVFXPlay(1, 3);
            canProccedSecond = true;
            canProccedFirst = false;
        }
    }

    private void CalulateObstacleBonus()
    {
        if (!canProccedSecond) return;

        if (canProccedThird)
        {
            //float y = (int)(runNormalCoins * (1 + (height / 10000f))) + obstaclesDestroyed * 10f;


            int y = (int)(runNormalCoins * (1 + (height / 10000f)));
            y += (int)(obstaclesDestroyed * 10f);

            if (x < y) x += (y / 1.5f) * Time.deltaTime;
            else x = y;

            uiController.RewardsCoinMenu(0, 0, 0, x);

            if (x >= y)
            {
                x = y;
                uiController.RewardsCoinMenu(0, 0, 0, x, true);
                AudioController.audioController.SwitchVFXPlay(1, 4);
                x = 0;
                canProccedFirst = false;
                canProccedSecond = false;
                canProccedThird = false;
                canProccedFourth = true;

                //AudioController.audioController.StopVFXPlay();

                //isCalculatingCoinRewards = false;
                //isCalculatingRubyRewards = true;
                //hasBeganCalculating = false;
            }
        }
        else
        {

            if (x < .3f) x += 1 * Time.deltaTime;
            else x = .3f;

            if (x >= .3f)
            {
                x = 0;
                uiController.RewardsCoinMenu(0, 0, obstaclesDestroyed * 10f, 0, true);
                AudioController.audioController.SwitchVFXPlay(1, 3);
                canProccedThird = true;
            }
        }
    }

    private void ProccedToRewardRubies()
    {
        if (!canProccedFourth) return;

        if (x < 1) x += 1 * Time.deltaTime;
        else x = 1;

        if (x >= 1)
        {
            x = 1;
            canProccedFirst = false;
            canProccedSecond = false;
            canProccedThird = false;
            canProccedFourth = false;

            isCalculatingCoinRewards = false;
            isCalculatingRubyRewards = true;

        }

    }

    #endregion

    #region Rewards Rubies

    private void CalulateHeightRubies()
    {
        if (canProccedFirst || canProccedSecond) return;

        //if (x < 1) x += 1 * Time.deltaTime;
        //else x = 1;

        int y = 0;
        if (height / 500 > 1) y = (int)height / 500;
        else y = -1;
        //if (height / 5000 > 1) y = (int)height / 5000;// DESABILITEI PARA TESTES        

        if (x >= 1)
        {
            uiController.RewardsRubyMenu(y, 0, 0, true);
            AudioController.audioController.SwitchVFXPlay(1, 3);
            x = 0;
            canProccedFirst = true;
        }
    }

    private void CalulateObstacleRubies()
    {
        if (!canProccedFirst) return;

        if (canProccedSecond)
        {

            float y = (int)height / 500 + (int)obstaclesDestroyed / 10;
            //float y = (int)height / 5000 + (int)obstaclesDestroyed / 100; // DESABILITEI PARA TESTES


            if (x < y) x += (y / 1.5f) * Time.deltaTime;
            else x = y;

            uiController.RewardsRubyMenu(0, 0, x);

            if (x >= y)
            {
                x = y;

                if (x <= 0) x = -1;
                else AudioController.audioController.SwitchVFXPlay(1, 5);

                uiController.RewardsRubyMenu(0, 0, x, true);
                //AudioController.audioController.StopVFXPlay();
                x = 0;
                canProccedFirst = false;
                canProccedSecond = false;
                canProccedThird = false;

                skipButton.SetActive(false);

                //isCalculatingCoinRewards = false;
                isCalculatingRubyRewards = false;
                hasBeganCalculating = false;
            }
        }
        else
        {

            if (x < 1) x += 1 * Time.deltaTime;
            else x = 1;

            int y = 0;
            if (obstaclesDestroyed / 10 > 1) y = (int)obstaclesDestroyed / 10;
            else y = -1;
            //if (obstaclesDestroyed / 100 > 1) y = (int)obstaclesDestroyed / 100;

            //uiController.RewardsRubyMenu(0, y, 0);

            if (x >= 1)
            {
                uiController.RewardsRubyMenu(0, y, 0, true);
                AudioController.audioController.SwitchVFXPlay(1, 3);
                x = 0;
                canProccedSecond = true;
            }
        }


    }

    #endregion

    #endregion

    #region Leaderboard Temp

    private void UpdateLeaderboard(float heigth)
    {
        for (int i = 0; i < bestHeigths.Length; i++)
        {
            if (heigth > bestHeigths[i])
            {
                for (int j = (bestHeigths.Length - 1); j > i; j--)
                {
                    bestHeigths[j] = bestHeigths[j - 1];
                }

                bestHeigths[i] = heigth;
                i += bestHeigths.Length;
            }
        }



        UpdateLeaderboarUI();
    }

    public void UpdateLeaderboarUI()
    {
        for (int i = 0; i < bestHeigths.Length; i++)
        {
            uiController.UpdateLeaderboardDisplay(i, playerName, bestHeigths[i]);
        }
    }

    #endregion

    #region Run

    public void UpdateRunCoins(int normalCoins = 0, int rubies = 0)
    {
        if (!isRunning) return;

        runNormalCoins += (int)(normalCoins * playerRoot.playerPowers.coinMultiplier);

        runRubies += rubies;

        uiController.UpdateCoinHUD(runNormalCoins);
    }

    public void ObstaclesDestroyedCounter()
    {
        obstaclesDestroyed++;
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

        for (int i = 0; i < startRocks.Length; i++)
        {
            startRocks[i].SetActive(false);
        }

        startRocks[activeLevelCode].SetActive(true);

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
