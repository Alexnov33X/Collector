using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using UnityEngine.UI;

public class SelectDeckTutor : MonoBehaviour
{
    public List<CardScriptableObject> deck1, deck2;
    
    public void GetDeck1()
    {
        PlayerBattleDeck.BattleDeck.AddRange(deck1);
    }
    public void GetDeck2()
    {
        PlayerBattleDeck.BattleDeck.AddRange(deck2);
    }
    
}
