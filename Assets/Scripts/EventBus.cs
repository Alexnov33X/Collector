using System;

/// <summary>
/// Шина событий. Здесь нужно хранить все ивенты.
/// </summary>
public static class EventBus
{
    /// <summary>
    /// При изменении количества карт в деке игрока
    /// </summary>
    public static Action OnPlayerDeckCardsChanged;

    /// <summary>
    /// При обновлении статов карте на игровом поле
    /// </summary>
    public static Action OnCardsInfoChanged;
    
    /// <summary>
    /// При инициализации CardData в CardEntity
    /// </summary>
    public static Action OnEntityCardInitialized;

/*    /// <summary>
    /// При изменении состояния карты
    /// </summary>
    public static Action OnCardStateChanged;*/

    /// <summary>
    /// При инициализации CardData в CardEntity
    /// </summary>
    public static Action OnTurnEnded;

}