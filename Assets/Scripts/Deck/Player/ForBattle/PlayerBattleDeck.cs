using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// ���������� ������ ������, ������� ������������ ��� ������. ���������������� ��� ������ ���(��� �������� ����� BattleScene) � 
/// ������������ ������ ��� ���. ������������� ���������� � BattleBootstrapp.
/// </summary>
public static class PlayerBattleDeck
{
    private static List<CardScriptableObject> battleDeck = new List<CardScriptableObject>();
    public static List<CardScriptableObject> BattleDeck { get { return battleDeck; } set => battleDeck = value; }

    /// <summary>
    /// ���������� ����� � ������
    /// ���� ��� ����� �������� ���. ����� �� �������.
    /// </summary>
    public static void AddCard()
    {

    }

    /// <summary>
    /// �������� ����� �� ������
    /// </summary>
    public static CardScriptableObject TransferCardToHand()
    {
        int elementIndex = Random.Range(0, battleDeck.Count);

        CardScriptableObject card = battleDeck[elementIndex];
        battleDeck.RemoveAt(elementIndex);

        return card;
    }
}
