using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// �������� �� ����������� ����� �� ����� � ������. �� ������ ������ ��� �������������� �������, 
/// ������� ����� ������� ��� �����, ������ ��� ������ ����� ������ ��� ����
/// </summary>
public class CardOnBoardDisplay : MonoBehaviour
{
    [Header("Parametrs Text")]
    [SerializeField] private TextMeshProUGUI attackText;
    [SerializeField] private TextMeshProUGUI healthText;

    [Header("Artwork")]
    [SerializeField] private Image artworkImage;

    [Header("Button")]
    [SerializeField] private Button button;

    [Header("Entity")]
    [SerializeField] private CardEntity cardEntity;

    private bool isInfoVisible;

    private void OnEnable()
    {
        EventBus.OnCardsInfoChanged += UpdateInformation;
        EventBus.OnCardStateChanged += InitializeCardView;
    }

    private void OnDisable()
    {
        EventBus.OnCardsInfoChanged -= UpdateInformation;
        EventBus.OnCardStateChanged -= InitializeCardView;
    }

    void Start()
    {
        isInfoVisible = false;

        button.onClick.AddListener(ChangeInfoBlockVisibility);
    }

    private void InitializeCardView()
    {
        attackText.text = cardEntity.cardData.Attack.ToString();
        healthText.text = cardEntity.cardData.Health.ToString();

        artworkImage.sprite = cardEntity.cardData.ArtworkBoardImage;
    }

    /// <summary>
    /// ������������ ��� �����������\������� ����� � ����������� ����� ��� ������� �� ���.
    /// </summary>
    private void ChangeInfoBlockVisibility()
    {
        isInfoVisible = !isInfoVisible;

    }

    public void UpdateInformation()
    {
        attackText.text = cardEntity.cardData.Attack.ToString();
        if (healthText.text != cardEntity.cardData.Health.ToString())
            healthText.color = Color.HSVToRGB(35, 100, 100);
        healthText.text = cardEntity.cardData.Health.ToString();
    }

    public void BURN(bool on)
    {
        if (on)
        {
            healthText.color = Color.red;
            attackText.color = Color.red;
        }
        else
        {
            healthText.color = Color.HSVToRGB(35, 100, 100);
            attackText.color = Color.white;
        }
    }

    public void Sleep(bool on)
    {
        if (on)
        {
            healthText.color = Color.black;
            attackText.color = Color.black;
        }
        else
        {
            if (healthText.color == Color.black)
            healthText.color = Color.HSVToRGB(35, 100, 100);
            attackText.color = Color.white;
        }
    }
}
