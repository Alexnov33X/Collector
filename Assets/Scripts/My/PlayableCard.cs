using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayableCard : MonoBehaviour
{
    public CardInfo card;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnPlay()
    {

    }
    public GameObject SpawnThing()
    {
        return Instantiate(this.gameObject);
    }

}
