using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

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
    public VictoryScreen vs;
    public Button turnStep;

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
    void Start()
    {
        EventBus.OnGameVictory += Victory;
        EventBus.OnGameLoss += Loss;
        StartCoroutine(Tasks());
    }
    public void ExucuteOneTurn()
    {
        StartCoroutine(Tasks());
    }
    private IEnumerator Tasks()
    {
        yield return new WaitForSeconds(1);
        StartingPhase();
        playerHand.ExecuteHandPhases();
        yield return new WaitForSeconds(2);
        enemyHand.ExecuteHandPhases();
        yield return new WaitForSeconds(1);
        EndingPhase();
    }

    private void StartingPhase()
    {

    }

    private void EndingPhase()
    {
        StartCoroutine(Tasks());
    }

    private void Victory()
    {
        StopAllCoroutines();
        vs.EndGame(true);
        turnStep.gameObject.SetActive(false);

    }

    private void Loss()
    {
        StopAllCoroutines();
        vs.EndGame(false);
        turnStep.gameObject.SetActive(false);
    }

}
