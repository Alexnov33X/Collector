using System.Collections.Generic;

/// <summary>
/// ƒествующа€ колода игрока, котора€ используетс€ дл€ боевки. »нициализируетс€ при начале бо€(при загрузке сцены BattleScene) и 
/// используетс€ только дл€ бо€. »нициализаци€ происходит в BattleBootstrapp.
/// </summary>
public static class PlayerBattleDeck
{
    private static List<CardScriptableObject> battleDeck = new List<CardScriptableObject>();
    public static List<CardScriptableObject> BattleDeck { get { return battleDeck; } set => battleDeck = value; }

    private static List<CardScriptableObject> enemyBattleDeck = new List<CardScriptableObject>(); // остылим дл€ колоды второго игрока
    public static List<CardScriptableObject> EnemyBattleDeck { get { return enemyBattleDeck; } set => enemyBattleDeck = value; }
}
