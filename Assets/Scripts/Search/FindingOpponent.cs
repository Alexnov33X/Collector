using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class FindingOpponent : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject contentOpponentFound;
    public Vector3 offset;
    private TMP_Text text;
    public float timeMove;
    public float timeGetColor;
    
    public void Activate()
    {
        gameObject.SetActive(true);
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
                text = currentChild.GetComponent<TMP_Text>();
                Color baseColor = text.color;
                currentChild.gameObject.GetComponent<TMP_Text>().color = new Color(baseColor.r,baseColor.g,baseColor.b,0);
                var fadeoutcolor = baseColor;
                fadeoutcolor.a = 0;
                LeanTween.value(gameObject, UpdateValueExampleCallback,fadeoutcolor, baseColor, timeGetColor);
            }
            LeanTween.move(currentChild, basePosition, timeMove).setEase(LeanTweenType.easeOutQuint);
        }
    }
    void UpdateValueExampleCallback(Color val)
    {
        text.color = val;
    }
}
