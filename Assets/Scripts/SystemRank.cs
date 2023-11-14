using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  static class SystemRank
{
   public enum Ranks
   {
       Silver = 100,
       Gold= 200, 
       Platinum = 300, 
       Diamond = 400, 
       Master = 500,
   }

   public static int victoryPoints, losePoints;
}
