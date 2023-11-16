using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class TimerStartBattle : MonoBehaviour
{

  [SerializeField] private int startNumberTimer;
  [SerializeField] private TMP_Text textTimer;
  [SerializeField] private float speedTimer;
  [SerializeField] private Sounder sounder;
  [SerializeField] private TurnTransmitter turnTransmitter;
    public IEnumerator StartTimer()
    {
        sounder.StopMusic();
        for (int i = startNumberTimer; i > 0; i--)
        {
            textTimer.text = i.ToString();
            sounder.PlaySound("timer_beep");
            yield return new WaitForSecondsRealtime(speedTimer); 
        }

        Window window = GetComponent<Window>();
        window.OpenNextWindowAndCloseOldWindow(window.nextWindow);
    }

    public void OnEnable()
    {
        StartCoroutine(StartTimer());
    }

    public void OnDisable()
    {
        sounder.PlayMusic("music_battle");
    }
}
