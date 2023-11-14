using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public  class SystemRank: MonoBehaviour
{
    [SerializeField] private List<Sprite> sprites;
    [SerializeField] private List<string> nameRanks;
    [SerializeField] private List<int> pointsRank;
    public static Dictionary<string, Sprite> SpritesRank;
    public static Dictionary<string, int> PointsRanks;

    public void Start()
   {

       SpritesRank = new Dictionary<string, Sprite>();
       PointsRanks = new Dictionary<string, int>();
       for(int i = 0; i < nameRanks.Count; i++)
       {
           SpritesRank.Add(nameRanks[i], sprites[i]);
           PointsRanks.Add(nameRanks[i], pointsRank[i]);
       }
   }
    public static  int victoryPoints = 18, losePoints = 10;
   
}
