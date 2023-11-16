using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionViewer : MonoBehaviour
{
    CardScriptableObject[] cardsToDisplay;
    public GameObject collectionCardPrefab;
    // Start is called before the first frame update
    void Start()
    {
        Object[] cardObjects = Resources.LoadAll("DefaultSO");
        cardsToDisplay = new CardScriptableObject[cardObjects.Length];
        cardObjects.CopyTo(cardsToDisplay, 0);
        foreach (var card in cardsToDisplay)
        {
            GameObject emptyCard = Instantiate(collectionCardPrefab, transform);
            emptyCard.GetComponent<CardInCollectionDisplay>().InitCard(card);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
