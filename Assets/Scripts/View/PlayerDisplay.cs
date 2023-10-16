using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDisplay : MonoBehaviour
{

    public Image artworkImage;
    public Text healthText;
    void Start()
    {
        updateInformation();
    }
    public void updateInformation()
    {
        if (gameObject != null)
        {
            Player pc = GetComponent<Player>();
            healthText.text = pc.health.ToString();
        }
    }
}
