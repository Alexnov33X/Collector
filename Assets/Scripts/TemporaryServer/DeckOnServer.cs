using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// затычка, что-то вроде записи в БД о деке игрока.
/// </summary>
public class DeckOnServer : ScriptableObject
{
    public List<CardScriptableObject> currentDeck;
}
