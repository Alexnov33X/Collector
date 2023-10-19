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
}
