using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Выполняет роль передатчика ходов. Является точкой где храниться порядок фаз хода
/// Выполняет фазы хода:
/// - Фаза начало хода
/// - Фаза конца хода
/// </summary>
public class TurnTransmitter : MonoBehaviour
{
    public PlayerHand playerHand;
    public PlayerHand enemyHand;

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
    //public void ExucuteOneTurn()
    //{
    //    StartingPhase();
    //    playerHand.ExecuteHandPhases();
    //    //Фаза атаки(еще нет боевой системы)
    //    EndingPhase();
    //}
    public void ExucuteOneTurn()
    {
        StartCoroutine(Tasks());
    }
    private IEnumerator Tasks()
    {
        StartingPhase();
        playerHand.ExecuteHandPhases();
        yield return new WaitForSeconds(2.0f); //добавляем задержку в 2 секунды между ходом игрока и оппонента
        enemyHand.ExecuteHandPhases();
        EndingPhase();
    }

    private void StartingPhase()
    {

    }

    private void EndingPhase()
    {

    }

}
