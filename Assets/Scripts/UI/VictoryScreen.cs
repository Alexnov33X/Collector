using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class VictoryScreen : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI endText;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
    }
    public void EndGame(bool victory)
    {
        if (!victory)
            gameObject.SetActive(true);
        else
        {
            endText.text = "You lost!";
            endText.color = Color.red;
            gameObject.SetActive(true);
        }
    }

}
