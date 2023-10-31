using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationAndDelays : MonoBehaviour
{
    public static AnimationAndDelays instance = null;
    [Header("Общее время анимации изменения стоимости карты")]
    [SerializeField]
    public float cardCostChangeAnimation = 0.25f;
    [Header("Общее время анимации атаки карт")]
    [SerializeField]
    public float attackAnimation = 0.8f;
    [Header("Задержка перед первым ходом в игре")]
    [SerializeField]
    public float gameStartDelay = 1f;
    [Header("Задержка перехода хода к врагу")]
    [SerializeField]
    public float delayBetweenPlayerAndEnemy = 2f;
    [Header("Задержка взятия карты (без анимации, просто задержка)")]
    [SerializeField]
    public float cardReceiveDelay = 1f;
    [Header("Задержка перед началом призыва карт")]
    [SerializeField]
    public float delayBeforeSummon = 1f;
    [Header("Анимация призыва карт")]
    [SerializeField]
    public float summonCardAnimation = 0.5f;
    [Header("Задержка между переход на следующий ход (когда оба игрока походили)")]
    [SerializeField]
    public float delayBetweenTurns = 2f;
    [Header("Время анимации взятия карты")]
    [SerializeField]
    public float drawingCardAnimation = 1f;
    void Start()
    {
    }
    private void Awake()
    {
        // Теперь, проверяем существование экземпляра
        if (instance == null)
        { // Экземпляр менеджера был найден
            instance = this; // Задаем ссылку на экземпляр объекта
        }
        else if (instance == this)
        { // Экземпляр объекта уже существует на сцене
            Destroy(gameObject); // Удаляем объект
        }
    }
}
