using UnityEngine;

public class UIAnimation : MonoBehaviour
{
    [Header("Cowboy")]
    [SerializeField] private RectTransform cowboyImage;
    [SerializeField] private CanvasGroup cowboyCanvasGroup;

    [Header("Cowboy Upgrade")]
    [SerializeField] private RectTransform[] upgradeCowboyCards;
    [SerializeField] private CanvasGroup[] upgradeCowboyCanvasGroups;

    [Header("Samurai")]
    [SerializeField] private RectTransform samuraiImage;
    [SerializeField] private CanvasGroup samuraiCanvasGroup;

    [Header("Samurai Upgrade")]
    [SerializeField] private RectTransform[] upgradeSamuraiCards;
    [SerializeField] private CanvasGroup[] upgradeSamuraiCanvasGroups;

    [Header("Dullahan")]
    [SerializeField] private RectTransform dullahanImage;
    [SerializeField] private CanvasGroup dullahanCanvasGroup;

    [Header("Dullahan Upgrade")]
    [SerializeField] private RectTransform[] upgradeDullahanCards;
    [SerializeField] private CanvasGroup[] upgradeDullahanCanvasGroups;

    [Header("Select Button")]
    [SerializeField] private RectTransform selectCowboyButton;
    [SerializeField] private CanvasGroup selectCowboyButtonCanvasGroup;
    [SerializeField] private RectTransform selectSamuraiButton;
    [SerializeField] private CanvasGroup selectSamuraiButtonCanvasGroup;
    [SerializeField] private RectTransform selectDullahanButton;
    [SerializeField] private CanvasGroup selectDullahanButtonCanvasGroup;

    [Header("Offsets")]
    [SerializeField] private Vector2 characterStartOffset = new Vector2(-900f, 0f);
    [SerializeField] private Vector2 upgradeStartOffset = new Vector2(900f, 0f);
    [SerializeField] private Vector2 buttonStartOffset = new Vector2(0f, -250f);

    [Header("Duration")]
    [SerializeField] private float characterDuration = 0.45f;
    [SerializeField] private float upgradeDuration = 0.35f;
    [SerializeField] private float buttonDuration = 0.30f;

    [Header("Delays")]
    [SerializeField] private float firstUpgradeDelay = 0.12f;
    [SerializeField] private float upgradeStaggerDelay = 0.10f;
    [SerializeField] private float buttonDelay = 0.45f;

    [Header("Configs")]
    [SerializeField] private bool playOnEnable = true;
    [SerializeField] private bool ignoreTimeScale = true;

    private Vector2 cowboyCharacterFinalPosition;
    private Vector2[] upgradeCowboyFinalPositions;
    private Vector2 buttonFinalPosition;

    private Vector2 samuraiCharacterFinalPosition;
    private Vector2[] upgradeSamuraiFinalPositions;

    private Vector2 dullahanCharacterFinalPosition;
    private Vector2[] upgradeDullahanFinalPositions;

    private bool cachedCowboyPositions;
    private bool cowboyFirstTime = true;
    private bool cachedSamuraiPositions;
    private bool samuraiFirstTime = true;
    private bool cachedDullahanPositions;
    private bool dullahanFirstTime = true;
    private float initialDelay = 0f;


    private void Awake()
    {
        CacheFinalPositions(0);
        CacheFinalPositions(1);
        CacheFinalPositions(2);
    }

    private void OnEnable()
    {
        if (playOnEnable)
        {
            PlayEntranceAnimation();
        }
    }

    private void OnDisable()
    {
        CancelTweens();

        //if (cachedCowboyPositions)
        //{
        //    SetFinalState();
        //}
    }

    private void Start()
    {
        // Invoke("Initialize", .4f);
    }

    private void Initialize()
    {
        PlayEntranceAnimation(0);
        PlayEntranceAnimation(1);
        PlayEntranceAnimation(2);

        //CacheFinalPositions(0);
        //CacheFinalPositions(1);
        //CacheFinalPositions(2);
    }

    public void PlayEntranceAnimation(int charCode = -1)
    {
        if (!cachedCowboyPositions)
        {
            CacheFinalPositions(0);
            Debug.Log("Cowboy foi chamado");
        }

        if (!cachedSamuraiPositions)
        {
            CacheFinalPositions(1);
            Debug.Log("Samurai foi chamado");
        }

        if (!cachedDullahanPositions)
        {
            CacheFinalPositions(2);
            Debug.Log("Dullahan foi chamado");
        }

        // CacheFinalPositions(charCode);
        CancelTweens(charCode);
        SetStartState(charCode);

        AnimateCharacter(charCode);
        AnimateUpgradeCards(charCode);
        AnimateSelectButton(); //NÃO ESTÁ FUNCIONANDO AINDA!!
    }

    public void FirstCall(int charCode = 0, bool wasFirstCall = false)
    {
        if (!wasFirstCall) return;
        
        if (charCode == 0) cowboyFirstTime = false;
        else if (charCode == 1) samuraiFirstTime = false;
        else if (charCode == 2) dullahanFirstTime = false;
    }

    public void SkipAnimation()
    {
        CancelTweens();
        //SetFinalState();
    }

    public void CacheFinalPositions(int charCode = -1)
    {

        //charCode = 0;
        if (charCode == 0)
        {

            Canvas.ForceUpdateCanvases();

            cowboyCharacterFinalPosition = cowboyImage.anchoredPosition;

            upgradeCowboyFinalPositions = new Vector2[upgradeCowboyCards.Length];

            for (int i = 0; i < upgradeCowboyCards.Length; i++)
            {
                upgradeCowboyFinalPositions[i] = upgradeCowboyCards[i].anchoredPosition;
            }

            //NÃO ESTÁ FUNCIONANDO AINDA!!
            if (selectCowboyButton != null)
            {
                buttonFinalPosition = selectCowboyButton.anchoredPosition;
            }

            cachedCowboyPositions = true;
        }
        else if (charCode == 1)
        {
            Canvas.ForceUpdateCanvases();

            samuraiCharacterFinalPosition = samuraiImage.anchoredPosition;

            upgradeSamuraiFinalPositions = new Vector2[upgradeSamuraiCards.Length];

            for (int i = 0; i < upgradeSamuraiCards.Length; i++)
            {
                upgradeSamuraiFinalPositions[i] = upgradeSamuraiCards[i].anchoredPosition;
            }

            //NÃO ESTÁ FUNCIONANDO AINDA!!
            if (selectSamuraiButton != null)
            {
                buttonFinalPosition = selectSamuraiButton.anchoredPosition;
            }

            cachedSamuraiPositions = true;
        }
        else if (charCode == 2)
        {
            Canvas.ForceUpdateCanvases();

            dullahanCharacterFinalPosition = dullahanImage.anchoredPosition;

            upgradeDullahanFinalPositions = new Vector2[upgradeDullahanCards.Length];

            for (int i = 0; i < upgradeDullahanCards.Length; i++)
            {
                upgradeDullahanFinalPositions[i] = upgradeDullahanCards[i].anchoredPosition;
            }

            //NÃO ESTÁ FUNCIONANDO AINDA!!
            if (selectDullahanButton != null)
            {
                buttonFinalPosition = selectDullahanButton.anchoredPosition;
            }

            cachedDullahanPositions = true;
        }


    }

    private void SetStartState(int charCode = -1)
    {
        if (charCode == 0)
        {
            cowboyImage.anchoredPosition = cowboyCharacterFinalPosition + characterStartOffset;

            SetCanvasGroupAlpha(cowboyCanvasGroup, 0f);

            for (int i = 0; i < upgradeCowboyCards.Length; i++)
            {
                //if (upgradeCowboyCards[i] == null)
                //    continue;

                upgradeCowboyCards[i].anchoredPosition = upgradeCowboyFinalPositions[i] + upgradeStartOffset;
                SetCanvasGroupAlpha(upgradeCowboyCanvasGroups[i], 0f);

            }
        }
        else if (charCode == 1)
        {
            samuraiImage.anchoredPosition = samuraiCharacterFinalPosition + characterStartOffset;

            SetCanvasGroupAlpha(samuraiCanvasGroup, 0f);

            for (int i = 0; i < upgradeSamuraiCards.Length; i++)
            {
                upgradeSamuraiCards[i].anchoredPosition = upgradeSamuraiFinalPositions[i] + upgradeStartOffset;
                SetCanvasGroupAlpha(upgradeSamuraiCanvasGroups[i], 0f);

            }
        }
        else if (charCode == 2)
        {
            dullahanImage.anchoredPosition = dullahanCharacterFinalPosition + characterStartOffset;

            SetCanvasGroupAlpha(dullahanCanvasGroup, 0f);

            for (int i = 0; i < upgradeDullahanCards.Length; i++)
            {
                upgradeDullahanCards[i].anchoredPosition = upgradeDullahanFinalPositions[i] + upgradeStartOffset;
                SetCanvasGroupAlpha(upgradeSamuraiCanvasGroups[i], 0f);

            }
        }


        //NÃO ESTÁ FUNCIONANDO AINDA
        if (selectCowboyButton != null)
        {
            selectCowboyButton.anchoredPosition = buttonFinalPosition + buttonStartOffset;
        }

        SetCanvasGroupAlpha(selectCowboyButtonCanvasGroup, 0f);
    }

    //private void SetFinalState()
    //{
    //    return;
    //    cowboyImage.anchoredPosition = cowboyCharacterFinalPosition;
    //    samuraiImage.anchoredPosition = samuraiCharacterFinalPosition;
    //    dullahanImage.anchoredPosition = dullahanCharacterFinalPosition;


    //    SetCanvasGroupAlpha(cowboyCanvasGroup, 1f);

    //    for (int i = 0; i < upgradeCowboyCards.Length; i++)
    //    {
    //        //if (upgradeCowboyCards[i] == null)
    //        //    continue;

    //        upgradeCowboyCards[i].anchoredPosition = upgradeCowboyFinalPositions[i];
    //        SetCanvasGroupAlpha(upgradeCowboyCanvasGroups[i], 1f);

    //        upgradeSamuraiCards[i].anchoredPosition = upgradeSamuraiFinalPositions[i];
    //        SetCanvasGroupAlpha(upgradeSamuraiCanvasGroups[i], 1f);

    //        upgradeDullahanCards[i].anchoredPosition = upgradeDullahanFinalPositions[i];
    //        SetCanvasGroupAlpha(upgradeDullahanCanvasGroups[i], 1f);
    //    }

    //    //NÃO ESTÁ FUNCIONANDO AINDA
    //    if (selectCowboyButton != null)
    //    {
    //        selectCowboyButton.anchoredPosition = buttonFinalPosition;
    //    }

    //    SetCanvasGroupAlpha(selectCowboyButtonCanvasGroup, 1f);
    //}

    private void AnimateCharacter(int charCode = -1)
    {
        if (charCode == 0)
        {
            if (cowboyFirstTime) initialDelay = .2f;
            else initialDelay = 0f;

            LeanTween.move(cowboyImage, ToVector3(cowboyCharacterFinalPosition), characterDuration)
            .setDelay(initialDelay)
            .setEase(LeanTweenType.easeOutCubic)
            .setIgnoreTimeScale(ignoreTimeScale);


            LeanTween.alphaCanvas(cowboyCanvasGroup, 1f, characterDuration)
                .setEase(LeanTweenType.easeOutCubic)
                .setIgnoreTimeScale(ignoreTimeScale);

        }
        else if (charCode == 1)
        {

            if (samuraiFirstTime) initialDelay = .2f;
            else initialDelay = 0f;

            LeanTween.move(samuraiImage, ToVector3(samuraiCharacterFinalPosition), characterDuration)
            .setDelay(initialDelay)
            .setEase(LeanTweenType.easeOutCubic)
            .setIgnoreTimeScale(ignoreTimeScale);


            LeanTween.alphaCanvas(samuraiCanvasGroup, 1f, characterDuration)
                .setEase(LeanTweenType.easeOutCubic)
                .setIgnoreTimeScale(ignoreTimeScale);

            samuraiFirstTime = false;

        }
        else if (charCode == 2)
        {
            if (dullahanFirstTime) initialDelay = .2f;
            else initialDelay = 0f;

                LeanTween.move(dullahanImage, ToVector3(dullahanCharacterFinalPosition), characterDuration)
                .setDelay(initialDelay)
                .setEase(LeanTweenType.easeOutCubic)
                .setIgnoreTimeScale(ignoreTimeScale);


            LeanTween.alphaCanvas(dullahanCanvasGroup, 1f, characterDuration)
                .setEase(LeanTweenType.easeOutCubic)
                .setIgnoreTimeScale(ignoreTimeScale);

        }


    }

    private void AnimateUpgradeCards(int charCode = -1)
    {
        if (charCode == 0)
        {
            for (int i = 0; i < upgradeCowboyCards.Length; i++)
            {
                //if (upgradeCowboyCards[i] == null)
                //    continue;

                float delay = firstUpgradeDelay + initialDelay + i * upgradeStaggerDelay;

                LeanTween.move(upgradeCowboyCards[i], ToVector3(upgradeCowboyFinalPositions[i]), upgradeDuration)
                    .setDelay(delay)
                    .setEase(LeanTweenType.easeOutCubic)
                    .setIgnoreTimeScale(ignoreTimeScale);

                //CanvasGroup cardCanvasGroup = GetUpgradeCanvasGroup(i);
                CanvasGroup cardCanvasGroup = upgradeCowboyCanvasGroups[i];


                LeanTween.alphaCanvas(cardCanvasGroup, 1f, upgradeDuration)
                    .setDelay(delay)
                    .setEase(LeanTweenType.easeOutCubic)
                    .setIgnoreTimeScale(ignoreTimeScale);

            }

            cowboyFirstTime = false;
        }
        else if (charCode == 1)
        {
            for (int i = 0; i < upgradeSamuraiCards.Length; i++)
            {
                //if (upgradeCowboyCards[i] == null)
                //    continue;

                float delay = firstUpgradeDelay + initialDelay + i * upgradeStaggerDelay;

                LeanTween.move(upgradeSamuraiCards[i], ToVector3(upgradeSamuraiFinalPositions[i]), upgradeDuration)
                    .setDelay(delay)
                    .setEase(LeanTweenType.easeOutCubic)
                    .setIgnoreTimeScale(ignoreTimeScale);

                //CanvasGroup cardCanvasGroup = GetUpgradeCanvasGroup(i);
                CanvasGroup cardCanvasGroup = upgradeSamuraiCanvasGroups[i];


                LeanTween.alphaCanvas(cardCanvasGroup, 1f, upgradeDuration)
                    .setDelay(delay)
                    .setEase(LeanTweenType.easeOutCubic)
                    .setIgnoreTimeScale(ignoreTimeScale);

            }

            samuraiFirstTime = false;
        }
        else if (charCode == 2)
        {
            for (int i = 0; i < upgradeDullahanCards.Length; i++)
            {
                //if (upgradeCowboyCards[i] == null)
                //    continue;

                float delay = firstUpgradeDelay + initialDelay + i * upgradeStaggerDelay;

                LeanTween.move(upgradeDullahanCards[i], ToVector3(upgradeDullahanFinalPositions[i]), upgradeDuration)
                    .setDelay(delay)
                    .setEase(LeanTweenType.easeOutCubic)
                    .setIgnoreTimeScale(ignoreTimeScale);

                //CanvasGroup cardCanvasGroup = GetUpgradeCanvasGroup(i);
                CanvasGroup cardCanvasGroup = upgradeDullahanCanvasGroups[i];


                LeanTween.alphaCanvas(cardCanvasGroup, 1f, upgradeDuration)
                    .setDelay(delay)
                    .setEase(LeanTweenType.easeOutCubic)
                    .setIgnoreTimeScale(ignoreTimeScale);

            }

            dullahanFirstTime = false;
        }
    }

    private void AnimateSelectButton() //NÃO ESTÁ FUNCIONANDO AINDA!!
    {
        if (selectCowboyButton == null)
            return;

        LeanTween.move(selectCowboyButton, ToVector3(buttonFinalPosition), buttonDuration)
            .setDelay(buttonDelay)
            .setEase(LeanTweenType.easeOutBack)
            .setIgnoreTimeScale(ignoreTimeScale);

        if (selectCowboyButtonCanvasGroup != null)
        {
            LeanTween.alphaCanvas(selectCowboyButtonCanvasGroup, 1f, buttonDuration)
                .setDelay(buttonDelay)
                .setEase(LeanTweenType.easeOutCubic)
                .setIgnoreTimeScale(ignoreTimeScale);
        }
    }

    private void CancelTweens(int charCode = -1)
    {
        if (charCode == 0)
        {
            LeanTween.cancel(cowboyImage.gameObject);

            LeanTween.cancel(cowboyCanvasGroup.gameObject);

            for (int i = 0; i < upgradeCowboyCards.Length; i++)
            {
                if (upgradeCowboyCards[i] != null)
                {
                    LeanTween.cancel(upgradeCowboyCards[i].gameObject);
                }

                CanvasGroup cardCanvasGroup = upgradeCowboyCanvasGroups[i];

                if (cardCanvasGroup != null)
                {
                    LeanTween.cancel(cardCanvasGroup.gameObject);
                }
            }
        }
        else if (charCode == 1)
        {
            LeanTween.cancel(samuraiImage.gameObject);

            LeanTween.cancel(samuraiCanvasGroup.gameObject);

            for (int i = 0; i < upgradeSamuraiCards.Length; i++)
            {
                if (upgradeSamuraiCards[i] != null)
                {
                    LeanTween.cancel(upgradeSamuraiCards[i].gameObject);
                }

                CanvasGroup cardCanvasGroup = upgradeSamuraiCanvasGroups[i];

                if (cardCanvasGroup != null)
                {
                    LeanTween.cancel(cardCanvasGroup.gameObject);
                }
            }
        }
        else if (charCode == 2)
        {
            LeanTween.cancel(dullahanImage.gameObject);

            LeanTween.cancel(dullahanCanvasGroup.gameObject);

            for (int i = 0; i < upgradeDullahanCards.Length; i++)
            {
                if (upgradeDullahanCards[i] != null)
                {
                    LeanTween.cancel(upgradeDullahanCards[i].gameObject);
                }

                CanvasGroup cardCanvasGroup = upgradeDullahanCanvasGroups[i];

                if (cardCanvasGroup != null)
                {
                    LeanTween.cancel(cardCanvasGroup.gameObject);
                }
            }
        }


        //NÃO ESTÁ FUNCIONANDO

        if (selectCowboyButton != null)
        {
            LeanTween.cancel(selectCowboyButton.gameObject);
        }


        if (selectCowboyButtonCanvasGroup != null)
        {
            LeanTween.cancel(selectCowboyButtonCanvasGroup.gameObject);
        }
    }


    private void SetCanvasGroupAlpha(CanvasGroup canvasGroup, float alpha)
    {
        if (canvasGroup == null)
            return;

        canvasGroup.alpha = alpha;
    }

    private Vector3 ToVector3(Vector2 value)
    {
        return new Vector3(value.x, value.y, 0f);
    }
}
