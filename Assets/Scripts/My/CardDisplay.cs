using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardDisplay : MonoBehaviour {

	public CardInfo card;

	public Text nameText;
	public Text descriptionText;

	public Image artworkImage;

	public Text timeText;
	public Text attackText;
	public Text healthText;
	public Text cardType;
	public Text cardAffinity;

public Image cardFront;
public Image cardMask;
	//public Text cardRarity;

// public CardDisplay (CardInfo card) {
// 		nameText.text = card.name;
// 		descriptionText.text = card.description;

// 		artworkImage.sprite = card.artwork;

// 		timeText.text = card.timeCost.ToString();
// 		attackText.text = card.attack.ToString();
// 		healthText.text = card.health.ToString();
// 		cardType.text = card.cardType.ToString();
// 		cardAffinity.text = card.cardAffinity.ToString();
// 		//cardRarity.text = card.cardRarity.ToString();
// 		//HideInformation();
// 	}
	// Use this for initialization
	void Start () {
		nameText.text = card.name;
		descriptionText.text = card.description;

		artworkImage.sprite = card.artwork;

		timeText.text = card.timeCost.ToString();
		attackText.text = card.attack.ToString();
		healthText.text = card.health.ToString();
		cardType.text = card.cardType.ToString();
		cardAffinity.text = card.cardAffinity.ToString();
		if (card.attack== 125)
		HideInformation();
		//cardRarity.text = card.cardRarity.ToString();
		//HideInformation();
	}

	public void HideInformation(){
	nameText.gameObject.SetActive(false);
    descriptionText.gameObject.SetActive(false);
	attackText.gameObject.SetActive(false);
	healthText.gameObject.SetActive(false);
	cardType.gameObject.SetActive(false);
	cardAffinity.gameObject.SetActive(false);
cardFront.gameObject.SetActive(false);
cardMask.gameObject.GetComponent<Mask>().enabled=false;
    }	
}
