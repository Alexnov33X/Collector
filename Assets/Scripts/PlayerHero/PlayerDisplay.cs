using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerDisplay : MonoBehaviour
{
 
    [SerializeField] private TextMeshProUGUI healthText;
    
    public void updateInformation(string health)
    {
            healthText.text = health;         
    }
}
