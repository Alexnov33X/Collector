using TMPro;
using UnityEngine;

/// <summary>
/// Отвечает за отображение количества оставшихся карт в деке игрока во время битвы.
/// Обновляется по ивенту.
/// </summary>
public class PlayerBattleDeckDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI amountOfCards;

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
        amountOfCards.text = PlayerDecks.CurrentDeck.Count.ToString();
    }
}
