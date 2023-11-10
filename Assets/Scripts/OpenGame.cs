using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenGame : MonoBehaviour
{
    public Loader loader;

    public GameObject mainMenu;
    public void Start()
    {
        StartCoroutine(loader.LoadWindow(mainMenu));
    }
    
}
