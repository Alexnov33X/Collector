using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.UI;
public class Loader : MonoBehaviour
{
    public float timer;
    public Image progressBar;
    public TMP_Text textProgress;

  //  public Window nextWindow;
    public void Start()
    {
        StartCoroutine(LoadWindow());
    }
    
   public  IEnumerator LoadWindow()
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
        Window window = GetComponent<Window>();
        window.OpenNextWindowAndCloseOldWindow(window.nextWindow);
    }
}
