using System.Collections;
using System.Collections.Generic;
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
    private const int Y = 240;

    /// <summary>
    /// Слой для отображения в руке
    /// </summary>
    [SerializeField] private GameObject handLayer;
    /// <summary>
    /// Слой для отображения на доске
    /// </summary>
    [SerializeField] private GameObject boardLayer;
    private bool firstStrike; //true if creature did not attack
    private int inActive = 0;

    private GameBoardRegulator gameBoardRegulator; //store and initialize gameBoard here instead of throwing refs around

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
    public IEnumerator Attack(GameBoardRegulator gameBoardRegulator, bool isPlayer, int row, int column)
    {
        if (inActive > 0) // для спячки
        {
            inActive--;
            yield return new WaitForEndOfFrame();
        }
        else
        {
            if (isPlayer)
            {
                if (gameBoardRegulator.enemyFirstLine[column].isOccupied)
                {
                    yield return StartCoroutine(AttackAnimationLocal(gameBoardRegulator.enemyFirstLine[column].occupant.gameObject.transform.localPosition - new Vector3(0, Y, 0), attackDelay));
                    gameBoardRegulator.enemyFirstLine[column].occupant.OnHit(gameBoardRegulator, !isPlayer, 0, column, cardData.Attack);

                    if (cardData.abilities.Contains(CardAbility.DefaultVerticalLinearAttack))
                        gameBoardRegulator.enemySecondLine[column].occupant.OnHit(gameBoardRegulator, !isPlayer, 1, column, cardData.Attack);

                    if (cardData.abilities.Contains(CardAbility.DefaultHorizontalLinearAttack))
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            if (i != column)
                                gameBoardRegulator.enemyFirstLine[i].occupant.OnHit(gameBoardRegulator, !isPlayer, 0, column, cardData.Attack);
                        }
                    }
                }
                else if (gameBoardRegulator.enemySecondLine[column].isOccupied)
                {
                    yield return StartCoroutine(AttackAnimationLocal(gameBoardRegulator.enemySecondLine[column].occupant.gameObject.transform.localPosition - new Vector3(0, Y, 0), attackDelay));
                    gameBoardRegulator.enemySecondLine[column].occupant.OnHit(gameBoardRegulator, !isPlayer, 1, column, cardData.Attack);

                    if (cardData.abilities.Contains(CardAbility.DefaultVerticalLinearAttack))
                        gameBoardRegulator.enemyFirstLine[column].occupant.OnHit(gameBoardRegulator, !isPlayer, 0, column, cardData.Attack);

                    if (cardData.abilities.Contains(CardAbility.DefaultHorizontalLinearAttack))
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            if (i != column)
                                gameBoardRegulator.enemySecondLine[i].occupant.OnHit(gameBoardRegulator, !isPlayer, 1, column, cardData.Attack);
                        }
                    }
                }
                else
                {
                    yield return StartCoroutine(AttackAnimationLocal(transform.localPosition + new Vector3(0, 80 + 200 * row, 0), attackDelay));
                    gameBoardRegulator.enemyHero.OnHit(cardData.Attack);
                }
            }
            else
            {
                if (gameBoardRegulator.playerFirstLine[column].isOccupied)
                {
                    yield return StartCoroutine(AttackAnimationLocal(gameBoardRegulator.playerFirstLine[column].occupant.gameObject.transform.localPosition + new Vector3(0, Y, 0), attackDelay));
                    gameBoardRegulator.playerFirstLine[column].occupant.OnHit(gameBoardRegulator, !isPlayer, 0, column, cardData.Attack);

                    if (cardData.abilities.Contains(CardAbility.DefaultVerticalLinearAttack))
                        gameBoardRegulator.playerSecondLine[column].occupant.OnHit(gameBoardRegulator, !isPlayer, 1, column, cardData.Attack);

                    if (cardData.abilities.Contains(CardAbility.DefaultHorizontalLinearAttack))
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            if (i != column)
                                gameBoardRegulator.playerFirstLine[i].occupant.OnHit(gameBoardRegulator, !isPlayer, 0, column, cardData.Attack);
                        }
                    }

                }
                else if (gameBoardRegulator.playerSecondLine[column].isOccupied)
                {
                    yield return StartCoroutine(AttackAnimationLocal(gameBoardRegulator.playerSecondLine[column].occupant.gameObject.transform.localPosition + new Vector3(0, Y, 0), attackDelay));
                    gameBoardRegulator.playerSecondLine[column].occupant.OnHit(gameBoardRegulator, !isPlayer, 1, column, cardData.Attack);

                    if (cardData.abilities.Contains(CardAbility.DefaultVerticalLinearAttack))
                        gameBoardRegulator.playerFirstLine[column].occupant.OnHit(gameBoardRegulator, !isPlayer, 0, column, cardData.Attack);

                    if (cardData.abilities.Contains(CardAbility.DefaultHorizontalLinearAttack))
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            if (i != column)
                                gameBoardRegulator.playerSecondLine[i].occupant.OnHit(gameBoardRegulator, !isPlayer, 1, column, cardData.Attack);
                        }
                    }
                }
                else
                {
                    yield return StartCoroutine(AttackAnimationLocal(transform.localPosition - new Vector3(0, 80 + 200 * row, 0), attackDelay));
                    gameBoardRegulator.playerHero.OnHit(cardData.Attack);
                }
            }
        }
    }

    //LeanTween анимация для Атаки позиции
    // 238
    public IEnumerator AttackAnimation(Vector3 location, float time)
    {
        Vector3 tempLocation = gameObject.transform.position;
        LeanTween.move(gameObject, location, time / 2).setEaseInBack();
        yield return new WaitForSecondsRealtime(time / 2);
        LeanTween.move(gameObject, tempLocation, time / 2).setEaseInBack();
    }
    public IEnumerator AttackAnimationLocal(Vector3 location, float time)
    {
        Vector3 tempLocation = gameObject.transform.position;
        LeanTween.moveLocal(gameObject, location, time / 2).setEaseInBack();
        yield return new WaitForSecondsRealtime(time / 2);
        LeanTween.move(gameObject, tempLocation, time / 2).setEaseInBack();
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

    private void Start()
    {
        gameBoardRegulator = GameObject.FindAnyObjectByType<GameBoardRegulator>();
    }

}
