using UnityEngine;
using System.Collections;
using System;


public class GameBoardRegulator : MonoBehaviour
{
    /// <summary>
    /// Unity инспектор не отображает двумерный массив, поэтому используем 
    /// одномерные массивы для инициализции enemySide and playerSide
    /// </summary>
    public BoardCell[] enemySecondLine = new BoardCell[3];
    public BoardCell[] enemyFirstLine = new BoardCell[3];
    public BoardCell[] playerFirstLine = new BoardCell[3];
    public BoardCell[] playerSecondLine = new BoardCell[3];

    /// <summary>
    /// Хранят в себе ячейки соответствующих сторон
    /// </summary>
    public BoardCell[,] enemySide; //делаю публичным для упрощенного доступа других классов. Нет смысла это прятать...
    public BoardCell[,] playerSide;

    public PlayerHero enemyHero; //сохраняем ссылки на игроков на поле
    public PlayerHero playerHero;
    private float attackDelay = 1;
    private int playerUnits = 0;
    private int enemyUnits = 0;

    private void Start()
    {
        InitializeSideArrays();
        attackDelay = AnimationAndDelays.instance.attackAnimation;
    }

    /// <summary>
    /// Инициализирует enemySide и playerSide BoardCell'ами
    /// </summary>
    private void InitializeSideArrays()
    {
        enemySide = new BoardCell[2, 3]
        {
            { enemyFirstLine[0], enemyFirstLine[1], enemyFirstLine[2]},
            { enemySecondLine[0], enemySecondLine[1], enemySecondLine[2] }
        };

        playerSide = new BoardCell[2, 3]
        {
            { playerFirstLine[0], playerFirstLine[1], playerFirstLine[2]},
            { playerSecondLine[0], playerSecondLine[1], playerSecondLine[2] }
        };
        for (int i = 0; i < 6; i++)
        {
            enemySide[i / 3, i % 3].cellPosition = new Vector2(i / 3, i % 3);
            enemySide[i / 3, i % 3].cellSide = false;
            playerSide[i / 3, i % 3].cellPosition = new Vector2(i / 3, i % 3);
            playerSide[i / 3, i % 3].cellSide = true;
        }
    }

    /// <summary>
    /// Попытаться призвать Карты из руки на доску
    /// </summary>
    /// <param name="card">Карта для призыва</param>
    /// <returns>true - получилось / false - нет</returns>
    public bool TrySummonCardToPlayerBoard(CardEntity card, bool isPlayer)
    {
        BoardCell freeCell = null;
        if (isPlayer && playerUnits < 6)
            freeCell = ReturnFreeCell(playerSide);
        else if (enemyUnits < 6)
            freeCell = ReturnFreeCell(enemySide);
        if (freeCell == null)
            return false;

        card.gameObject.transform.SetParent(gameObject.transform);
        freeCell.SetCardinCell(card);
        if (isPlayer)
            playerUnits++;
        else
            enemyUnits++;
        return true;

    }

    /// <summary>
    /// Находит свободную клетку и возвращает ее. Если все клетки заняты, то возвращает null
    /// </summary>
    /// <param name="boardSide">В какой доске искать</param>
    /// <returns>Возвращает свободный BoardCell или null</returns>
    public BoardCell ReturnFreeCell(BoardCell[,] boardSide)
    {

        int index = UnityEngine.Random.Range(0, 6); //finding random spawn spot

        if (!boardSide[index / 3, index % 3].isOccupied)
            return boardSide[index / 3, index % 3];
        else
        {
            index = UnityEngine.Random.Range(0, 6);
            while (boardSide[index / 3, index % 3].isOccupied)
                index = UnityEngine.Random.Range(0, 6);
            return boardSide[index / 3, index % 3];
        }
    }

    public IEnumerator OrderAttackToCells(bool isPlayer)
    {
        yield return StartCoroutine(CreatureAttacks(isPlayer));
    }
    //Проходимся по всем ячейкам и говорим им атаковать
    public IEnumerator CreatureAttacks(bool isPlayer)
    {
        if (isPlayer)
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    if (playerSide[j, i].occupant != null)
                    {
                        yield return playerSide[j, i].occupant.Attack(this, isPlayer, j, i);
                        yield return new WaitForSeconds(attackDelay);
                    }
                }
            }
        else
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    if (enemySide[j, i].occupant != null)
                    {
                        yield return enemySide[j, i].occupant.Attack(this, isPlayer, j, i);
                        yield return new WaitForSeconds(attackDelay);
                    }
                }
            }
        }
    }

    public IEnumerator TurnEnd(bool isPlayer)
    {
        if (isPlayer)
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    if (playerSide[j, i].occupant != null)
                    {
                        playerSide[j, i].occupant.TurnEnd(isPlayer, j, i);
                        yield return new WaitForSeconds(attackDelay);
                    }
                }
            }
        else
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    if (enemySide[j, i].occupant != null)
                    {
                        enemySide[j, i].occupant.TurnEnd(isPlayer, j, i);
                        yield return new WaitForSeconds(attackDelay);
                    }
                }
            }
        }
    }
}
