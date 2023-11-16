using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Window : MonoBehaviour
{

    public List<GameObject> objects;
    public float timeOpen, timeClose, delayBetweenObject;
    [SerializeField] private LeanTweenType easingOpenFunction, easingClosedFunction;
    [SerializeField] private bool isOrderObjects;
    public Window nextWindow;
    private IEnumerator Activate(Window window, bool isCloseOldWindow)
    {
        if (isCloseOldWindow)
        {
            foreach (var obj in objects)
            {
                obj.transform.localScale = new Vector3(1,1,1);
                LeanTween.scale(obj, new Vector3(0, 0, 0), timeClose).setEase(easingClosedFunction);
            }
            yield return new WaitForSecondsRealtime(timeClose);
            gameObject.SetActive(false);
        }
        foreach (var obj in window.objects)
        {
            obj.transform.localScale = new Vector3(0,0,0);
        }
        window.gameObject.SetActive(true);
        foreach (var obj in window.objects)
        {
            LeanTween.scale(obj, new Vector3(1, 1, 1), window.timeOpen).setEase(easingOpenFunction);
            if (isOrderObjects)
            {
                yield return  new WaitForSecondsRealtime(window.delayBetweenObject);
            }
        }
        yield return new WaitForSecondsRealtime(window.timeOpen);
    } 
    public void OpenNextWindow(Window window)
    {
        StartCoroutine(Activate(window, false));
    }
    public void OpenNextWindowAndCloseOldWindow(Window window)
    {
        StartCoroutine(Activate(window, true));
    }

}
