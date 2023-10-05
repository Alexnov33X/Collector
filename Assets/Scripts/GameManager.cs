using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{

	public List<GameCard> deck;
	public TextMeshProUGUI deckSizeText;
	public Transform[] battleSlots;
	public bool[] availableBattleSlots;
	public Transform[] cardSlots;
	public bool[] availableCardSlots;

	public List<GameCard> discardPile;
	public TextMeshProUGUI discardPileSizeText;

	private Animator camAnim;

	private void Start()
	{
		camAnim = Camera.main.GetComponent<Animator>();
		availableBattleSlots = new bool[battleSlots.Length];
		//availableCardSlots = new bool[cardSlots.Length];
		Debug.Log(Random.Range(0, battleSlots.Length));
		Debug.Log(this);

	}

	public void DrawCard()
	{
		if (deck.Count >= 1)
		{
			camAnim.SetTrigger("shake");

			GameCard randomCard = deck[Random.Range(0, deck.Count)];
			for (int i = 0; i < availableCardSlots.Length; i++)
			{
				if (availableCardSlots[i] == true)
				{
					randomCard.gameObject.SetActive(true);
					randomCard.handIndex = i;
					randomCard.transform.position = cardSlots[i].position;
					randomCard.transform.rotation = cardSlots[i].rotation;
					randomCard.hasBeenPlayed = false;
					deck.Remove(randomCard);
					availableCardSlots[i] = false;
					return;
				}
			}
		}
	}

	public void PlayCard(GameCard gc)
	{
		int index = Random.Range(0, battleSlots.Length);
		//while (!availableBattleSlots[index])
		//	index = Random.Range(0, battleSlots.Length);
		//availableBattleSlots[index] = false;
		gc.transform.localPosition = battleSlots[index].position;
	}

	public void Shuffle()
	{
		if (discardPile.Count >= 1)
		{
			foreach (GameCard card in discardPile)
			{
				deck.Add(card);
			}
			discardPile.Clear();
		}
	}

	private void Update()
	{
		deckSizeText.text = deck.Count.ToString();
		discardPileSizeText.text = discardPile.Count.ToString();
	}

}
