using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Отвечает за отображение карты в руке
/// </summary>
public class CardOnHandDisplay : MonoBehaviour
{
    [Header("Name and Description")]
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI descriptionText;

    [Header("Parametrs Text")]
    [SerializeField] private TextMeshProUGUI costText;
    [SerializeField] private TextMeshProUGUI attackText;
    [SerializeField] private TextMeshProUGUI healthText;

    [Header("Artwork")]
    [SerializeField] private Image artworkImage;

    [Header("Parametrs Images")]
    [SerializeField] private Image rarityImage;
    [SerializeField] private Image universeImage;

    [Header("Info Block")]
    [SerializeField] private GameObject infoBlock;

    [Header("Button")]
    [SerializeField] private Button button;

    [Header("Entity")]
    [SerializeField] private CardEntity cardEntity;
    private float animationDelay = 0.25f;

    private bool isInfoVisible;

    private void OnEnable()
    {
        EventBus.OnCardsInfoChanged += UpdateInformation;
        EventBus.OnEntityCardInitialized += InitializeCardView;
    }

    private void OnDisable()
    {
        EventBus.OnCardsInfoChanged -= UpdateInformation;
        EventBus.OnEntityCardInitialized -= InitializeCardView;
    }

    void Start()
    {
        isInfoVisible = false;

        button.onClick.AddListener(ChangeInfoBlockVisibility);
        animationDelay = AnimationAndDelays.instance.cardCostChangeAnimation;
    }

    private void InitializeCardView()
    {
        nameText.text = cardEntity.cardData.Name;
        descriptionText.text = cardEntity.cardData.Description;

        costText.text = cardEntity.cardData.CardCost.ToString();
        attackText.text = cardEntity.cardData.Attack.ToString();
        healthText.text = cardEntity.cardData.Health.ToString();

        artworkImage.sprite = cardEntity.cardData.ArtworkHandImage;
        rarityImage.sprite = cardEntity.cardData.RarityImage;
        universeImage.sprite = cardEntity.cardData.UniverseImage;
    }

    /// <summary>
    /// Используется для отображения\скрытия блока с информацией карты при нажатии на нее.
    /// </summary>
    private void ChangeInfoBlockVisibility()
    {
        isInfoVisible = !isInfoVisible;

        infoBlock.SetActive(isInfoVisible);
    }

    /// <summary>
    /// Используется для обновления информации на карте в руке\на доске
    /// </summary>
    public void UpdateInformation()
    {
        if (costText.text != cardEntity.cardData.CardCost.ToString()) //из-за того что событие вызывается часто, то запускаем корутину только когда меняется значение
            StartCoroutine(BounceCost());
        costText.text = cardEntity.cardData.CardCost.ToString();
        attackText.text = cardEntity.cardData.Attack.ToString();
        healthText.text = cardEntity.cardData.Health.ToString();
    }
    /// <summary>
    /// Анимация изменения стоимости карты
    /// </summary>
    public IEnumerator BounceCost()
    {
        Vector3 startSize = transform.localScale;
        Vector3 newSize = new Vector3(startSize.x * 2, startSize.y * 2, startSize.z);
        LeanTween.scale(costText.gameObject, newSize, animationDelay / 2).setDelay(0f);
        yield return new WaitForSecondsRealtime(animationDelay / 2);
        LeanTween.scale(costText.gameObject, startSize, animationDelay / 2).setDelay(0f);
        yield return new WaitForSecondsRealtime(animationDelay / 2);
    }
}
