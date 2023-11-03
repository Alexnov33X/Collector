using System.Collections;
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
    private float attackDelay = 0.8f;
    public void InitializeCard(CardScriptableObject card, bool isEnemy)
    {
        cardData = new CardData(card);
        cardData.PrintCardData();
        EventBus.OnEntityCardInitialized?.Invoke(isEnemy);

        handLayer.SetActive(true);
        boardLayer.SetActive(false);
        attackDelay = AnimationAndDelays.instance.attackAnimation;
    }

    /// <summary>
    /// Используется, когда карта "призывается" - выходит из руки на доску
    /// </summary>
    public void ChangeCardState()
    {
        cardData.CardState = CardState.OnBoard;

        handLayer.SetActive(false);
        boardLayer.SetActive(true);

        EventBus.OnCardStateChanged?.Invoke();
    }

    /// <summary>
    /// Используется каждый ход, для уменьшения Стоимости карты.
    /// Использовать в дефолтной ситуации.
    /// </summary>
    public void ReduceCardCost()
    {
        if (cardData.CardCost <= 0)
            return;

        //StartCoroutine(BounceCost());
        cardData.CardCost--;
        EventBus.OnCardsInfoChanged?.Invoke();
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

    //row и column - координаты этого существа на поле боя, для просчёта лина и эффектов вероятно нужен будет
    //В этом методе ищем что атаковать и атакуем
    public IEnumerator Attack(GameBoardRegulator gb, bool isPlayer, int row, int column)
    {
        if (isPlayer)
        {
            if (gb.enemyFirstLine[column].isOccupied)
            {
                yield return StartCoroutine(AttackAnimation(gb.enemyFirstLine[column].occupant.gameObject.transform.position, attackDelay));
                gb.enemyFirstLine[column].occupant.OnHit(gb, !isPlayer, 0, column, cardData.Attack);
            }
            else if (gb.enemySecondLine[column].isOccupied)
            {
                yield return StartCoroutine(AttackAnimation(gb.enemySecondLine[column].occupant.gameObject.transform.position, attackDelay));
                gb.enemySecondLine[column].occupant.OnHit(gb, !isPlayer, 1, column, cardData.Attack);
            }
            else
            {
                yield return StartCoroutine(AttackAnimationLocal(transform.position + new Vector3(0, 500 + 200*row, 0), attackDelay));
                gb.enemyHero.OnHit(cardData.Attack);
            }
        }
        else
        {
            if (gb.playerFirstLine[column].isOccupied)
            {
                yield return StartCoroutine(AttackAnimation(gb.playerFirstLine[column].occupant.gameObject.transform.position, attackDelay));
                gb.playerFirstLine[column].occupant.OnHit(gb, !isPlayer, 0, column, cardData.Attack);

            }
            else if (gb.playerSecondLine[column].isOccupied)
            {
                yield return  StartCoroutine(AttackAnimation(gb.playerSecondLine[column].occupant.gameObject.transform.position, attackDelay));
                gb.playerSecondLine[column].occupant.OnHit(gb, !isPlayer, 1, column, cardData.Attack);
            }
            else
            {
                yield return StartCoroutine(AttackAnimationLocal(transform.position - new Vector3(0, 500 + 200 * row, 0), attackDelay));
                gb.playerHero.OnHit(cardData.Attack);
            }
        }
        Debug.Log("OVARDIA");
    }

    //LeanTween анимация для Атаки позиции
    public IEnumerator AttackAnimation(Vector3 location, float time)
    {
        Vector3 tempLocation = gameObject.transform.position;
        LeanTween.move(gameObject, location, time / 2).setEaseInBack();
        yield return new WaitForSecondsRealtime(time / 2);
        LeanTween.move(gameObject, tempLocation, time / 2).setEaseInBack();
        //yield return new WaitForSecondsRealtime(time / 3); //делаем меньше задержку, чтобы после анимации удара убавлялось здоровья а не после всей анимации
    }
    public IEnumerator AttackAnimationLocal(Vector3 location, float time)
    {
        Vector3 tempLocation = gameObject.transform.position;
        LeanTween.moveLocal(gameObject, location, time / 2).setEaseInBack();
        yield return new WaitForSecondsRealtime(time / 2);
        LeanTween.move(gameObject, tempLocation, time / 2).setEaseInBack();
        //yield return new WaitForSecondsRealtime(time / 3); //делаем меньше задержку, чтобы после анимации удара убавлялось здоровья а не после всей анимации
    }

    //метод получения удара. Если умираем то сообщаем об этом полю боя
    public void OnHit(GameBoardRegulator gb, bool isPlayer, int row, int column, int damage)
    {
        cardData.Health -= damage;
        EventBus.OnCardsInfoChanged?.Invoke();
        if (cardData.Health <= 0)
        {
            if (isPlayer)
                gb.playerSide[row, column].DestroyCardinCell();
            else
                gb.enemySide[row, column].DestroyCardinCell();

        }
    }

}
