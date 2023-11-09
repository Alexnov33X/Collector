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

    public GameObject windowOpponentFound;
    public GameObject contentOpponentFound;
    public Vector3 offset;
    
    public void Update()
    {
        if(tmpTimer > 0) tmpTimer-= Time.deltaTime;
        if(tmpTimer < 0) {tmpTimer = 0; gameObject.SetActive(false);OpponentFound();}
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
    public void OpponentFound()
    {
        windowOpponentFound.SetActive(true);
        
      for(int i = 0; i < contentOpponentFound.transform.childCount; i++)
        {
            GameObject currentChild = contentOpponentFound.transform.GetChild(i).gameObject;
            Vector3 basePosition = currentChild.transform.position;
            currentChild.transform.position -= offset;
            if (currentChild.GetComponent<TMP_Text>().IsUnityNull())
            {
                Color baseColor = currentChild.GetComponent<Image>().color;
                currentChild.gameObject.GetComponent<Image>().color = new Color(baseColor.r,baseColor.g,baseColor.b,0);
                LeanTween.alpha(currentChild.GetComponent<RectTransform>(),1f , 1f);
            }
            else
            {
                Color baseColor = currentChild.GetComponent<TMP_Text>().color;
                currentChild.gameObject.GetComponent<TMP_Text>().color = new Color(baseColor.r,baseColor.g,baseColor.b,0);
                LeanTween.alphaVertex(currentChild,1f , 1f);
            }
            LeanTween.move(currentChild, basePosition, 1f);
        }
    }
}
