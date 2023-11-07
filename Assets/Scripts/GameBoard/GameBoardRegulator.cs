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
    private float attackDelay = 1;

    private void Start()
    {
        InitializeSideArrays();
        attackDelay = AnimationAndDelays.instance.attackAnimation;
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

        card.gameObject.transform.SetParent(gameObject.transform);
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

        int index = Random.Range(0, 6); //finding random spawn spot

        if (!boardSide[index / 3, index % 3].isOccupied) // need unit counter, when counter = 6 do not search and return NULL
            return boardSide[index / 3, index % 3];
        else
        {
            index = Random.Range(0, 6);
            while (boardSide[index / 3, index % 3].isOccupied)
                index = Random.Range(0, 6);
            return boardSide[index / 3, index % 3];
        }
    }

    public IEnumerator OrderAttackToCells(bool isPlayer)
    {
        yield return StartCoroutine(CreatureAttacks(isPlayer));
    }
    //���������� �� ���� ������� � ������� �� ���������
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
                        yield return new WaitForSeconds(attackDelay + 0.1f);
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
                        yield return new WaitForSeconds(attackDelay + 0.1f);
                    }
                }
            }
        }
    }
}
