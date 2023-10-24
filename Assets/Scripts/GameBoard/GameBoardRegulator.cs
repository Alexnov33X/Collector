using System.Collections.Generic;
using UnityEngine;

public class GameBoardRegulator : MonoBehaviour
{
    /// <summary>
    /// Unity ��������� �� ���������� ��������� ������, ������� ���������� 
    /// ���������� ������� ��� ������������ enemySide and playerSide
    /// </summary>
    public BoardCell[] enemySecondLine = new BoardCell[3];
    public BoardCell[] enemyFirstLine = new BoardCell[3];
    public BoardCell[] playerFirstLine = new BoardCell[3];
    public BoardCell[] playerSecondLine = new BoardCell[3];

    /// <summary>
    /// ������ � ���� ������ ��������������� ������
    /// </summary>
    private BoardCell[,] enemySide;
    private BoardCell[,] playerSide;

    private void Start()
    {
        InitializeSideArrays();
    }

    /// <summary>
    /// �������������� enemySide � playerSide BoardCell'���
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
    /// ���������� �������� ����� �� ���� �� �����
    /// </summary>
    /// <param name="card">����� ��� �������</param>
    /// <returns>true - ���������� / false - ���</returns>
    public bool TrySummonCardToPlayerBoard(CardEntity card)
    {
        BoardCell freeCell = ReturnFreeCell(playerSide);
        
        if (freeCell == null)
            return false;
        
        freeCell.SetCardinCell(card);

        return true;
    }

    /// <summary>
    /// ������� ��������� ������ � ���������� ��. ���� ��� ������ ������, �� ���������� null
    /// </summary>
    /// <param name="boardSide">� ����� ����� ������</param>
    /// <returns>���������� ��������� BoardCell ��� null</returns>
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
