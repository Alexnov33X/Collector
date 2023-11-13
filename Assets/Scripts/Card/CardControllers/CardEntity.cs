using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
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
    protected bool firstStrike = true; //true if creature did not attack
    public bool isEnemyEntity = false; // TO DO, make creature understand which side it's on
    private Vector2Int boardPosition = new Vector2Int();
    public BoardCell cellHost;
    public Dictionary<CardAbility, int> abilitiesAndStatus = new Dictionary<CardAbility, int>();

    protected GameBoardRegulator gameBoardRegulator; //store and initialize gameBoard here instead of throwing refs around
    protected CardOnBoardDisplay displayController;
    /// <summary>
    /// Экземпляр класса, который будет хранить всю информацию о 
    /// </summary>
    [HideInInspector] public CardData cardData;
    private float attackDelay = 0.8f;

    private void Start()
    {
        gameBoardRegulator = GameObject.FindAnyObjectByType<GameBoardRegulator>();
        //Debug.Log(GetComponentInChildren<CardOnHandDisplay>() == null);
        //handLayer = GetComponentInChildren<CardOnHandDisplay>().gameObject;
        //boardLayer = GetComponentInChildren<CardOnBoardDisplay>().gameObject;
    }
    public void InitializeCard(CardScriptableObject card, bool isEnemy)
    {
        handLayer = GetComponentInChildren<CardOnHandDisplay>().gameObject;
        boardLayer = GetComponentInChildren<CardOnBoardDisplay>().gameObject;
        cardData = new CardData(card);
        //cardData.PrintCardData();
        EventBus.OnEntityCardInitialized?.Invoke(isEnemy);
        abilitiesAndStatus = cardData.abilityAndStatus;
        if (handLayer == null)
            handLayer = GetComponentInChildren<CardOnHandDisplay>().gameObject;
        if (boardLayer == null)
            boardLayer = GetComponentInChildren<CardOnBoardDisplay>().gameObject;
        handLayer.SetActive(true);
        boardLayer.SetActive(false);
        attackDelay = AnimationAndDelays.instance.attackAnimation;
        isEnemyEntity = isEnemy;
        displayController = GetComponentInChildren<CardOnBoardDisplay>();
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
    public virtual IEnumerator Attack(GameBoardRegulator gameBoardRegulator, bool isPlayer, int row, int column)
    {
        if (cardData.abilities.Contains(CardAbility.Sleep) && cardData.abilityPotency[cardData.abilities.FindIndex(x => x == CardAbility.Sleep)] > 0) // для спячки
        {
            cardData.abilityPotency[cardData.abilities.FindIndex(x => x == CardAbility.Sleep)]--;
            displayController.Sleep(true);
            yield return new WaitForEndOfFrame();
        }
        else if (cardData.Attack < 1)
            yield return new WaitForEndOfFrame();
        else
        {
            displayController.Sleep(false);
            if (isPlayer)
            {
                if (gameBoardRegulator.enemyFirstLine[column].isOccupied)
                {
                    if (cardData.abilities.Contains(CardAbility.DefaultVerticalLinearAttack) && gameBoardRegulator.enemySecondLine[column].isOccupied) //VERTICAL additional attack
                    {
                        Debug.Log("Vertical");
                        ApplyIgnite(gameBoardRegulator, 0, column, false); //attack main target
                        yield return StartCoroutine(AttackAnimationLocal(gameBoardRegulator.enemyFirstLine[column].occupant.gameObject.transform.localPosition, attackDelay));
                        yield return new WaitForSeconds(attackDelay);
                        gameBoardRegulator.enemyFirstLine[column].occupant.OnHit(!isPlayer, 1, column, cardData.Attack);

                        yield return StartCoroutine(AttackAnimationLocal(gameBoardRegulator.enemySecondLine[column].occupant.gameObject.transform.localPosition, attackDelay)); //attack second target
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
                                if (i == column)
                                    ApplyIgnite(gameBoardRegulator, 0, i, false); //ignite only main target
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
                        ApplyIgnite(gameBoardRegulator, 1, column, false);

                        yield return StartCoroutine(AttackAnimationLocal(gameBoardRegulator.enemySecondLine[column].occupant.gameObject.transform.localPosition, attackDelay));
                        gameBoardRegulator.enemySecondLine[column].occupant.OnHit(!isPlayer, 0, column, cardData.Attack);
                        yield return new WaitForSeconds(attackDelay);

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
                                if (i == column)
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
                    //yield return StartCoroutine(AttackAnimationLocal(gameBoardRegulator.playerFirstLine[column].occupant.gameObject.transform.localPosition + new Vector3(0, Y, 0), attackDelay));

                    if (cardData.abilities.Contains(CardAbility.DefaultVerticalLinearAttack) && gameBoardRegulator.playerSecondLine[column].isOccupied)
                    {
                        Debug.Log("Vertical");
                        ApplyIgnite(gameBoardRegulator, 0, column, true);

                        yield return StartCoroutine(AttackAnimationLocal(gameBoardRegulator.playerFirstLine[column].occupant.gameObject.transform.localPosition + new Vector3(0, Y, 0), attackDelay));
                        gameBoardRegulator.playerFirstLine[column].occupant.OnHit(!isPlayer, 0, column, cardData.Attack);
                        yield return new WaitForSeconds(attackDelay);

                        yield return StartCoroutine(AttackAnimationLocal(gameBoardRegulator.playerSecondLine[column].occupant.gameObject.transform.localPosition + new Vector3(0, Y, 0), attackDelay));
                        gameBoardRegulator.playerSecondLine[column].occupant.OnHit(!isPlayer, 1, column, cardData.Attack);
                        yield return new WaitForSeconds(attackDelay);
                    }

                    if (cardData.abilities.Contains(CardAbility.DefaultHorizontalLinearAttack) || cardData.abilityAndStatus.ContainsKey(CardAbility.DefaultHorizontalLinearAttack))
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            if (gameBoardRegulator.playerFirstLine[i].isOccupied)
                            {
                                Debug.Log("Horizontal");
                                if (i == column)
                                    ApplyIgnite(gameBoardRegulator, 0, i, true);

                                yield return StartCoroutine(AttackAnimationLocal(gameBoardRegulator.playerFirstLine[i].occupant.gameObject.transform.localPosition + new Vector3(0, Y, 0), attackDelay));
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
                    //yield return StartCoroutine(AttackAnimationLocal(gameBoardRegulator.playerSecondLine[column].occupant.gameObject.transform.localPosition + new Vector3(0, Y, 0), attackDelay));


                    if (cardData.abilities.Contains(CardAbility.DefaultVerticalLinearAttack) && gameBoardRegulator.playerFirstLine[column].isOccupied)
                    {
                        Debug.Log("Vertical");
                        ApplyIgnite(gameBoardRegulator, 0, column, true);

                        yield return StartCoroutine(AttackAnimationLocal(gameBoardRegulator.playerSecondLine[column].occupant.gameObject.transform.localPosition + new Vector3(0, Y, 0), attackDelay));
                        gameBoardRegulator.playerSecondLine[column].occupant.OnHit(!isPlayer, 1, column, cardData.Attack);
                        yield return new WaitForSeconds(attackDelay);

                        yield return StartCoroutine(AttackAnimationLocal(gameBoardRegulator.playerFirstLine[column].occupant.gameObject.transform.localPosition + new Vector3(0, Y, 0), attackDelay));
                        gameBoardRegulator.playerFirstLine[column].occupant.OnHit(!isPlayer, 0, column, cardData.Attack);
                        yield return new WaitForSeconds(attackDelay);
                    }

                    else if (cardData.abilities.Contains(CardAbility.DefaultHorizontalLinearAttack) || cardData.abilityAndStatus.ContainsKey(CardAbility.DefaultHorizontalLinearAttack))
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            if (gameBoardRegulator.playerSecondLine[i].isOccupied)
                            {
                                Debug.Log("Horizontal");
                                ApplyIgnite(gameBoardRegulator, 1, i, true);

                                yield return StartCoroutine(AttackAnimationLocal(gameBoardRegulator.playerSecondLine[i].occupant.gameObject.transform.localPosition + new Vector3(0, Y, 0), attackDelay));
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
        if (cardData.abilities.Contains(CardAbility.IgniteCreature) && isPlayer)
        {
            gameBoardRegulator.playerSide[targetRow, targetColumn].occupant.ReceiveAbility(CardAbility.Ignited, cardData.abilities.FindIndex(x => x == CardAbility.IgniteCreature));
        }
        else if (cardData.abilities.Contains(CardAbility.IgniteCreature))
        {
            gameBoardRegulator.enemySide[targetRow, targetColumn].occupant.ReceiveAbility(CardAbility.Ignited, cardData.abilities.FindIndex(x => x == CardAbility.IgniteCreature));
        }
    }

    //LeanTween анимация для Атаки позиции
    // 238
    public virtual IEnumerator AttackAnimation(Vector3 location, float time)
    {
        transform.SetAsLastSibling();
        Vector3 tempLocation = gameObject.transform.position;
        LeanTween.move(gameObject, location, time / 2).setEaseInBack();
        yield return new WaitForSecondsRealtime(time / 2);
        LeanTween.move(gameObject, tempLocation, time / 2).setEaseInBack();
    }
    public virtual IEnumerator AttackAnimationLocal(Vector3 location, float time)
    {
        transform.SetAsLastSibling();
        Vector3 tempLocation = gameObject.transform.position;
        LeanTween.moveLocal(gameObject, location, time / 2).setEaseInBack();
        yield return new WaitForSecondsRealtime(time / 2);
        LeanTween.move(gameObject, tempLocation, time / 2).setEaseInBack();
    }

    //метод получения удара. Если умираем то сообщаем об этом полю боя
    public virtual void OnHit(bool isPlayer, int row, int column, int damage)
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

    public virtual void OnHit(int damage) //updated method
    {
        cardData.Health -= damage;
        EventBus.OnCardsInfoChanged?.Invoke();
        if (cardData.Health <= 0)    
            cellHost.DestroyCardinCell();
        
    }


    public virtual void TurnStart()
    {

    }

    public virtual void OnCardPlayed()
    {
        if (cardData.abilityAndStatus.ContainsKey(CardAbility.DrawCards))
            FindObjectOfType<TurnTransmitter>().DrawCardsForPlayer(cardData.abilityAndStatus[CardAbility.DrawCards], !isEnemyEntity);

        if (cardData.abilityAndStatus.ContainsKey(CardAbility.SummonCopy))
            for (int i = 0; i < cardData.abilityAndStatus[CardAbility.SummonCopy]; i++)
                CreatureSpawner.instance.spawnCreatureByNameOnField(cardData.Name, !isEnemyEntity);

        if (cardData.abilityAndStatus.ContainsKey(CardAbility.ShootForEachAlly)) //potency is damage, count is creatures
            if (isEnemyEntity) {
                var side = gameBoardRegulator.playerSide;
                int amountOfShots = gameBoardRegulator.EnemyUnitsCount;
                List<CardEntity> livingCreatures = new List<CardEntity>();
                foreach (BoardCell cell in side)
                    if (cell.isOccupied)
                        livingCreatures.Add(cell.occupant);
                for (int i = 0; i < amountOfShots;i++)
                {
                    int random = Random.Range(0, livingCreatures.Count);
                    if ((livingCreatures[random].cardData.Health - cardData.abilityAndStatus[CardAbility.ShootForEachAlly]) <= 0)
                    {
                        livingCreatures[random].OnHit
                    }
                }

            }
        else
                for (int i = 0; i < gameBoardRegulator.EnemyUnitsCount; i++)
                    CreatureSpawner.instance.spawnCreatureByNameOnField(cardData.Name, !isEnemyEntity);

    }

    public virtual void TurnEnd(bool isPlayer, int row, int column)
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
        cardData.abilities.Add(cardAbility); //DEPRECATED
        cardData.abilityPotency.Add(potency);

        if (cardAbility == CardAbility.Ignited)
            displayController.BURN(true);
        if (abilitiesAndStatus.ContainsKey(cardAbility))
            abilitiesAndStatus[cardAbility] += potency;
        else
            abilitiesAndStatus.Add(cardAbility, potency);

    }

    public void RemoveAbility(CardAbility cardAbility)
    {
        cardData.abilityPotency.RemoveAt(cardData.abilities.FindIndex(x => x == cardAbility)); //DEPRECATED
        cardData.abilities.Remove(cardAbility);
        if (cardAbility == CardAbility.Ignited)
            displayController.BURN(false);
        abilitiesAndStatus.Remove(cardAbility);

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
        abilitiesAndStatus[cardAbility] += value;
    }

    public void changeStats(int attack, int health) //Buffs/Debuffs and damage/heal effects go here
    {
        cardData.Attack += attack;
        cardData.Health += health;
        if (cardData.Health <= 0)
            cellHost.DestroyCardinCell();
    }

}
