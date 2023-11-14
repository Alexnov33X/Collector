using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class VictoryScreen : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI endText;

    [SerializeField] private TMP_Text textNewPoints;
    [SerializeField] private Color colorText;
    // Start is called before the first frame update

    void Awake()
    {
        gameObject.SetActive(false);
        //endText.color = colorText;
    }

    /// <summary>
    /// В зависимости от победы или поражения меняем текст и показываем его
    /// </summary>
    public void EndGame(bool victory)
    {
        Window currentWindow = GetComponent<Window>();
        gameObject.SetActive(true);
        currentWindow.OpenNextWindow(currentWindow);
        if(victory)
        {
            endText.text = "VICTORY!";
            textNewPoints.text = "+" + SystemRank.victoryPoints;
        }
        else
        {
            endText.text = "DEFEAT";
        }
    }
}
