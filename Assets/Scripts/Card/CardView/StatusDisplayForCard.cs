using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusDisplayForCard : MonoBehaviour
{
    //private CardData cardData;
    [SerializeField] private List<Image> imageSlots;
    void Start()
    {
        //cardData = GetComponentInParent<CardData>();// Go through all status, and set active equal to count. All others set active false. Request image from Storage
    }

    public void updateStatus(CardData cardData)
    {
        for (int i = 0; i < imageSlots.Count; i++)
        {
            if (i < cardData.abilityAndStatus.Count && StatusStorage.StatusSprites.ContainsKey(cardData.abilities[i].ToString()))
            {
                imageSlots[i].sprite = StatusStorage.StatusSprites[cardData.abilities[i].ToString()];
                imageSlots[i].gameObject.SetActive(true);
            }
            else
                imageSlots[i].gameObject.SetActive(false);

        }
    }


}
