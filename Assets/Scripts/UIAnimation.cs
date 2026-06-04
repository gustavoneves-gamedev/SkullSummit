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

    private Vector2 characterFinalPosition;
    private Vector2[] upgradeFinalPositions;
    private Vector2 buttonFinalPosition;

    private bool cachedPositions;

    private void Awake()
    {
        CacheFinalPositions();
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

        if (cachedPositions)
        {
            SetFinalState();
        }
    }

    public void PlayEntranceAnimation(int charCode = -1)
    {
        if (!cachedPositions)
        {
            CacheFinalPositions();
        }

        CancelTweens();
        SetStartState();

        AnimateCharacter(charCode);
        AnimateUpgradeCards(charCode);
        AnimateSelectButton(); //NÃO ESTÁ FUNCIONANDO AINDA!!
    }

    public void SkipAnimation()
    {
        CancelTweens();
        SetFinalState();
    }

    public void CacheFinalPositions()
    {
        Canvas.ForceUpdateCanvases();

        if (cowboyImage != null)
        {
            characterFinalPosition = cowboyImage.anchoredPosition;
        }

        if (upgradeCowboyCards != null)
        {
            upgradeFinalPositions = new Vector2[upgradeCowboyCards.Length];

            for (int i = 0; i < upgradeCowboyCards.Length; i++)
            {
                if (upgradeCowboyCards[i] != null)
                {
                    upgradeFinalPositions[i] = upgradeCowboyCards[i].anchoredPosition;
                }
            }
        }

        if (selectCowboyButton != null)
        {
            buttonFinalPosition = selectCowboyButton.anchoredPosition;
        }

        cachedPositions = true;
    }

    private void SetStartState()
    {
        if (cowboyImage != null)
        {
            cowboyImage.anchoredPosition = characterFinalPosition + characterStartOffset;
        }

        SetCanvasGroupAlpha(cowboyCanvasGroup, 0f);

        if (upgradeCowboyCards != null)
        {
            for (int i = 0; i < upgradeCowboyCards.Length; i++)
            {
                if (upgradeCowboyCards[i] == null)
                    continue;

                upgradeCowboyCards[i].anchoredPosition = upgradeFinalPositions[i] + upgradeStartOffset;
                SetCanvasGroupAlpha(GetUpgradeCanvasGroup(i), 0f);
            }
        }

        if (selectCowboyButton != null)
        {
            selectCowboyButton.anchoredPosition = buttonFinalPosition + buttonStartOffset;
        }

        SetCanvasGroupAlpha(selectCowboyButtonCanvasGroup, 0f);
    }

    private void SetFinalState()
    {
        if (cowboyImage != null)
        {
            cowboyImage.anchoredPosition = characterFinalPosition;
        }

        SetCanvasGroupAlpha(cowboyCanvasGroup, 1f);

        if (upgradeCowboyCards != null)
        {
            for (int i = 0; i < upgradeCowboyCards.Length; i++)
            {
                if (upgradeCowboyCards[i] == null)
                    continue;

                upgradeCowboyCards[i].anchoredPosition = upgradeFinalPositions[i];
                SetCanvasGroupAlpha(GetUpgradeCanvasGroup(i), 1f);
            }
        }

        if (selectCowboyButton != null)
        {
            selectCowboyButton.anchoredPosition = buttonFinalPosition;
        }

        SetCanvasGroupAlpha(selectCowboyButtonCanvasGroup, 1f);
    }

    private void AnimateCharacter(int charCode = -1)
    {
        if (charCode == 0)
        {
            LeanTween.move(cowboyImage, ToVector3(characterFinalPosition), characterDuration)
            .setEase(LeanTweenType.easeOutCubic)
            .setIgnoreTimeScale(ignoreTimeScale);


            LeanTween.alphaCanvas(cowboyCanvasGroup, 1f, characterDuration)
                .setEase(LeanTweenType.easeOutCubic)
                .setIgnoreTimeScale(ignoreTimeScale);

        }
        else if (charCode == 1)
        {
            LeanTween.move(samuraiImage, ToVector3(characterFinalPosition), characterDuration)
            .setEase(LeanTweenType.easeOutCubic)
            .setIgnoreTimeScale(ignoreTimeScale);


            LeanTween.alphaCanvas(samuraiCanvasGroup, 1f, characterDuration)
                .setEase(LeanTweenType.easeOutCubic)
                .setIgnoreTimeScale(ignoreTimeScale);

        }
        else if (charCode == 2)
        {
            LeanTween.move(dullahanImage, ToVector3(characterFinalPosition), characterDuration)
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

                float delay = firstUpgradeDelay + i * upgradeStaggerDelay;

                LeanTween.move(upgradeCowboyCards[i], ToVector3(upgradeFinalPositions[i]), upgradeDuration)
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
        }
        else if (charCode == 1)
        {
            for (int i = 0; i < upgradeSamuraiCards.Length; i++)
            {
                //if (upgradeCowboyCards[i] == null)
                //    continue;

                float delay = firstUpgradeDelay + i * upgradeStaggerDelay;

                LeanTween.move(upgradeSamuraiCards[i], ToVector3(upgradeFinalPositions[i]), upgradeDuration)
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
        }
        else if (charCode == 2)
        {
            for (int i = 0; i < upgradeDullahanCards.Length; i++)
            {
                //if (upgradeCowboyCards[i] == null)
                //    continue;

                float delay = firstUpgradeDelay + i * upgradeStaggerDelay;

                LeanTween.move(upgradeDullahanCards[i], ToVector3(upgradeFinalPositions[i]), upgradeDuration)
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

    private void CancelTweens()
    {
        if (cowboyImage != null)
        {
            LeanTween.cancel(cowboyImage.gameObject);
        }

        if (cowboyCanvasGroup != null)
        {
            LeanTween.cancel(cowboyCanvasGroup.gameObject);
        }

        if (upgradeCowboyCards != null)
        {
            for (int i = 0; i < upgradeCowboyCards.Length; i++)
            {
                if (upgradeCowboyCards[i] != null)
                {
                    LeanTween.cancel(upgradeCowboyCards[i].gameObject);
                }

                CanvasGroup cardCanvasGroup = GetUpgradeCanvasGroup(i);

                if (cardCanvasGroup != null)
                {
                    LeanTween.cancel(cardCanvasGroup.gameObject);
                }
            }
        }

        if (selectCowboyButton != null)
        {
            LeanTween.cancel(selectCowboyButton.gameObject);
        }

        if (selectCowboyButtonCanvasGroup != null)
        {
            LeanTween.cancel(selectCowboyButtonCanvasGroup.gameObject);
        }
    }

    private CanvasGroup GetUpgradeCanvasGroup(int index)
    {
        if (upgradeCowboyCanvasGroups == null)
            return null;

        if (index < 0 || index >= upgradeCowboyCanvasGroups.Length)
            return null;

        return upgradeCowboyCanvasGroups[index];
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
