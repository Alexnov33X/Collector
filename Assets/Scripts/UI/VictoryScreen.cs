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
        gameObject.SetActive(false); //прячем объект в начале игры
    }
    /// <summary>
    /// В зависимости от победы или поражения меняем текст и показываем его
    /// </summary>
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
