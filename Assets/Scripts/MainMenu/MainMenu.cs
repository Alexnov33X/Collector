using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public Window nextWindowPlay;
    public void OpenPlay()
    {
        OpenNextWindowPlay();
    }
    public void  OpenNextWindowPlay()
    {
        Window currentWindow = GetComponent<Window>();
        StartCoroutine(currentWindow.GetComponent<Window>().Activate(nextWindowPlay));
    }

    public IEnumerator OpenDecks()
    {
        yield return null;
    }

}
