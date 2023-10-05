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
	//public Text cardRarity;

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
		//cardRarity.text = card.cardRarity.ToString();
	}
	
}
