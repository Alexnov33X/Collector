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
    private float attackDelay = 0.7f;
    public void InitializeCard(CardScriptableObject card)
    {
        cardData = new CardData(card);
        cardData.PrintCardData();
        EventBus.OnEntityCardInitialized?.Invoke();

        handLayer.SetActive(true);
        boardLayer.SetActive(false);
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

    //Анимация уменьшения маны
    //public IEnumerator BounceCost()
    //{
    //    Vector3 startSize = transform.localScale;
    //    Vector3 newSize = new Vector3(startSize.x * 2, startSize.y * 2, startSize.z);

    //    LeanTween.scale(gameObject, newSize, 1 / 2).setDelay(0f);
    //    yield return new WaitForSecondsRealtime(1 / 2);
    //    LeanTween.scale(gameObject, startSize, 1 / 2).setDelay(0f);
    //    yield return new WaitForSecondsRealtime(1 / 2);
    //}

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
    public void Attack(GameBoardRegulator gb, bool isPlayer, int row, int column)
    {
        if (isPlayer)
        {
            if (gb.enemyFirstLine[column].isOccupied)
            {
                StartCoroutine(AttackAnimation(gb.enemyFirstLine[column].occupant.gameObject.transform.position, attackDelay));
                gb.enemyFirstLine[column].occupant.OnHit(gb, !isPlayer, 0, column, cardData.Attack);         
            }
            else if (gb.enemySecondLine[column].isOccupied)
            {
                StartCoroutine(AttackAnimation(gb.enemySecondLine[column].occupant.gameObject.transform.position, attackDelay));
                gb.enemySecondLine[column].occupant.OnHit(gb, !isPlayer, 1, column, cardData.Attack);
          
            }
            else
            {
                StartCoroutine(AttackAnimation(gb.enemyHero.gameObject.transform.position, attackDelay));
                gb.enemyHero.OnHit(cardData.Attack);
               
            }
        }
        else
        {
            if (gb.playerFirstLine[column].isOccupied)
            {
                StartCoroutine(AttackAnimation(gb.playerFirstLine[column].occupant.gameObject.transform.position, attackDelay));
                gb.playerFirstLine[column].occupant.OnHit(gb, !isPlayer, 0, column, cardData.Attack);
                
            }
            else if (gb.playerSecondLine[column].isOccupied)
            {
                StartCoroutine(AttackAnimation(gb.playerSecondLine[column].occupant.gameObject.transform.position, attackDelay));
                gb.playerSecondLine[column].occupant.OnHit(gb, !isPlayer, 1, column, cardData.Attack);
                
            }
            else
            {
                StartCoroutine(AttackAnimation(gb.playerHero.gameObject.transform.position, attackDelay));
                gb.playerHero.OnHit(cardData.Attack);
               
            }
        }
        
    }

    //LeanTween анимация для Атаки позиции
    public IEnumerator AttackAnimation(Vector3 location, float time)
    {
        Vector3 tempLocation = gameObject.transform.position;
        LeanTween.move(gameObject,location, time/3).setEaseInOutBounce();
        yield return new WaitForSecondsRealtime(time / 3);
        LeanTween.move(gameObject, tempLocation, time / 3).setEaseInOutBounce();
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
