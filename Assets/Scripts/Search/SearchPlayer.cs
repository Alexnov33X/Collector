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
    public GameObject header;
    public GameObject cancel;
    public float timer = 5f;
    private float tmpTimer;
    public float angle = 0;
    public float radius = 0.1f;
    public float speed = 1f;
     
    public FindingOpponent findingOpponent;
    
    public float timeClose;
    
    public void Update()
    {
        if(tmpTimer > 0) tmpTimer-= Time.deltaTime;
        if(tmpTimer < 0) {tmpTimer = 0; AnimationClose(); Invoke("OpenOpponentFound", timeClose + 0.2f);}
        angle += Time.deltaTime; // меняется плавно значение угла
        var x = -Mathf.Cos (angle * speed) * radius;
        var y = Mathf.Sin (angle * speed) * radius;
        icon.transform.position = new Vector3( x, y, icon.transform.position.z);
    }

    public void OpenSearch(float time)
    {
        tmpTimer = timer;
        Animation(time);
    }

    public void OpenOpponentFound()
    {
        gameObject.SetActive(false);
        ReturnBaseSizes();
        findingOpponent.Activate();
    }

    public void Animation(float timeDelay)
    {
        icon.transform.localScale = new Vector3(0,0,0);
        header.transform.localScale = new Vector3(0,0,0);
        cancel.transform.localScale = new Vector3(0,0,0);
        LeanTween.scale(icon, new Vector3(1, 1, 1), 0.4f).setDelay(timeDelay);
        LeanTween.scale(header, new Vector3(1, 1, 1), 0.4f).setDelay(timeDelay);
        LeanTween.scale(cancel, new Vector3(1, 1, 1), 0.2f).setDelay(timeDelay * 1.5f);
       // LeanTween.move(icon, Vector3.forward, -360, 10f).setLoopClamp();
    }

    private TMP_Text text;

    public void AnimationClose()
    {
        LeanTween.scale(cancel, Vector3.zero, timeClose);
        LeanTween.scale(header, Vector3.zero, timeClose);
        LeanTween.scale(icon, Vector3.zero, timeClose);
    }

    public void ReturnBaseSizes()
    {
        cancel.transform.localScale = Vector3.one;
        header.transform.localScale = Vector3.one;
        icon.transform.localScale = Vector3.one;
    }
}
