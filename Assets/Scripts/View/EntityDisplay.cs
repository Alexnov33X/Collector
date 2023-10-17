using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EntityDisplay : MonoBehaviour
{

    public Image artworkImage;
    public Text healthText;
    public Text attackText;
    public Image statusEffect;
    public Image affinity;
    void Start()
    {
        updateInformation();
        artworkImage.sprite = GetComponent<Entity>().artwork;
    }
    public void updateInformation()
    {
        if (gameObject != null)
        {
            Entity pc = GetComponent<Entity>();
            healthText.text = pc.health.ToString();
            attackText.text = pc.attack.ToString();
        }
    }
}
