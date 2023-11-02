using System;

/// <summary>
/// Шина событий. Здесь нужно хранить все ивенты.
/// </summary>
public static class EventBus
{
    /// <summary>
    /// При изменении количества карт в деке игрока
    /// </summary>
    public static Action OnPlayerBatttleDeckAmountChanged;

    /// <summary>
    /// При обновлении статов карте на игровом поле
    /// </summary>
    public static Action OnCardsInfoChanged;

    /// <summary>
    /// При инициализации CardData в CardEntity
    /// </summary>
    public static Action<bool> OnEntityCardInitialized;

    /// <summary>
    /// При инициализации CardData в CardEntity
    /// </summary>
    public static Action OnCardStateChanged;

    /// <summary>
    /// При завершении хода
    /// </summary>
    public static Action OnTurnEnded;

    /// <summary>
    /// При победе
    /// </summary>
    public static Action OnGameVictory;

    /// <summary>
    /// При поражении
    /// </summary>
    public static Action OnGameLoss;

}