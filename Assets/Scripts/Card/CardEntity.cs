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
    private bool firstStrike = true; //true if creature did not attack
    private bool isEnemyEntity = false; // TO DO, make creature understand which side it's on
    private Vector2Int boardPosition = new Vector2Int();

    private GameBoardRegulator gameBoardRegulator; //store and initialize gameBoard here instead of throwing refs around
    private CardOnBoardDisplay displayController;
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
        isEnemyEntity = isEnemy;
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
        displayController = GetComponentInChildren<CardOnBoardDisplay>();
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
        if (cardData.abilities.Contains(CardAbility.Sleep) && cardData.abilityPotency[cardData.abilities.FindIndex(x => x == CardAbility.Sleep)] > 0) // для спячки
        {
            cardData.abilityPotency[cardData.abilities.FindIndex(x => x == CardAbility.Sleep)]--;
            displayController.Sleep(true);
            yield return new WaitForEndOfFrame();
        }
        else
        {
            displayController.Sleep(false);
            if (isPlayer)
            {
                if (gameBoardRegulator.enemyFirstLine[column].isOccupied)
                {
                    //yield return StartCoroutine(AttackAnimationLocal(gameBoardRegulator.enemyFirstLine[column].occupant.gameObject.transform.localPosition - new Vector3(0, Y, 0), attackDelay));
                    //gameBoardRegulator.enemyFirstLine[column].occupant.OnHit(!isPlayer, 0, column, cardData.Attack);

                    if (cardData.abilities.Contains(CardAbility.DefaultVerticalLinearAttack) && gameBoardRegulator.enemySecondLine[column].isOccupied) //VERTICAL additional attack
                    {
                        Debug.Log("Vertical");
                        ApplyIgnite(gameBoardRegulator, 1, column, false);
                        yield return StartCoroutine(AttackAnimationLocal(gameBoardRegulator.enemySecondLine[column].occupant.gameObject.transform.localPosition, attackDelay));
                        yield return new WaitForSeconds(attackDelay);
                        gameBoardRegulator.enemySecondLine[column].occupant.OnHit(!isPlayer, 1, column, cardData.Attack);
                    }

                    else if (cardData.abilities.Contains(CardAbility.DefaultHorizontalLinearAttack)) //HORIZONTAL addtional attack
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            if (gameBoardRegulator.enemyFirstLine[i].isOccupied)
                            {
                                Debug.Log("Horizontal");
                                ApplyIgnite(gameBoardRegulator, 0, i, false);
                                yield return StartCoroutine(AttackAnimationLocal(gameBoardRegulator.enemyFirstLine[i].occupant.gameObject.transform.localPosition, attackDelay));
                                yield return new WaitForSeconds(attackDelay);
                                gameBoardRegulator.enemyFirstLine[i].occupant.OnHit(!isPlayer, 0, column, cardData.Attack);
                            }
                        }
                    }

                    else //DEFAULT attack
                    {
                        yield return StartCoroutine(AttackAnimationLocal(gameBoardRegulator.enemyFirstLine[column].occupant.gameObject.transform.localPosition - new Vector3(0, Y, 0), attackDelay));
                        ApplyIgnite(gameBoardRegulator, 0, column, false);
                        gameBoardRegulator.enemyFirstLine[column].occupant.OnHit(!isPlayer, 0, column, cardData.Attack);
                    }

                    firstStrike = false; //If we damage creature, then first strike should proc. It should not proc on enemyHero
                }
                else if (gameBoardRegulator.enemySecondLine[column].isOccupied)
                {
                    //yield return StartCoroutine(AttackAnimationLocal(gameBoardRegulator.enemySecondLine[column].occupant.gameObject.transform.localPosition - new Vector3(0, Y, 0), attackDelay));
                    //gameBoardRegulator.enemySecondLine[column].occupant.OnHit(!isPlayer, 1, column, cardData.Attack);

                    if (cardData.abilities.Contains(CardAbility.DefaultVerticalLinearAttack) && gameBoardRegulator.enemyFirstLine[column].isOccupied)
                    {
                        Debug.Log("Vertical");
                        ApplyIgnite(gameBoardRegulator, 0, column, false);
                        yield return StartCoroutine(AttackAnimationLocal(gameBoardRegulator.enemyFirstLine[column].occupant.gameObject.transform.localPosition, attackDelay));
                        gameBoardRegulator.enemyFirstLine[column].occupant.OnHit(!isPlayer, 0, column, cardData.Attack);
                        yield return new WaitForSeconds(attackDelay);
                    }

                    else if (cardData.abilities.Contains(CardAbility.DefaultHorizontalLinearAttack))
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            if (gameBoardRegulator.enemySecondLine[i].isOccupied)
                            {
                                Debug.Log("Horizontal");
                                ApplyIgnite(gameBoardRegulator, 1, i, false);
                                yield return StartCoroutine(AttackAnimationLocal(gameBoardRegulator.enemySecondLine[i].occupant.gameObject.transform.localPosition, attackDelay));
                                gameBoardRegulator.enemySecondLine[i].occupant.OnHit(!isPlayer, 1, column, cardData.Attack);
                                yield return new WaitForSeconds(attackDelay);
                            }
                        }
                    }
                    else
                    {
                        ApplyIgnite(gameBoardRegulator, 1, column, false);
                        yield return StartCoroutine(AttackAnimationLocal(gameBoardRegulator.enemySecondLine[column].occupant.gameObject.transform.localPosition - new Vector3(0, Y, 0), attackDelay));
                        gameBoardRegulator.enemySecondLine[column].occupant.OnHit(!isPlayer, 1, column, cardData.Attack);
                    }

                    firstStrike = false;
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

                    if (cardData.abilities.Contains(CardAbility.DefaultVerticalLinearAttack) && gameBoardRegulator.playerSecondLine[column].isOccupied)
                    {
                        Debug.Log("Vertical");
                        ApplyIgnite(gameBoardRegulator, 1, column, true);
                        gameBoardRegulator.playerSecondLine[column].occupant.OnHit(!isPlayer, 1, column, cardData.Attack);
                        yield return new WaitForSeconds(attackDelay);
                    }

                    if (cardData.abilities.Contains(CardAbility.DefaultHorizontalLinearAttack))
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            if (gameBoardRegulator.playerFirstLine[i].isOccupied)
                            {
                                Debug.Log("Horizontal");
                                ApplyIgnite(gameBoardRegulator, 0, i, true);
                                gameBoardRegulator.playerFirstLine[i].occupant.OnHit(!isPlayer, 0, column, cardData.Attack);
                                yield return new WaitForSeconds(attackDelay);
                            }
                        }
                    }
                    else
                    {
                        ApplyIgnite(gameBoardRegulator, 0, column, true);
                        gameBoardRegulator.playerFirstLine[column].occupant.OnHit(!isPlayer, 0, column, cardData.Attack);
                    }

                    firstStrike = false;

                }
                else if (gameBoardRegulator.playerSecondLine[column].isOccupied)
                {
                    yield return StartCoroutine(AttackAnimationLocal(gameBoardRegulator.playerSecondLine[column].occupant.gameObject.transform.localPosition + new Vector3(0, Y, 0), attackDelay));


                    if (cardData.abilities.Contains(CardAbility.DefaultVerticalLinearAttack) && gameBoardRegulator.playerFirstLine[column].isOccupied)
                    {
                        Debug.Log("Vertical");
                        ApplyIgnite(gameBoardRegulator, 0, column, true);
                        gameBoardRegulator.playerFirstLine[column].occupant.OnHit(!isPlayer, 0, column, cardData.Attack);
                        yield return new WaitForSeconds(attackDelay);
                    }

                    else if (cardData.abilities.Contains(CardAbility.DefaultHorizontalLinearAttack))
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            if (gameBoardRegulator.playerSecondLine[i].isOccupied)
                            {
                                Debug.Log("Horizontal");
                                ApplyIgnite(gameBoardRegulator, 1, i, true);
                                gameBoardRegulator.playerSecondLine[i].occupant.OnHit(!isPlayer, 1, column, cardData.Attack);
                                yield return new WaitForSeconds(attackDelay);
                            }
                        }
                    }
                    else
                    {
                        ApplyIgnite(gameBoardRegulator, 1, column, true);
                        gameBoardRegulator.playerSecondLine[column].occupant.OnHit(!isPlayer, 1, column, cardData.Attack);
                    }

                    firstStrike = false;
                }
                else
                {
                    yield return StartCoroutine(AttackAnimationLocal(transform.localPosition - new Vector3(0, 80 + 200 * row, 0), attackDelay));
                    gameBoardRegulator.playerHero.OnHit(cardData.Attack);
                }
            }
        }

    }

    private void ApplyIgnite(GameBoardRegulator gameBoardRegulator, int targetRow, int targetColumn, bool isPlayer)
    {
        if (cardData.abilities.Contains(CardAbility.IgniteCreature) && firstStrike && isPlayer)
        {
            gameBoardRegulator.playerSide[targetRow, targetColumn].occupant.ReceiveAbility(CardAbility.Ignited, cardData.abilities.FindIndex(x => x == CardAbility.IgniteCreature));
            firstStrike = false;
        }
        else if (cardData.abilities.Contains(CardAbility.IgniteCreature) && firstStrike)
        {
            gameBoardRegulator.enemySide[targetRow, targetColumn].occupant.ReceiveAbility(CardAbility.Ignited, cardData.abilities.FindIndex(x => x == CardAbility.IgniteCreature));
            firstStrike = false;
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
    public void OnHit(bool isPlayer, int row, int column, int damage)
    {
        cardData.Health -= damage;
        EventBus.OnCardsInfoChanged?.Invoke();
        if (cardData.Health <= 0)
        {
            if (isPlayer)
                gameBoardRegulator.playerSide[row, column].DestroyCardinCell();
            else
                gameBoardRegulator.enemySide[row, column].DestroyCardinCell();

        }
    }

    public void TurnEnd(bool isPlayer, int row, int column)
    {
        if (cardData.abilities.Contains(CardAbility.Ignited))
        {
            cardData.Health--;
            cardData.abilityPotency[cardData.abilities.FindIndex(x => x == CardAbility.Ignited)]--;
            if (cardData.abilityPotency[cardData.abilities.FindIndex(x => x == CardAbility.Ignited)] == 0)
                RemoveAbility(CardAbility.Ignited);
            EventBus.OnCardsInfoChanged?.Invoke();
            if (cardData.Health <= 0)
            {
                if (isPlayer)
                    gameBoardRegulator.playerSide[row, column].DestroyCardinCell();
                else
                    gameBoardRegulator.enemySide[row, column].DestroyCardinCell();
            }
        }

    }

    public void ReceiveAbility(CardAbility cardAbility, int potency) // receive ability
    {
        cardData.abilities.Add(cardAbility);
        cardData.abilityPotency.Add(potency);
        if (cardAbility == CardAbility.Ignited)
            displayController.BURN(true);

    }

    public void RemoveAbility(CardAbility cardAbility)
    {
        cardData.abilityPotency.RemoveAt(cardData.abilities.FindIndex(x => x == cardAbility)); //remove ability
        cardData.abilities.Remove(cardAbility);
        if (cardAbility == CardAbility.Ignited)
            displayController.BURN(false);
    }

    public void changeAbilityPotency(CardAbility cardAbility, int value)
    {
        int index = cardData.abilities.FindIndex(x => x == cardAbility);
        cardData.abilityPotency[index] += value;
        if (cardData.abilityPotency[index] <= 0)
        {
            cardData.abilities.Remove(cardAbility);
            cardData.abilityPotency.RemoveAt(index);
        }
    }

    private void Start()
    {
        gameBoardRegulator = GameObject.FindAnyObjectByType<GameBoardRegulator>();
    }

}
