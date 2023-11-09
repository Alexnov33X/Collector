using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class SearchPlayer : MonoBehaviour
{

    public GameObject icon;
    public GameObject header;
    public GameObject cancel;
    public float timer = 5f;
    public float angle = 0;
    public float radius = 0.1f;
    public float speed = 1f;
    public void Update()
    {
        if(timer > 0) timer -= Time.deltaTime;
        if(timer < 0) timer = 0;
        angle += Time.deltaTime; // меняется плавно значение угла
        var x = -Mathf.Cos (angle * speed) * radius;
        var y = Mathf.Sin (angle * speed) * radius;
        icon.transform.position = new Vector3( x, y, icon.transform.position.z);
    }


    public void Animation(float timeDelay)
    {
        icon.transform.localScale = new Vector3(0,0,0);
        header.transform.localScale = new Vector3(0,0,0);
        cancel.transform.localScale = new Vector3(0,0,0);
        LeanTween.scale(icon, new Vector3(1, 1, 1), 0.4f).setDelay(timeDelay);
        LeanTween.scale(header, new Vector3(1, 1, 1), 0.4f).setDelay(timeDelay);
        LeanTween.scale(cancel, new Vector3(1, 1, 1), 0.2f).setDelay(timeDelay * 1.5f);
    }
}
