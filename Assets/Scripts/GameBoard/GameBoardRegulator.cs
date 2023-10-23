using System.Collections.Generic;
using UnityEngine;

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
    private BoardCell[,] enemySide;
    private BoardCell[,] playerSide;

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
    public bool TrySummonCardToPlayerBoard(CardEntity card)
    {
        BoardCell freeCell = ReturnFreeCell(playerSide);
        
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
}
