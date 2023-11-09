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
    public float angle = 0;
    public float radius = 0.1f;
    public float speed = 1f;

    public GameObject windowOpponentFound;
    public GameObject contentOpponentFound;
    public Vector3 offset;
    public void Update()
    {
        if(timer > 0) timer -= Time.deltaTime;
        if(timer < 0) {timer = 0; gameObject.SetActive(false);OpponentFound();}
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

    public void OpponentFound()
    {
        windowOpponentFound.SetActive(true);
        
      for(int i = 0; i < contentOpponentFound.transform.childCount; i++)
        {
            GameObject currentChild = contentOpponentFound.transform.GetChild(i).gameObject;
            Vector3 basePosition = currentChild.transform.position;

            Color baseColor;
            if (currentChild.GetComponent<TMP_Text>().IsUnityNull())
            {
                baseColor = currentChild.GetComponent<Image>().color;
                currentChild.gameObject.GetComponent<Image>().color = new Color(baseColor.r,baseColor.g,baseColor.b,0);
            }
            else
            {
                baseColor = currentChild.GetComponent<TMP_Text>().color;
                currentChild.gameObject.GetComponent<TMP_Text>().color = new Color(baseColor.r,baseColor.g,baseColor.b,0);
            }
            currentChild.transform.position -= offset;
            
            LeanTween.move(currentChild, basePosition, 1f);
            Debug.Log(baseColor);
            ///LeanTween.color(currentChild.gameObject.GetComponent<TMP_Text>(), Color.HSVToRGB(255,255,255), 1f); 
        }
    }
}
