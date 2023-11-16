using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// �������� �� ����������� ����� � ����
/// </summary>
public class CardInCollectionDisplay : MonoBehaviour
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

    [Header("Card Info")]
    [SerializeField] private CardScriptableObject cardSO;
    private float animationDelay = 0.25f;

    private bool isInfoVisible;


    void Start()
    {
        isInfoVisible = false;
        if (cardSO != null)
        {
            nameText.text = cardSO.Name;
            descriptionText.text = cardSO.Description;

            costText.text = cardSO.CardCost.ToString();
            attackText.text = cardSO.Attack.ToString();
            healthText.text = cardSO.Health.ToString();

            artworkImage.sprite = cardSO.ArtworkHandImage;
            rarityImage.sprite = cardSO.RarityImage;
            universeImage.sprite = cardSO.UniverseImage;
        }
        //button.onClick.AddListener(ChangeInfoBlockVisibility);

    }

    public void InitCard(CardScriptableObject card)
    {
        isInfoVisible = false;
        cardSO = card;
        if (cardSO != null)
        {
            nameText.text = cardSO.Name;
            descriptionText.text = cardSO.Description;

            costText.text = cardSO.CardCost.ToString();
            attackText.text = cardSO.Attack.ToString();
            healthText.text = cardSO.Health.ToString();

            artworkImage.sprite = cardSO.ArtworkHandImage;
            rarityImage.sprite = cardSO.RarityImage;
            universeImage.sprite = cardSO.UniverseImage;
        }
        //button.onClick.AddListener(ChangeInfoBlockVisibility);

    }

}
