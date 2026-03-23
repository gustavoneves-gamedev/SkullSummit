using UnityEngine;

public class PlayerRoot : MonoBehaviour
{
    //TENHO QUE REVER AS FUNÇÕES QUE ENVOLVEM A ALTURA!!
    [Header("Run")]
    public bool canRun;
    private bool isDead;
    public bool isGamePaused;
    public float heightClimbed;
    private float initialHeight;
    //public float runHeightClimbed;
    public float totalHeight; //Valor que será mostrado ao final e durante a run
    private int coinsCollected; //Valor que será mostrado ao final e durante a run
    private int rubiesCollected; //Valor que será mostrado ao final e durante a run
    private int obstaclesDestroyed; //Valor que será mostrado ao final e durante a run
    //private bool canCountCheckpoint;
    private Vector3 move;
    private int desiredLane = 0;
    public bool isChangingLane; //IPC: Não está funcionando ainda porque vou colocar um trigger no meio das lanes para
                                //o script detectar quando o jogador finalizar a troca de lane

    [Header("Status")]
    public float currentStamina;
    public float maxStamina;
    [SerializeField] private float movementSpeed;
    public float initialSpeed;
    public float maxSpeed;
    public float aceleration;
    private float acelerationCooldown;
    private float defaultAcelerationCooldown = 10f;
    [SerializeField] private float horizontalSpeed;

    [Header("Attack")]
    [SerializeField] private GameObject bulletPrefab;
    private bool canAttack;
    public float damage;
    public float cooldown;
    private float cooldownRemaining; //Esta variável pode ficar apenas aqui
    public int maxAmmo;
    public int currentAmmo; //Esta variável pode ficar apenas aqui
    public float reloadTime;
    private float reloadTimeRemaining; //Esta variável pode ficar apenas aqui

    
    public float defense;
    public float resistance;


    [Header("PowerUps")]
    public int normalCoinMultiplier = 1;
    public int rubyMultiplier = 1;


    [Header("References")]
    [SerializeField] private CharacterController cc;
    public characterID selectedCharacter = characterID.Cowboy;
    [SerializeField] private CharacterData[] characterDatas;
    [SerializeField] private GameObject[] characterModels;


    void Start()
    {
        GameController.gameController.playerRoot = this;
        Initialize(selectedCharacter);

    }

    //Tive que fazer desta forma porque o CharacterController não estava deixando reposicionar
    //AVALIAR SE PRECISAREI USAR UM INVOKE PARA DAR TEMPO DE O JOGADOR CHEGAR NA NOVA POSIÇÃO!! (ACHO QUE NÃO)
    public void ResetPosition(Vector3 worldPos)
    {
        cc.enabled = false;

        //transform.position = worldPos + Vector3.forward;
        transform.position = worldPos;

        cc.enabled = true;
    }

    public void Initialize(characterID selectedChar)
    {
        normalCoinMultiplier = 1;
        rubyMultiplier = 1;

        //Serve para atualizar o script do progress manager e pegar o incremento correto
        ProgressManager.progressManager.UpdateIncrement(selectedChar);

        //Esconde todos os modelos
        for (int i = 0; i < characterModels.Length; i++)
        {
            characterModels[i].SetActive(false);
        }

        if (selectedChar == characterID.Cowboy)
            InitializePlayer(0);

        if (selectedChar == characterID.Samurai)
            InitializePlayer(1);

        if (selectedChar == characterID.Alpinista)
            InitializePlayer(2);

    }
    

    private void InitializePlayer(int charCode)
    {
        maxStamina = characterDatas[charCode].baseMaxStamina + ProgressManager.progressManager.staminaIncrement;

        movementSpeed = characterDatas[charCode].baseMovementSpeed + ProgressManager.progressManager.movementSpeedIncrement;
        initialSpeed = movementSpeed * 0.6f;
        maxSpeed = movementSpeed * 1.2f;

        damage = characterDatas[charCode].baseDamage + ProgressManager.progressManager.damageIncrement;

        cooldown = characterDatas[charCode].baseCooldown + ProgressManager.progressManager.cooldownIncrement;

        maxAmmo = characterDatas[charCode].baseAmmo + ProgressManager.progressManager.ammoIncrement;

        reloadTime = characterDatas[charCode].reloadTime + ProgressManager.progressManager.reloadIncrement;

        defense = characterDatas[charCode].baseDefense + ProgressManager.progressManager.defenseIncrement;

        resistance = characterDatas[charCode].baseResistance + ProgressManager.progressManager.resistanceIncrement;

        characterModels[charCode].SetActive(true); //Ativa o modelo do personagem selecionado

        currentStamina = maxStamina;

        //O BeginRunAnimation vai primeiro atualizar as informações do jogador e depois
        //canRun = true;

    }


    public void BeginRunAnimation()
    {

        //PlayRunAnimation -> Terei que elaborar isso aqui, definir qual animação deverá tocar (jogador está no checkpoin
        //ou no início da Run?)

        //Depois disso a animação terá um event trigger que irá mudar a bool isRunning para true. Para testes, irei colocar 
        //aqui o gatilho para começar a corrida

        BeginRunEvent();
    }

    //Reseta os valores para a nova Run
    private void BeginRunEvent()
    {
        isDead = false;
        desiredLane = 0;
        currentStamina = maxStamina;
        movementSpeed = initialSpeed;
        acelerationCooldown = defaultAcelerationCooldown;
        coinsCollected = 0;
        rubiesCollected = 0;
        obstaclesDestroyed = 0;
        currentAmmo = maxAmmo;
        cooldownRemaining = 0;
        reloadTimeRemaining = reloadTime;
        heightClimbed = 0;
        initialHeight = transform.position.z;
        canRun = true;
    }



    void Update()
    {
        if (canRun == false || isDead || isGamePaused) return;

        //Calcula a altura escalada pelo jogador - APRIMORAR
        heightClimbed = transform.position.z - initialHeight;

        PlayerMovement();
        SpeedScale();
        AttackTimeCounter();
        StaminaConsumption();

        //Atualiza a barra de stamina na HUD
        GameController.gameController.uiController.UpdateHUD(currentStamina / maxStamina);


        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (canAttack == true)
                Attack();
            else if (currentAmmo <= 0)
                Debug.Log("Sem munição suficiente");
            else if (cooldownRemaining >= 0)
                Debug.Log("Ataque em cooldown ainda");
        }

        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    GameController.gameController.UpdateCheckpoint();
        //}
        
    }

    #region Movement
    private void PlayerMovement()
    {
        move = transform.forward * movementSpeed;

        if (Input.GetKeyDown(KeyCode.D) && desiredLane < 1 && !isChangingLane)
        {
            desiredLane = desiredLane + 1;
            //isChangingLane = true;
        }

        if (Input.GetKeyDown(KeyCode.A) && desiredLane > -1 && !isChangingLane)
        {
            desiredLane = desiredLane - 1;
            //isChangingLane = true;
        }

        float targetX = Mathf.Lerp(transform.position.x, desiredLane * 4, horizontalSpeed * Time.deltaTime);

        move.x = (targetX - transform.position.x) / Time.deltaTime;      


        cc.Move(move * Time.deltaTime);
    }

    private void SpeedScale()
    {
        acelerationCooldown -= ((1 / (2 - aceleration)) * Time.deltaTime);

        if (acelerationCooldown >= 0 || movementSpeed >= maxSpeed) return;

        movementSpeed += .5f * Time.deltaTime;

        if (movementSpeed >= maxSpeed) movementSpeed = maxSpeed;
    }

    //Irei chamar esta função lá no script de obstáculo para que apenas acionar quando o jogador colidir
    //evitando que seja chamada quando o jogador realizar alguma outra ação que consuma stamina
    //CASO a gente decida que ele não consumirá stamina ao atacar ou trocar de lane, poderei mover esta função
    //para dentro do stamina update e chamar caso o valor passado seja negativo
    public void SpeedReset()
    {
        acelerationCooldown = defaultAcelerationCooldown;
        movementSpeed = initialSpeed;
    }

    #endregion

    #region Stamina Management
    private void StaminaConsumption()
    {
        currentStamina -= ((2 - resistance / 10f) * Time.deltaTime);

        if (currentStamina <= 0)
        {
            currentStamina = 0;
            OnDeathEvent();
        }
    }

    //Esta função serve para regenerar a stamina e talvez reduzi-la ao longo da Run
    //AINDA NÃO ESTÁ SENDO UTILIZADA!!
    public void UpdateStamina(float x)
    {
        currentStamina += x;

        if (currentStamina > maxStamina)
            currentStamina = maxStamina;

        if (currentStamina <= 0)
        {
            currentStamina = 0;
            OnDeathEvent();
        }
    }

    #endregion

    #region Attack
    private void AttackTimeCounter()
    {
        if (currentAmmo < maxAmmo)
        {
            if (reloadTimeRemaining <= 0)
            {
                currentAmmo++;
                reloadTimeRemaining = reloadTime;
            }
            else
                reloadTimeRemaining -= Time.deltaTime;
        }

        if (cooldownRemaining <= 0 && currentAmmo >= 1)
        {
            canAttack = true;
        }
        else if (cooldownRemaining >= 0)
        {
            cooldownRemaining -= Time.deltaTime;
        }

    }

    private void Attack()
    {
        Debug.Log("Ataquei!!");
        Instantiate(bulletPrefab, transform.position, transform.rotation);
        currentAmmo--;
        cooldownRemaining = cooldown;
        canAttack = false;
    }

    #endregion


    //O CONTROLE DE PAUSE ESTÁ NO GAME CONTROLLER POR ENQUANTO!!!
    private void Pause()
    {
        canRun = false;
        GameController.gameController.uiController.BackToMainMenu();
    }


    private void OnDeathEvent()
    {
        canRun = false;
        isDead = true;

        EndRun();
    }

    //Coloquei esta função abaixo para o caso de o jogador ganhar a escalada e não encerrá-la com sua morte
    //Outra observação, o ciclo está estranho. O Game Controller chama esta função que depois chama o próprio game controller
    //Manterei por enquanto, mas vou tentar otimizar no futuro
    public void EndRun()
    {


        GameController.gameController.UpdateBestHeight(heightClimbed);

        GameController.gameController.uiController.
            StaticsMenu(heightClimbed, coinsCollected, rubiesCollected, obstaclesDestroyed);

        GameController.gameController.isRunning = false;
    }

    //Talvez eu deva criar um script de moedas para colocar isso tudo lá e
    //tocar o som delas quando o jogador as coletar. VER COM PROFESSOR O QUE PESA MENOS
    //OU POSSO COLOCAR OS SONS AQUI E TOCAR QUANDO COLETAR AS MOEDAS!! - VOU FAZER ISTO
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coin"))
        {
            GameController.gameController.UpdateRunCoins(normalCoinMultiplier);
            other.gameObject.SetActive(false);
        }

        if (other.CompareTag("Ruby"))
        {
            GameController.gameController.UpdateRunCoins(0, rubyMultiplier);
            other.gameObject.SetActive(false);
        }

        //Este trigger serve para spawnar um novo conjunto de prefabs após passar pelo checkpoint 
        if (other.CompareTag("UpdatePrefabMarker"))
        {
            //VIDE ANOTAÇÃO NO LEVEL DATA QUANTO AO CÁLCULO DE QUANDO OS PREFABS SERÃO ATUALIZADOS
            GameController.gameController.UpdatePrefab();
            
        }

        if (other.CompareTag("Checkpoint"))
        {
            GameController.gameController.UpdateCheckpoint();
            UpdateStamina(maxStamina * 0.1f);
        }



        if (other.CompareTag("LevelSpawnTrigger"))
        {
            GameController.gameController.levelManager.SpawnLevelPrefab();
        }

        if (other.CompareTag("LevelDestroyer"))
        {
            GameController.gameController.levelManager.DestroyLevelPrefab();
        }

    }

}
