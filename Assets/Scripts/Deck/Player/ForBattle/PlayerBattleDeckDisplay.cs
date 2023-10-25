using TMPro;
using UnityEngine;

/// <summary>
/// �������� �� ����������� ���������� ���������� ���� � ���� ������ �� ����� �����.
/// ����������� �� ������.
/// </summary>
public class PlayerBattleDeckDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI amountOfCards;
    public bool enemyDeck;

    private void OnEnable()
    {
        EventBus.OnPlayerBatttleDeckAmountChanged += UpdateCardsAmountText;
    }

    private void OnDisable()
    {
        EventBus.OnPlayerBatttleDeckAmountChanged -= UpdateCardsAmountText;
    }

    private void Start()
    {
        UpdateCardsAmountText();
    }

    private void UpdateCardsAmountText()
    {
        if (enemyDeck)
            amountOfCards.text = PlayerDecks.CurrentEnemyDeck.Count.ToString();
        else
            amountOfCards.text = PlayerDecks.CurrentDeck.Count.ToString();
    }
}
