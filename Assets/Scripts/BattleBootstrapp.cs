using UnityEngine;

/// <summary>
/// Точка входа. Здесь можно четко прописать порядок инициализации всех объектов.(Не обязветельно всех, но желательно)
/// </summary>
public class BattleBootstrapp : MonoBehaviour
{
    void Awake()
    {
        //Загружаем инфу с воображаемого сервера(В будущем вынесем в MainBootstrapp)
        PlayerStats.LoadPlayerData();

        //Инициализируем боевую колоду игрока
        PlayerBattleDeck.BattleDeck = PlayerDecks.CurrentDeck;
    }
}
