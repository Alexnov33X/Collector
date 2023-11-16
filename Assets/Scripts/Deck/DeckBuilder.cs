using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class DeckBuilder : MonoBehaviour
{
    public GameObject gridCardCollection;
    public GameObject gridDeck;
    [SerializeField] static int deckLimit = 24;
    [SerializeField] GameObject collectionCardPrefab;
    private int deckCount = 0;
    private List<CardScriptableObject> deck;
    private List<CardInCollectionDisplay> deckOfCollectionCards= new List<CardInCollectionDisplay>();
    public static DeckBuilder instance;

    public void SaveDeck()
    {
        deck = new List<CardScriptableObject>();
        var a = gridDeck.GetComponentsInChildren<CardInCollectionDisplay>();
        foreach (var card in a)
            deck.Add(card.cardSO);

        ServerSurrogate.Instance.currentDeckOnServer.currentDeck = deck;

    }

    public bool IsCardInDeck(CardScriptableObject card)
    {
        return deck.Contains(card);

    }

    public void AddCard(CardInCollectionDisplay cardToAdd)
    {
        if (!deck.Contains(cardToAdd.cardSO) && deckCount<deckLimit)
            deck.Add(cardToAdd.cardSO);
        deckCount = deck.Count;
        cardToAdd.switchAccess(true);
        RecompileDeck();
    }

    public void RemoveCard(CardInCollectionDisplay cardToRemove)
    {
        if (deck.Contains(cardToRemove.cardSO))
            deck.Remove(cardToRemove.cardSO);
        deckCount = deck.Count;
        cardToRemove.switchAccess(false);
        RecompileDeck();
    }

    private void RecompileDeck()
    {
        for (int i = 0; i < deckLimit; i++)
        {
            if (i < deckCount)
            {
                deckOfCollectionCards[i].InitCard(deck[i]);
                deckOfCollectionCards[i].gameObject.SetActive(true);
            }
            else
                deckOfCollectionCards[i].gameObject.SetActive(false);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        // “еперь, провер€ем существование экземпл€ра
        if (instance == null)
        { // Ёкземпл€р менеджера был найден
            instance = this; // «адаем ссылку на экземпл€р объекта
        }
        else if (instance == this)
        { // Ёкземпл€р объекта уже существует на сцене
            Destroy(gameObject); // ”дал€ем объект
        }

        deck = ServerSurrogate.Instance.currentDeckOnServer.currentDeck;
        for (int i = 0; i < deckLimit; i++)
        {
            var card = Instantiate(collectionCardPrefab, gridDeck.transform).GetComponent<CardInCollectionDisplay>(); //fill deck with fiddles
            if (i<deck.Count)
            {
                deckCount++;
                card.InitCard(deck[i]); //make fiddles into real cards
            }
            else
                card.gameObject.SetActive(false);
            deckOfCollectionCards.Add(card);

        }
        //foreach (CardScriptableObject card in deck)
        //{
        //    Instantiate(collectionCardPrefab, gridDeck.transform);
        //    deckCount++;
        //}    
    }
}
