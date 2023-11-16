using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class TimerStartBattle : MonoBehaviour
{

  [SerializeField] private int timer;
  [SerializeField] private TMP_Text textTimer;
  [SerializeField] private float speedTimer;
    public IEnumerator StartTimer()
    {
        for (int i = timer; i >= 0; i--)
        {
            textTimer.text = i.ToString();
            yield return new WaitForSecondsRealtime(speedTimer); 
        }
       
        gameObject.SetActive(false);
    }

    public void OnEnable()
    {
        StartCoroutine(StartTimer());
    }
}
