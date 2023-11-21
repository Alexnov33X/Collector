using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using UnityEngine.UI;

public class SelectDeckTutor : MonoBehaviour
{
    public List<CardScriptableObject> deck1, deck2;
    [SerializeField] private DeckOnServer playerDecksServer;
    
    public void GetDeck1()
    {

        playerDecksServer.currentDeck = deck1;
        playerDecksServer.enemyCurrentDeck = deck2;
    }
    public void GetDeck2()
    {
        playerDecksServer.currentDeck = deck2;
        playerDecksServer.enemyCurrentDeck = deck1;
    }
    
}
