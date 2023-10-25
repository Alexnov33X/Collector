using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// затычка, что-то вроде записи в БД о деке игрока.
/// </summary>
[CreateAssetMenu(fileName = "DeckOnServer", menuName = "Deck")]
public class DeckOnServer : ScriptableObject
{
    public CardScriptableObject[] currentDeck;
}
