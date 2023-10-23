using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPercentOnScreen : MonoBehaviour
{
    [Range(0, 100), SerializeField] private int horizontalPercent;
    [Range(0, 100), SerializeField] private int verticalPercent;

    private void Start()
    {
        RectTransform rectTransform = GetComponent<RectTransform>();
        Vector2 screenSize = new Vector2(Screen.width, Screen.height);
        rectTransform.sizeDelta = new Vector2(screenSize.x / 100 * horizontalPercent, screenSize.y / 100 * verticalPercent);
    }
}
