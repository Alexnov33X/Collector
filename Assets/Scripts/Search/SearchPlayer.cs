using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SearchPlayer : MonoBehaviour
{

    public GameObject icon;
    public float timer = 5f;
    private float tmpTimer;
    public float angle = 0;
    public float radius = 0.1f;
    public float speed = 1f;
     
    public FindingOpponent findingOpponent;
    public Window cancelWindow;

    public void Start()
    {
        tmpTimer = timer;
    }
    public void Update()
    {
        if(tmpTimer > 0)
        {
            tmpTimer-= Time.deltaTime;
        }
       
        if (tmpTimer < 0)
        {
            tmpTimer = 0;
            Window window = GetComponent<Window>();
            Invoke("OpenOpponentFound", window.timeClose);
        }
        angle += Time.deltaTime; // меняется плавно значение угла
        var x = -Mathf.Cos (angle * speed) * radius;
        var y = Mathf.Sin (angle * speed) * radius;
        icon.transform.position = new Vector3( x, y, icon.transform.position.z);
    }
    
    public void OpenOpponentFound()
    {
        gameObject.SetActive(false);
        tmpTimer = timer;
        findingOpponent.Activate();
    }
    public void Cancel()
    {
        tmpTimer = timer;
        Window currentWindow = GetComponent<Window>();
        StartCoroutine(currentWindow.Activate(cancelWindow));
    }
}
