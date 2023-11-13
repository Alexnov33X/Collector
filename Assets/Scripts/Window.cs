using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Window : MonoBehaviour
{

    public List<GameObject> objects;
    public float timeOpen, timeClose;
    public IEnumerator Activate(Window newWindow)
    {
        foreach (var obj in objects)
        {
            obj.transform.localScale = new Vector3(1,1,1);
            LeanTween.scale(obj, new Vector3(0, 0, 0), timeClose);
        }
        yield return new WaitForSecondsRealtime(timeClose);
        gameObject.SetActive(false);
        
        newWindow.gameObject.SetActive(true);
        foreach (var obj in newWindow.objects)
        {
            obj.transform.localScale = new Vector3(0,0,0);
            LeanTween.scale(obj, new Vector3(1, 1, 1), newWindow.timeOpen);
        }
        yield return new WaitForSecondsRealtime(newWindow.timeOpen);
    }
}
