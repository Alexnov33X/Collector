using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �������, ���-�� ����� ������ � �� � ���� ������.
/// </summary>
[CreateAssetMenu(fileName = "DeckOnServer", menuName = "Deck")]
public class DeckOnServer : ScriptableObject
{
    public CardScriptableObject[] currentDeck;
}
