using UnityEngine;

/// <summary>
/// Здесь будет описываться логика попадания в руку, выхода карты на доску и уничтожения.
/// </summary>
public class CardBoardBehaviour : MonoBehaviour
{
    private CardEntity creature;

    private void Start()
    {
        creature = GetComponent<CardEntity>();
    }

    public void ChangeTimeCost(int change)
    {
        creature.cardData.TimeCost += change;
        EventBus.OnCardsInfoChanged?.Invoke();
    }
}
