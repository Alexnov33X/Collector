using UnityEngine;
using static Enums;

/// <summary>
/// Здесь описывается логика поведения карты:
/// - Инициализация данных карты
/// - Изменение TimeCost
/// - Изменения CardState
/// ! Логика боевки должна быть отдельно, не здесь.
/// </summary>
public class CardEntity : MonoBehaviour
{
    /// <summary>
    /// Слой для отображения в руке
    /// </summary>
    [SerializeField] private GameObject handLayer;
    /// <summary>
    /// Слой для отображения на доске
    /// </summary>
    [SerializeField] private GameObject boardLayer;

    /// <summary>
    /// Экземпляр класса, который будет хранить всю информацию о 
    /// </summary>
    [HideInInspector] public CardData cardData;

    public void InitializeCard(CardScriptableObject card)
    {
        cardData = new CardData(card);
        cardData.PrintCardData();
        EventBus.OnEntityCardInitialized?.Invoke();

        handLayer.SetActive(true);
    }

    /// <summary>
    /// Используется, когда карта "призывается" - выходит из руки на доску
    /// </summary>
    public void ChangeCardState()
    {
        cardData.CardState = CardState.OnBoard;

        handLayer.SetActive(false);
        boardLayer.SetActive(true);
    }

    /// <summary>
    /// Используется каждый ход, для уменьшения Стоимости карты.
    /// Использовать в дефолтной ситуации.
    /// </summary>
    public void ReduceCardCost()
    {
        if (cardData.CardCost <= 0)
            return;

        cardData.CardCost--;
    }

    /// <summary>
    /// Перегрузка метода для уменьшения стоимости.
    /// Использовать при необходимости увеличить стоимость на N значение
    /// </summary>
    /// <param name="change">Значение на которое менятеся CardCost</param>
    public void ReduceCardCost(int change)
    {
        cardData.CardCost += change;
    }
}
