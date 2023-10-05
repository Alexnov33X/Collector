using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCard : MonoBehaviour
{
	public bool hasBeenPlayed;
	public int handIndex;

	GameManager gm;

	private Animator anim;
	private Animator camAnim;

	public GameObject effect;
	public GameObject hollowCircle;

	private void Start()
	{
		gm = FindObjectOfType<GameManager>();
		anim = GetComponent<Animator>();
		camAnim = Camera.main.GetComponent<Animator>();
	}
	private void OnMouseDown()
	{
        if (!hasBeenPlayed)
        {
            Instantiate(hollowCircle, transform.position, Quaternion.identity);

            camAnim.SetTrigger("shake");
            anim.SetTrigger("move");
			Debug.Log(transform.localScale);
			//transform.position += Vector3.up * 3f;
			gm.PlayCard(this);
			//transform.localScale = new Vector3(3, 3, 3);
			
			Debug.Log(transform.localScale);
			hasBeenPlayed = true;
            gm.availableCardSlots[handIndex] = true;
            Invoke("MoveToDiscardPile", 4f);
        }
        
    }

	void MoveToDiscardPile()
	{
		Instantiate(effect, transform.position, Quaternion.identity);
		gm.discardPile.Add(this);
		gameObject.SetActive(false);
	}



}
