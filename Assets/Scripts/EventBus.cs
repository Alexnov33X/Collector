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
}