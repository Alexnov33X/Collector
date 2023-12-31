﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

/// <summary>
/// ///
/// Выполняет роль передатчика ходов.///Является точкой где храниться порядок фаз хода
/// /// Выполняет фазы хода:
/// /// - Фаза начало хода
/// /// - Фаза конца хода
/// /// </summary>
public class TurnTransmitter : MonoBehaviour
{
    public PlayerHand playerHand;
    public PlayerHand enemyHand;
    public VictoryScreen vs;
    public Button turnStep;
    public bool startImmediately = false;
    private float gameStartDelay = 1f;
    private float delayBetweenPlayerAndEnemy = 2f;

    void Start()
    {
        EventBus.OnGameOver += BattleResult;
        gameStartDelay = AnimationAndDelays.instance.gameStartDelay;
        delayBetweenPlayerAndEnemy = AnimationAndDelays.instance.delayBetweenPlayerAndEnemy;
        if (startImmediately)
            StartCoroutine(Tasks()); //запускаем игровой цикл
    }

    public void StartTheGame()
    {
        StartCoroutine(Tasks());
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
    private void BattleResult(bool isVictory)
    {
        StopAllCoroutines();
        playerHand.StopAllCoroutines();
        enemyHand.StopAllCoroutines();
        vs.EndGame(isVictory);
    }
    public void DrawCardsForPlayer(int amount, bool isPlayer)
    {
        if (isPlayer)
            StartCoroutine(playerHand.DrawCardPhase(amount));
        else
            StartCoroutine(enemyHand.DrawCardPhase(amount));
    }
    public void CallPartnerInHandsAndDeck(bool isPlayer)
    {
        if (isPlayer)
        {
            //List<CardEntity> partner = new List<CardEntity>();
            //foreach (CardEntity card in playerHand.handList)
            //    if (card.cardData.abilityAndStatus.ContainsKey(Enums.CardAbility.PartnerSummon))
            //        card.cardData.CardCost = 0;

            playerHand.DrawAndSummonPartners();

        }
        else
        {
            //List<CardEntity> partner = new List<CardEntity>();
            //foreach (CardEntity card in enemyHand.handList)
            //    if (card.cardData.abilityAndStatus.ContainsKey(Enums.CardAbility.PartnerSummon))
            //        card.cardData.CardCost = 0;

            enemyHand.DrawAndSummonPartners();
        }
    }
    private void DrawInnateCards() //This logic moved to PlayerHand
    {
        var innateCards = PlayerBattleDeck.BattleDeck.FindAll(x => x.abilities.Contains(Enums.CardAbility.InnateCard));
        foreach (var card in innateCards)
        {
            StartCoroutine(playerHand.AddDefiniteCardToHand(card, false));
            PlayerBattleDeck.BattleDeck.Remove(card);
        }
        innateCards = PlayerBattleDeck.EnemyBattleDeck.FindAll(x => x.abilities.Contains(Enums.CardAbility.InnateCard));
        foreach (var card in innateCards)
        {
            StartCoroutine(enemyHand.AddDefiniteCardToHand(card, false));
            PlayerBattleDeck.BattleDeck.Remove(card);
        }

    }
}