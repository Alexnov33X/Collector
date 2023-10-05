using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Enums;

[CreateAssetMenu(fileName = "New Card", menuName = "Card")]
public class CardInfo : ScriptableObject {

	public new string name;
	public string description;

	public Sprite artwork;

	public int timeCost = 1;
	public int attack;
	public int health;
	public CardType cardType;
	public CardAffinity cardAffinity;
	public CardRarity cardRarity;


	public void Print ()
	{
		Debug.Log(name + ": " + description + " The card costs: " + timeCost);
	}


}
