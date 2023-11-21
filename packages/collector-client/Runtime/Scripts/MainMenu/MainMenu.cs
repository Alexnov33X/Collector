using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private TMP_Text rankPoints;
    [SerializeField] private Image rankIcon;
    public void OnEnable()
    {
        rankPoints.text = PlayerStats.RankPoints.ToString();
        rankIcon.sprite = SystemRank.SpritesRank[PlayerStats.rank];
    }
}
