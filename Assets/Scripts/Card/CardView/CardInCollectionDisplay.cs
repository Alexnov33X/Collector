using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Отвечает за отображение карты в руке
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
    [SerializeField] private GameObject hider;

    [Header("Button")]
    [SerializeField] private Button button;

    [Header("Card Info")]
    [SerializeField] public CardScriptableObject cardSO;

    //private bool isInfoVisible;
    private bool isMouseDown;
    private bool hasStartedHold;
    public bool isSelected = false;

    void Start()
    {
        //isInfoVisible = false;
        infoBlock.SetActive(false);
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
        //isInfoVisible = false;
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
        switchAccess(false);
        //button.onClick.AddListener(ChangeInfoBlockVisibility);

    }

    public void switchAccess(bool cardSelected)
    {
        isSelected = cardSelected;
        if (isSelected)
        {
            hider.SetActive(true);
        }
        else
            hider.SetActive(false);
    }

    private void OnMouseDown()
    {
        isMouseDown = true;
        StartCoroutine(ClickAndHoldCoroutine());
        Debug.Log("MOUSE DOWN");
    }
    private void OnMouseUp()
    {
        Debug.Log(DeckBuilder.instance.ToString());
        if (hasStartedHold)
        {
            hasStartedHold = false;
            //Debug.Log("Событие при удерживании кнопки мыши на объекте");
        }
        else if (!isSelected)
            DeckBuilder.instance.AddCard(this);
        else
            DeckBuilder.instance.RemoveCard(this);

        isMouseDown = false;
    }

    private IEnumerator ClickAndHoldCoroutine()
    {
        float timer = 0f;

        while (isMouseDown)
        {
            timer += Time.deltaTime;

            if (timer >= 0.5f && !hasStartedHold)
            {
                hasStartedHold = true;
                //Debug.Log("Событие при клике на объекте и удерживании как минимум полсекунды");
                infoBlock.SetActive(!infoBlock.activeSelf);
            }

            yield return null;
        }
    }



}
