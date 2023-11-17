using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckWindowSwitcher : MonoBehaviour
{
    public GameObject otherWindow;
    public void Switch()
    {
        otherWindow.SetActive(true);
        gameObject.SetActive(false);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
