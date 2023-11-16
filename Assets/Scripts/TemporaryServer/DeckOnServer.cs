using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// затычка, что-то вроде записи в БД о деке игрока.
/// </summary>
[CreateAssetMenu(fileName = "DeckOnServer", menuName = "Deck")]
public class DeckOnServer : ScriptableObject
{
    
    public List<CardScriptableObject> currentDeck;
    public List<CardScriptableObject> enemyCurrentDeck;
}
