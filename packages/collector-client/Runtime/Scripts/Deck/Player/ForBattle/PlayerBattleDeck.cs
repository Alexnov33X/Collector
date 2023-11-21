using System.Collections.Generic;

/// <summary>
/// Дествующая колода игрока, которая используется для боевки. Инициализируется при начале боя(при загрузке сцены BattleScene) и 
/// используется только для боя. Инициализация происходит в BattleBootstrapp.
/// </summary>
public static class PlayerBattleDeck
{
    private static List<CardScriptableObject> battleDeck = new List<CardScriptableObject>();
    public static List<CardScriptableObject> BattleDeck { get { return battleDeck; } set => battleDeck = value; }

    private static List<CardScriptableObject> enemyBattleDeck = new List<CardScriptableObject>(); //Костылим для колоды второго игрока
    public static List<CardScriptableObject> EnemyBattleDeck { get { return enemyBattleDeck; } set => enemyBattleDeck = value; }

}

