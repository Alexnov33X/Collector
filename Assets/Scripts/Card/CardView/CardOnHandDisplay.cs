using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardOnHandDisplay : MonoBehaviour
{
    [Header("Name and Description")]
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI descriptionText;

    [Header("Parametrs Text")]
    [SerializeField] private TextMeshProUGUI timeText;
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
    }

    private void InitializeCardView()
    {
        nameText.text = cardEntity.cardData.Name;
        descriptionText.text = cardEntity.cardData.Description;

        timeText.text = cardEntity.cardData.TimeCost.ToString();
        attackText.text = cardEntity.cardData.Attack.ToString();
        healthText.text = cardEntity.cardData.Health.ToString();

        artworkImage.sprite = cardEntity.cardData.ArtworkImage;
        rarityImage.sprite = cardEntity.cardData.RarityImage;
        universeImage.sprite = cardEntity.cardData.UniverseImage;
    }

    private void ChangeInfoBlockVisibility()
    {
        isInfoVisible = !isInfoVisible;

        infoBlock.SetActive(isInfoVisible);
    }

    public void UpdateInformation()
    {
        timeText.text = cardEntity.cardData.TimeCost.ToString();
        attackText.text = cardEntity.cardData.Attack.ToString();
        healthText.text = cardEntity.cardData.Health.ToString();
    }
}
