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
}