using UnityEngine;

/// <summary>
/// Выполняет роль передатчика ходов. Является точкой где храниться порядок фаз хода
/// Выполняет фазы хода:
/// - Фаза начало хода
/// - Фаза конца хода
/// </summary>
public class TurnTransmitter : MonoBehaviour
{
    public PlayerHand playerHand;

    /// <summary>
    /// Выполняет все фазы одного хода. 
    /// Порядок фаз:
    /// - Фаза начало хода
    /// - Фазы Руки:
    ///     -- Фаза снижения стоимости
    ///     -- Фаза выдачи карты
    ///     -- Фаза призыва
    /// - Фаза атаки
    /// - Фаза конца хода
    /// </summary>
    public void ExucuteOneTurn()
    {
        StartingPhase();
        playerHand.ExecuteHandPhases();
        //Фаза атаки(еще нет боевой системы)
        EndingPhase();
    }

    private void StartingPhase()
    {

    }

    private void EndingPhase()
    {

    }
}
