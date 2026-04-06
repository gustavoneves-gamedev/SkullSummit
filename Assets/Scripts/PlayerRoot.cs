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
    private Vector3 move;
    private int desiredLane = 0;
    public bool isChangingLane; //IPC: Não está funcionando ainda porque vou colocar um trigger no meio das lanes para
                                //o script detectar quando o jogador finalizar a troca de lane

    [Header("Status")]
    public float currentStamina;
    public float maxStamina;
    [SerializeField] private float movementSpeed;
    public float initialSpeed;
    private float defaultSpeed;
    public float maxSpeed;
    public float aceleration;
    private float acelerationCooldown;
    private float defaultAcelerationCooldown = 10f;
    [SerializeField] private float horizontalSpeed;

    [Header("Attack")]
    [SerializeField] private GameObject bulletPrefab;
    private bool canAttack;
    private float attackSpeed = 50f; //Valor temporário fixo, mas deverá variar de acordo com o personagem
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
    public float normalCoinMultiplier = 1;
    public PlayerPowers playerPowers;
    //public int rubyMultiplier = 1;

    [Header("Touch config")]
    [SerializeField] private float swipeDistance = 100f;
    private float lastTapTime;
    private int tapCount;
    private Vector2 startTouch;
    private bool hasSwipe;
    private float touchTime;

    [Header("Scene Plane")]
    [SerializeField] private GameObject scenePlane;
    private Vector3 originalPosition;

    [Header("References")]
    [SerializeField] private CharacterController cc;
    public characterID selectedCharacter = characterID.Cowboy;
    [SerializeField] private CharacterData[] characterDatas;
    [SerializeField] private GameObject[] characterModels;
    private AudioSource audioSource;


    void Start()
    {
        GameController.gameController.playerRoot = this;
        audioSource = GetComponent<AudioSource>();
        playerPowers = GetComponent<PlayerPowers>();

        originalPosition = scenePlane.transform.position;
        scenePlane.transform.localPosition = originalPosition;

        Initialize(selectedCharacter);

    }

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
        defaultSpeed = initialSpeed;
        maxSpeed = movementSpeed * 1.1f;

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
        scenePlane.transform.localPosition = originalPosition;

        isDead = false;
        desiredLane = 0;
        currentStamina = maxStamina;
        initialSpeed = defaultSpeed;
        movementSpeed = initialSpeed;
        acelerationCooldown = defaultAcelerationCooldown;
        currentAmmo = maxAmmo;
        cooldownRemaining = 0;
        reloadTimeRemaining = reloadTime;
        heightClimbed = 0;
        initialHeight = transform.position.z;
        playerPowers.ResetPowers(); //Serve para resetar os poderes do Player
                                    //PROVAVELMENTE IREI MUDAR PARA SER CHAMADO AO FIM DA RUN
        canRun = true;
    }

    void Update()
    {

        //Pause
        if (Input.GetKeyDown(KeyCode.Escape) && GameController.gameController.isRunning)
        {
            GameController.gameController.uiController.PauseMenu();
        }

        if (canRun == false || isDead || isGamePaused) return;

        //Calcula a altura escalada pelo jogador - APRIMORAR
        heightClimbed = transform.position.z - initialHeight;

        DetectSwipes();
        DetectTaps();

        PlayerMovement();
        SpeedScale();
        AttackTimeCounter();
        StaminaConsumption();

        scenePlane.transform.localPosition += Vector3.back * 0.4f * Time.deltaTime;


        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (canAttack == true)
                Attack();
            else if (currentAmmo <= 0)
                Debug.Log("Sem munição suficiente");
            else if (cooldownRemaining >= 0)
                Debug.Log("Ataque em cooldown ainda");
        }

    }

    #region Mobile Inputs

    private void DetectSwipes()
    {
        if (Input.touchCount == 1)
        {
            Touch t = Input.GetTouch(0);

            if (t.phase == TouchPhase.Began)
            {
                startTouch = t.position;
                touchTime = Time.time;
                hasSwipe = false;
            }
            else if (t.phase == TouchPhase.Ended)
            {
                Vector2 delta = t.position - startTouch;

                if (delta.magnitude > swipeDistance)
                {
                    hasSwipe = true;

                    if (Mathf.Abs(delta.x) > Mathf.Abs(delta.y))
                    {
                        if (delta.x > 0)
                        {
                            //Debug.Log("Swipe Right");
                            if (canRun && desiredLane < 1 && !isChangingLane)
                            {
                                desiredLane = desiredLane + 1;
                                tapCount = 0;
                                //isChangingLane = true;
                            }
                        }
                        else
                        {
                            //Debug.Log("Swipe Left");                            
                            if (canRun && desiredLane > -1 && !isChangingLane)
                            {
                                desiredLane = desiredLane - 1;
                                tapCount = 0;
                                //isChangingLane = true;
                            }
                        }
                    }
                    else
                    {
                        if (delta.y > 0)
                        {
                            //Debug.Log("Swipe Up");

                        }
                        else
                        {
                            //Debug.Log("Swipe Down");
                        }
                    }
                }
            }
        }
    }

    private void DetectTaps()
    {
        if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Ended) //|| Input.GetKeyDown(KeyCode.Mouse0)
        {
            if (Time.time - touchTime > 0.15f || hasSwipe)
            {
                tapCount = 0;
                return;
            }


            float timeNow = Time.time;

            if (timeNow - lastTapTime < 0.2f)
                tapCount++;
            else
                tapCount = 1;

            lastTapTime = timeNow;

            if (tapCount == 1)
            {
                if (canAttack == true)
                    Attack();
                else if (currentAmmo <= 0)
                    Debug.Log("Sem munição suficiente");
                else if (cooldownRemaining >= 0)
                    Debug.Log("Ataque em cooldown ainda");
            }

            //if (tapCount == 2)
            //{
            //    CancelInvoke();
            //    Invoke("DoubleTap", 0.3f);
            //}
            //if (tapCount == 3)
            //{
            //    CancelInvoke();
            //    Invoke("TripleTap", 0.2f); //Queria colocar instantâneo para o triple,
            //                               //mas decidi só reduzir o tempo para manter o paralelismo
            //}
        }
    }

    #endregion

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

        initialSpeed = defaultSpeed;

        //Serve para o jogador não ficar muito lento após colidir em alturas mais altas
        if (heightClimbed >= 1500 && heightClimbed < 4000) initialSpeed *= 1.2f;
        else if (heightClimbed >= 4000 && heightClimbed < 10000) initialSpeed *= 1.3f;
        else if (heightClimbed >= 10000) initialSpeed *= 1.4f;

    }

    #endregion

    #region Stamina Management
    private void StaminaConsumption()
    {
        currentStamina -= ((2 - resistance / 10f) * Time.deltaTime);

        if (currentStamina <= 0 && !isDead)
        {
            currentStamina = 0;
            OnDeathEvent();
        }
    }

    //Esta função serve para regenerar a stamina e reduzi-la ao colidir com obstáculos
    public void UpdateStamina(float x)
    {
        if (x < 0 && playerPowers.isShieldUp)
        {
            playerPowers.Shield(x);
            return;
        }

        currentStamina += x;

        if (x < 0)
        {
            audioSource.Play();
            SpeedReset();
        }

        if (currentStamina > maxStamina)
            currentStamina = maxStamina;

        if (currentStamina <= 0 && !isDead)
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
        //Debug.Log("Ataquei!!");
        GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
        Bullet bulletScript = bullet.GetComponent<Bullet>();
        bulletScript.bulletSpeed = movementSpeed + attackSpeed;
        // bulletScript.Movement();
        currentAmmo--;
        cooldownRemaining = cooldown;
        canAttack = false;
    }

    #endregion


    private void OnDeathEvent()
    {
        canRun = false;
        isDead = true;

        GameController.gameController.EndRun(heightClimbed);
    }

    private void OnTriggerEnter(Collider other)
    {

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
