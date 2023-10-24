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
        Creature,
        Spell,
        Artifact,
        Field
    }

    public enum CardRarity
    {
        Common,
        Rare,
        Epic,
        Legendary
    }

    public enum CardUniverse
    {
        Dragons1,
        Dragons2,
        Dragons3
    }

    /// <summary>
    /// Состояние карты на игровом поле. В руке\На доске.
    /// </summary>
    public enum CardState
    {
        OnHand,
        OnBoard
    }

    public enum CardAbility
    {
        DefaultVerticalLinearAttack,
        DefaultHorizontalLinearAttack,
        EveryoneAttack,
    }
}
