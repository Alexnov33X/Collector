using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardDisplay : MonoBehaviour
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
    [SerializeField] public Creature creature;

    private bool isInfoVisible;

    private void OnEnable()
    {
        EventBus.OnCardsInfoChanged += UpdateInformation;
    }

    private void OnDisable()
    {
        EventBus.OnCardsInfoChanged -= UpdateInformation;
    }

    void Start()
    {
        isInfoVisible = false;

        button.onClick.AddListener(ChangeInfoBlockVisibility);

        InitializeCardView();
    }

    private void InitializeCardView()
    {
        nameText.text = creature.cardData.Name;
        descriptionText.text = creature.cardData.Description;

        timeText.text = creature.cardData.TimeCost.ToString();
        attackText.text = creature.cardData.Attack.ToString();
        healthText.text = creature.cardData.Health.ToString();

        artworkImage.sprite = creature.cardData.ArtworkImage;
        rarityImage.sprite = creature.cardData.RarityImage;
        universeImage.sprite = creature.cardData.UniverseImage;
    }

    private void ChangeInfoBlockVisibility()
    {
        isInfoVisible = !isInfoVisible;

        infoBlock.SetActive(isInfoVisible);
    }

    public void UpdateInformation()
    {
        if (gameObject != null)
        {
            timeText.text = creature.cardData.TimeCost.ToString();
            attackText.text = creature.cardData.Attack.ToString();
            healthText.text = creature.cardData.Health.ToString();
        }
    }

    public void HideInformation()
    {
        nameText.gameObject.SetActive(false);
        descriptionText.gameObject.SetActive(false);
        attackText.gameObject.SetActive(false);
        healthText.gameObject.SetActive(false);
        UpdateInformation();
    }
}
