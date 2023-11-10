using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.UI;
public class Loader : MonoBehaviour
{
    public float timer;
    //public float period;
    public Image progressBar;
    public TMP_Text textProgress;
    


   public  IEnumerator LoadWindow(GameObject loadedWindow)
    {
        float random1 = Random.Range(1, 50);
        float random2 = Random.Range(51, 95);
        gameObject.SetActive(true);
        for(float i = 0; i <100; i++)
        {
            progressBar.fillAmount += 0.01f;
            textProgress.text = Mathf.Round(progressBar.fillAmount * 100) + "%";
            if (i == random1)
            {
                yield return  new WaitForSecondsRealtime(0.7f);
            }
            if (i == random2)
            {
                yield return  new WaitForSecondsRealtime(0.7f);
            }
            yield return  new WaitForSecondsRealtime(timer / 100);
        }
        gameObject.SetActive(false);
        loadedWindow.SetActive(true);
    }
}
