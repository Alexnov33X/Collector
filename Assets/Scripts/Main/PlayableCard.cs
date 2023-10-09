using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayableCard : MonoBehaviour
{
    public CardInfo card;

    public void OnPlay()
    {

    }
    public GameObject SpawnThing()
    {
        return Instantiate(this.gameObject);
    }

}
