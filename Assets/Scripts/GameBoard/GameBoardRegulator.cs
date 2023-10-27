using System.Collections.Generic;
using UnityEngine;
using System.Collections;

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

    private void Start()
    {
        InitializeSideArrays();
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
    }

    /// <summary>
    /// Попытаться призвать Карты из руки на доску
    /// </summary>
    /// <param name="card">Карта для призыва</param>
    /// <returns>true - получилось / false - нет</returns>
    public bool TrySummonCardToPlayerBoard(CardEntity card, bool isPlayer)
    {
        BoardCell freeCell = null;
        if (isPlayer)
            freeCell = ReturnFreeCell(playerSide);
        else
            freeCell = ReturnFreeCell(enemySide);

        if (freeCell == null)
            return false;

        freeCell.SetCardinCell(card);

        return true;

    }

    /// <summary>
    /// Находит свободную клетку и возвращает ее. Если все клетки заняты, то возвращает null
    /// </summary>
    /// <param name="boardSide">В какой доске искать</param>
    /// <returns>Возвращает свободный BoardCell или null</returns>
    public BoardCell ReturnFreeCell(BoardCell[,] boardSide)
    {
        foreach (BoardCell cell in boardSide)
        {
            if (!cell.isOccupied)
            {
                return cell;
            }
        }

        return null;
    }

    public void OrderAttackToCells(bool isPlayer)
    {
        StartCoroutine(CreatureAttacks(isPlayer));
    }
    //Проходимся по всем ячейкам и говорим им атаковать
    public IEnumerator CreatureAttacks(bool isPlayer)
    {
        if (isPlayer)
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (playerSide[i, j].occupant != null)
                    {
                        playerSide[i, j].occupant.Attack(this, isPlayer, i, j);
                        yield return new WaitForSeconds(1);
                    }
                }
            }
        else
        {
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (enemySide[i, j].occupant != null)
                    {
                        enemySide[i, j].occupant.Attack(this, isPlayer, i, j);
                        yield return new WaitForSeconds(1);
                    }
                }
            }
        }
    }
}
