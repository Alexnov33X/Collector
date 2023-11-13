using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using UnityEngine.UI;

public class SelectDeckTutor : MonoBehaviour
{
 //   public Button buttonDeck1, buttonDeck2;
  //  public GameObject header;
    public List<CardScriptableObject> deck1, deck2;
  //  public float timeCloseSelectDeck;

    public Window nextWindow;
    public void GetDeck1()
    {
        PlayerBattleDeck.BattleDeck.AddRange(deck1);
        OpenNextWindow();
    }
    public void GetDeck2()
    {
        PlayerBattleDeck.BattleDeck.AddRange(deck2);
        OpenNextWindow();
    }

    public void OpenNextWindow()
    {
        Window currentWindow = GetComponent<Window>();
        StartCoroutine(currentWindow.Activate(nextWindow));
        // StartCoroutine(currentWindow.Deactivate());
        //  LeanTween.scale(buttonDeck1.gameObject, new Vector3(0, 0, 0), timeCloseSelectDeck);
        // //  LeanTween.scale(buttonDeck2.gameObject, new Vector3(0, 0, 0), timeCloseSelectDeck);
       // LeanTween.scale(header, new Vector3(0, 0, 0), timeCloseSelectDeck);
        //yield return new WaitForSecondRealtime(timeCloseSelectDeck + 0.02f);
        // StartCoroutine(newWindow.Activate());
        
    }
}
