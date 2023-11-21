using System.Collections.Generic;
using UnityEngine;
using static Enums;

[CreateAssetMenu(fileName = "New Card", menuName = "Card")]
public class CardScriptableObject : ScriptableObject
{
    [Header("Name and Description")]
    [Tooltip("Название карты")]
    public string Name;
    [Tooltip("Описание карты"), TextArea]
    public string Description;

    [Header("Sprite Elements")]
    [Tooltip("Спрайт Карты в Руке")]
    public Sprite ArtworkHandImage;
    [Tooltip("Спрайт Карты на Доске")]
    public Sprite ArtworkBoardImage;
    [Tooltip("Спрайт Кристалла Редкости карты")]
    public Sprite RarityImage;
    [Tooltip("Спрайт Вселенной к которой принадлежит персонаж")]
    public Sprite UniverseImage;

    [Header("Card Stats")]
    [Tooltip("Стоимость карты.\r\n Количество ходов, необходимое для розыгрыша карты")]
    public int CardCost;
    [Tooltip("Значение атаки карты")]
    public int Attack;
    [Tooltip("Значение здоровья карты")]
    public int Health;

    [Header("Card Types")]
    [Tooltip("Тип карты: Существо\\Заклинание\\Артифакт\\Поле")]
    public CardType CardType;
    [Tooltip("Вселенная карты")]
    public CardUniverse CardUniverse;
    [Tooltip("Редкость карты: Common,Rare, Epic,Legendary")]
    public CardRarity CardRarity;

    [Header("Abilities")]
    [Tooltip("Способности и сила эффекта. Например поджог и 3 -> поджог на 3 хода. Надо их соотносить по индексам")]
    public List<CardAbility> abilities = new List<CardAbility>();
    public List<int> abilityPotency = new List<int>();

    [Header("Card ID")]
    [Tooltip("Id карты. Пока не юзаем, так как не нужен.")]
    public int CardId;
    [Header("Spawner")]
    [Tooltip("Put spawnable creature here")]
    public CardScriptableObject creatureToSpawn;
    public void Print()
    {
        Debug.Log(Name + ": " + Description + " The card costs: " + CardCost);
    }
}
