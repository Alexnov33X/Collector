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

    private float gameStartDelay = 1f;
    private float delayBetweenPlayerAndEnemy = 2f;

    void Start()
    {
        EventBus.OnGameVictory += Victory; //подписываемся на события окончания игры
        EventBus.OnGameLoss += Loss;
        gameStartDelay = AnimationAndDelays.instance.gameStartDelay;
        delayBetweenPlayerAndEnemy = AnimationAndDelays.instance.delayBetweenPlayerAndEnemy;
        StartCoroutine(Tasks()); //запускаем игровой цикл
    }
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
    /// Но теперь делаем через корутины задержки +
    /// Теперь бой работает автоматически без кнопок, 
    /// правда с фиксированной задержкой.
    /// Это приводит к тому что анимации иногда 
    /// переносятся на следующий ход
    /// </summary>
    public void ExucuteOneTurn()
    {
        StartCoroutine(Tasks());
    }
    private IEnumerator Tasks()
    {
        yield return new WaitForSeconds(gameStartDelay);
        StartingPhase();
        yield return playerHand.ExecuteHandPhases();
        yield return new WaitForSeconds(delayBetweenPlayerAndEnemy);
        yield return enemyHand.ExecuteHandPhases();
        yield return new WaitForSeconds(AnimationAndDelays.instance.delayBetweenTurns);
        EndingPhase();
    }

    private void StartingPhase()
    {

    }

    private void EndingPhase()
    {
        StartCoroutine(Tasks()); //Пока что тут просто перезапускаем игровой цикл
    }

    /// <summary>
    /// Методы победы и поражения. В них мы
    /// останавливаем все корутины, к сожалению код продолжает выполняться
    /// и если не повезёт, то будут запускаться новые корутины которые не остановятся
    /// </summary>
    private void Victory()
    {
        StopAllCoroutines();
        playerHand.StopAllCoroutines();
        enemyHand.StopAllCoroutines();
        vs.EndGame(true);
        turnStep.gameObject.SetActive(false);
    }

    private void Loss()
    {
        StopAllCoroutines();
        playerHand.StopAllCoroutines();
        enemyHand.StopAllCoroutines();
        vs.EndGame(false);
        turnStep.gameObject.SetActive(false);
    }

    public void DrawCardsForPlayer(int amount, bool isPlayer)
    {
        if (isPlayer)
            StartCoroutine(playerHand.DrawCardPhase(amount));
        else
            StartCoroutine(enemyHand.DrawCardPhase(amount));
    }

}
