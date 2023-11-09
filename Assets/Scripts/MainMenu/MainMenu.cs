using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public SearchPlayer searchPlayer;

    public GameObject play, decks, header, rank;
    public float timeCloseMenu;
    public void StartSearhPlayer()
    {
        AnimationClose();
    }

    public void  AnimationClose()
    {
        LeanTween.scale(play, new Vector3(0, 0, 0), timeCloseMenu);
        LeanTween.scale(header, new Vector3(0, 0, 0), timeCloseMenu);
        LeanTween.scale(decks, new Vector3(0, 0, 0), timeCloseMenu);
        LeanTween.scale(rank, new Vector3(0, 0, 0), timeCloseMenu);
        Invoke("OpenWindowSearch", timeCloseMenu + 0.02f);
    }

    public void OpenWindowSearch()
    {
        gameObject.SetActive(false);
        ReturnBaseSize();
        searchPlayer.gameObject.SetActive(true);
        searchPlayer.OpenSearch(timeCloseMenu);
    }
    public void ReturnBaseSize()
    {
        play.transform.localScale = new Vector3(1,1,1);
        decks.transform.localScale = new Vector3(1,1,1);
        header.transform.localScale = new Vector3(1,1,1);
        rank.transform.localScale = new Vector3(1,1,1);
    }


}
