using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Все наши Enum
/// </summary>
public static class Enums
{
    public enum CardType
    {
        Creature = 0,
        Spell,
        Artifact,
        Field
    }

    public enum CardRarity
    {
        Common = 0,
        Rare,
        Epic,
        Legendary
    }

    public enum CardUniverse
    {
        Dragons = 0,
        Pirates,
        Circus
    }

    /// <summary>
    /// Состояние карты на игровом поле. В руке\На доске.
    /// </summary>
    public enum CardState
    {
        OnHand = 0,
        OnBoard
    }

    public enum CardAbility
    {
        DefaultVerticalLinearAttack = 0,
        DefaultHorizontalLinearAttack,
        EveryoneAttack,
        Sleep,
        IgniteCreature,
        Ignited,
        SacrificeSpawn,       //Kill self and spawn new card on current place
        SpawnCreature,
        SummonCopy,
        InnateCard,         //Card appears in heand when game starts
        DrawCards,
        PartnerSummon,      //summon card when another card spawns
        Shoot,              //Shoots a random enemy
        
    }

    public enum UniqueCard
    {
       Common = 0,
       LegendaryBeast,
       LegendaryDragon,
       Spawner


    }
}
