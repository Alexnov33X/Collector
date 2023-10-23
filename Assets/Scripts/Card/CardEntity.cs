using UnityEngine;
using static Enums;

/// <summary>
/// Здесь описывается логика поведения карты:
/// - Инициализация данных карты
/// - Изменение TimeCost
/// - Изменения CardState
/// - Уничтожение карты
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
    public void ChangeCardState(GameBoardDisplay gb, bool isPlayer)
    {
        gb.AddEntityToPlayerSide(this, isPlayer);
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

    #region BattleSystem need to Decomposite
    public virtual void BeforeAttack(GameBoardDisplay gb, int position)
    {

    }

    public virtual void Attack(GameBoardDisplay gb, int position)
    {
        if (position < 6) //Player creature
        {
            if (gb.occupants[6 + position % 3] != null)
                gb.occupants[6 + position % 3].OnHit(gb, position, this, cardData.Attack);
            else if (gb.occupants[6 + 3 + position % 3] != null)
                gb.occupants[6 + 3 + position % 3].OnHit(gb, position, this, cardData.Attack);
            else
            attackPlayer(gb, position);
        }
        else //Enemy Creature
        {
            if (gb.occupants[position % 3] != null)
                gb.occupants[position % 3].OnHit(gb, position, this, cardData.Attack);
            else if (gb.occupants[3 + position % 3] != null)
                gb.occupants[3 + position % 3].OnHit(gb, position, this, cardData.Attack);
            else
            attackPlayer(gb, position); 
        }
    }
    public virtual void attackPlayer(GameBoardDisplay gb, int position)
    { //Надо подумать где должен находится слот игрока и через что к нему обращаться
        if (position < 6)
            gb.enemyPlayer.OnHit(gb, position, this, cardData.Attack);
        else
            gb.mainPlayer.OnHit(gb, position, this, cardData.Attack);
    }

    public virtual void AfterAttack(GameBoardDisplay gb, int position)
    {

    }

    public virtual void BeforeHit(GameBoardDisplay gb, int position, CardEntity Attacker, int damage)
    {

    }

    public virtual void OnHit(GameBoardDisplay gb, int position, CardEntity Attacker, int damage)
    {
        cardData.Health -= damage;
    }

    public virtual void AfterHit(GameBoardDisplay gb, int position, CardEntity Attacker, int damage)
    {

    }

    public virtual void OnPlay(GameBoardDisplay gb, int position)
    {

    }

    public virtual void OnDeath(GameBoardDisplay gb, int position)
    {

    }
    #endregion
}
