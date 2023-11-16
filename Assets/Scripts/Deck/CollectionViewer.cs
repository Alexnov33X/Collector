using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class CollectionViewer : MonoBehaviour
{
    CardScriptableObject[] cardsToDisplay;
    public GameObject collectionCardPrefab;
    private List<CardInCollectionDisplay> cards = new List<CardInCollectionDisplay>();
    private bool dragonsOn = true;
    private bool piratesOn = true;
    // Start is called before the first frame update
    void Start()
    {
        Object[] cardObjects = Resources.LoadAll("DefaultSO");
        cardsToDisplay = new CardScriptableObject[cardObjects.Length];
        cardObjects.CopyTo(cardsToDisplay, 0);
        foreach (var card in cardsToDisplay)
        {
            GameObject emptyCard = Instantiate(collectionCardPrefab, transform);
            var display = emptyCard.GetComponent<CardInCollectionDisplay>();
            display.InitCard(card);
            Debug.Log(display == null);
            cards.Add(display);
        }
    }

    private void UpdateCollection()
    {
        //var collection = GetComponentsInChildren<CardInCollectionDisplay>();
        //Debug.Log(cards.Count());
        foreach (var card in cards)
        {
            if (!dragonsOn && card.cardSO.CardUniverse == Enums.CardUniverse.Dragons)
                card.gameObject.SetActive(false);
            else if (!piratesOn && card.cardSO.CardUniverse == Enums.CardUniverse.Pirates)
                card.gameObject.SetActive(false);
            else
                card.gameObject.SetActive(true);
        }
    }
    public void SwitchDragon(bool value)
    {
        Debug.Log(value);
        dragonsOn = !value;
        UpdateCollection();
    }

    public void SwitchPirates(bool value)
    {
        Debug.Log(value);
        piratesOn = !value;
        UpdateCollection();
    }
}
