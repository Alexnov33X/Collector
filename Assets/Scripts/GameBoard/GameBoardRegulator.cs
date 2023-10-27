using System.Collections.Generic;
using UnityEngine;
using System.Collections;

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
    public BoardCell[,] enemySide; //����� ��������� ��� ����������� ������� ������ �������. ��� ������ ��� �������...
    public BoardCell[,] playerSide;

    public PlayerHero enemyHero; //��������� ������ �� ������� �� ����
    public PlayerHero playerHero;

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

    public void OrderAttackToCells(bool isPlayer)
    {
        StartCoroutine(CreatureAttacks(isPlayer));
    }
    //���������� �� ���� ������� � ������� �� ���������
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
