using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DeckBuilder : MonoBehaviour, IPointerClickHandler
{
    public GameObject gridCardCollection;
    public GameObject gridDeck;
    private List<CardScriptableObject> deck;
    public void OnPointerClick(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    public void SaveDeck()
    {
        deck = new List<CardScriptableObject>();
        var a = gridDeck.GetComponentsInChildren<CardInCollectionDisplay>();
        foreach (var card in a)
            deck.Add(card.cardSO);

        ServerSurrogate.Instance.currentDeckOnServer.currentDeck = deck;

    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
