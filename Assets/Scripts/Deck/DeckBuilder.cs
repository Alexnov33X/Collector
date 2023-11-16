using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class DeckBuilder : MonoBehaviour, IPointerClickHandler
{
    public GameObject gridCardCollection;
    public GameObject gridDeck;
    public static int deckLimit = 24;
    private int deckCount = 0;
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

    public void AddCard(CardInCollectionDisplay cardToAdd)
    {
        if (!deck.Contains(cardToAdd.cardSO) && deckCount<deckLimit)
            deck.Add(cardToAdd.cardSO);
        deckCount = deck.Count;
    }

    public void RemoveCard(CardInCollectionDisplay cardToRemove)
    {
        if (deck.Contains(cardToRemove.cardSO))
            deck.Remove(cardToRemove.cardSO);
        deckCount = deck.Count;
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
