using UnityEngine;

public class UIAnimation : MonoBehaviour
{
    [Header("Personagem")]
    [SerializeField] private RectTransform characterImage;
    [SerializeField] private CanvasGroup characterCanvasGroup;

    [Header("Cards de Upgrade")]
    [SerializeField] private RectTransform[] upgradeCards;
    [SerializeField] private CanvasGroup[] upgradeCanvasGroups;

    [Header("Botão Inferior - Opcional")]
    [SerializeField] private RectTransform selectButton;
    [SerializeField] private CanvasGroup selectButtonCanvasGroup;

    [Header("Offsets de Entrada")]
    [SerializeField] private Vector2 characterStartOffset = new Vector2(-900f, 0f);
    [SerializeField] private Vector2 upgradeStartOffset = new Vector2(900f, 0f);
    [SerializeField] private Vector2 buttonStartOffset = new Vector2(0f, -250f);

    [Header("Duração")]
    [SerializeField] private float characterDuration = 0.45f;
    [SerializeField] private float upgradeDuration = 0.35f;
    [SerializeField] private float buttonDuration = 0.30f;

    [Header("Delays")]
    [SerializeField] private float firstUpgradeDelay = 0.12f;
    [SerializeField] private float upgradeStaggerDelay = 0.10f;
    [SerializeField] private float buttonDelay = 0.45f;

    [Header("Configurações")]
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

    public void PlayEntranceAnimation()
    {
        if (!cachedPositions)
        {
            CacheFinalPositions();
        }

        CancelTweens();
        SetStartState();

        AnimateCharacter();
        AnimateUpgradeCards();
        AnimateSelectButton();
    }

    public void SkipAnimation()
    {
        CancelTweens();
        SetFinalState();
    }

    public void CacheFinalPositions()
    {
        Canvas.ForceUpdateCanvases();

        if (characterImage != null)
        {
            characterFinalPosition = characterImage.anchoredPosition;
        }

        if (upgradeCards != null)
        {
            upgradeFinalPositions = new Vector2[upgradeCards.Length];

            for (int i = 0; i < upgradeCards.Length; i++)
            {
                if (upgradeCards[i] != null)
                {
                    upgradeFinalPositions[i] = upgradeCards[i].anchoredPosition;
                }
            }
        }

        if (selectButton != null)
        {
            buttonFinalPosition = selectButton.anchoredPosition;
        }

        cachedPositions = true;
    }

    private void SetStartState()
    {
        if (characterImage != null)
        {
            characterImage.anchoredPosition = characterFinalPosition + characterStartOffset;
        }

        SetCanvasGroupAlpha(characterCanvasGroup, 0f);

        if (upgradeCards != null)
        {
            for (int i = 0; i < upgradeCards.Length; i++)
            {
                if (upgradeCards[i] == null)
                    continue;

                upgradeCards[i].anchoredPosition = upgradeFinalPositions[i] + upgradeStartOffset;
                SetCanvasGroupAlpha(GetUpgradeCanvasGroup(i), 0f);
            }
        }

        if (selectButton != null)
        {
            selectButton.anchoredPosition = buttonFinalPosition + buttonStartOffset;
        }

        SetCanvasGroupAlpha(selectButtonCanvasGroup, 0f);
    }

    private void SetFinalState()
    {
        if (characterImage != null)
        {
            characterImage.anchoredPosition = characterFinalPosition;
        }

        SetCanvasGroupAlpha(characterCanvasGroup, 1f);

        if (upgradeCards != null)
        {
            for (int i = 0; i < upgradeCards.Length; i++)
            {
                if (upgradeCards[i] == null)
                    continue;

                upgradeCards[i].anchoredPosition = upgradeFinalPositions[i];
                SetCanvasGroupAlpha(GetUpgradeCanvasGroup(i), 1f);
            }
        }

        if (selectButton != null)
        {
            selectButton.anchoredPosition = buttonFinalPosition;
        }

        SetCanvasGroupAlpha(selectButtonCanvasGroup, 1f);
    }

    private void AnimateCharacter()
    {
        if (characterImage == null)
            return;

        LeanTween.move(characterImage, ToVector3(characterFinalPosition), characterDuration)
            .setEase(LeanTweenType.easeOutCubic)
            .setIgnoreTimeScale(ignoreTimeScale);

        if (characterCanvasGroup != null)
        {
            LeanTween.alphaCanvas(characterCanvasGroup, 1f, characterDuration)
                .setEase(LeanTweenType.easeOutCubic)
                .setIgnoreTimeScale(ignoreTimeScale);
        }
    }

    private void AnimateUpgradeCards()
    {
        if (upgradeCards == null)
            return;

        for (int i = 0; i < upgradeCards.Length; i++)
        {
            if (upgradeCards[i] == null)
                continue;

            float delay = firstUpgradeDelay + i * upgradeStaggerDelay;

            LeanTween.move(upgradeCards[i], ToVector3(upgradeFinalPositions[i]), upgradeDuration)
                .setDelay(delay)
                .setEase(LeanTweenType.easeOutCubic)
                .setIgnoreTimeScale(ignoreTimeScale);

            CanvasGroup cardCanvasGroup = GetUpgradeCanvasGroup(i);

            if (cardCanvasGroup != null)
            {
                LeanTween.alphaCanvas(cardCanvasGroup, 1f, upgradeDuration)
                    .setDelay(delay)
                    .setEase(LeanTweenType.easeOutCubic)
                    .setIgnoreTimeScale(ignoreTimeScale);
            }
        }
    }

    private void AnimateSelectButton()
    {
        if (selectButton == null)
            return;

        LeanTween.move(selectButton, ToVector3(buttonFinalPosition), buttonDuration)
            .setDelay(buttonDelay)
            .setEase(LeanTweenType.easeOutBack)
            .setIgnoreTimeScale(ignoreTimeScale);

        if (selectButtonCanvasGroup != null)
        {
            LeanTween.alphaCanvas(selectButtonCanvasGroup, 1f, buttonDuration)
                .setDelay(buttonDelay)
                .setEase(LeanTweenType.easeOutCubic)
                .setIgnoreTimeScale(ignoreTimeScale);
        }
    }

    private void CancelTweens()
    {
        if (characterImage != null)
        {
            LeanTween.cancel(characterImage.gameObject);
        }

        if (characterCanvasGroup != null)
        {
            LeanTween.cancel(characterCanvasGroup.gameObject);
        }

        if (upgradeCards != null)
        {
            for (int i = 0; i < upgradeCards.Length; i++)
            {
                if (upgradeCards[i] != null)
                {
                    LeanTween.cancel(upgradeCards[i].gameObject);
                }

                CanvasGroup cardCanvasGroup = GetUpgradeCanvasGroup(i);

                if (cardCanvasGroup != null)
                {
                    LeanTween.cancel(cardCanvasGroup.gameObject);
                }
            }
        }

        if (selectButton != null)
        {
            LeanTween.cancel(selectButton.gameObject);
        }

        if (selectButtonCanvasGroup != null)
        {
            LeanTween.cancel(selectButtonCanvasGroup.gameObject);
        }
    }

    private CanvasGroup GetUpgradeCanvasGroup(int index)
    {
        if (upgradeCanvasGroups == null)
            return null;

        if (index < 0 || index >= upgradeCanvasGroups.Length)
            return null;

        return upgradeCanvasGroups[index];
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
