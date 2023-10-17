using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardDisplay : MonoBehaviour
{
    [Header("Card Reference")]
    public CardInfo card;

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

    private bool isInfoVisible;

    void Start()
    {
        isInfoVisible = false;

        button.onClick.AddListener(ChangeInfoBlockVisibility);

        InitializeCardView();
    }

    private void InitializeCardView()
    {
        nameText.text = card.Name;
        descriptionText.text = card.Description;

        timeText.text = card.TimeCost.ToString();
        attackText.text = card.Attack.ToString();
        healthText.text = card.Health.ToString();

        artworkImage.sprite = card.Artwork;
        rarityImage.sprite = card.Rarity;
        universeImage.sprite = card.Universe;
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
            PlayableCard pc = GetComponent<PlayableCard>();
            timeText.text = pc.timeCost.ToString();
            attackText.text = pc.attack.ToString();
            healthText.text = pc.health.ToString();
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
