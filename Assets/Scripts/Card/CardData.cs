using static Enums;
using UnityEngine;

public class CardData
{
    /// <summary>
    /// Поля класса
    /// </summary>
    private string name;
    private string description;

    private Sprite artworkImage;
    private Sprite rarityImage;
    private Sprite universeImage;

    private int timeCost;
    private int attack;
    private int health;

    private CardType cardType;
    private CardUniverse cardUniverse;
    private CardRarity cardRarity;
    private CardState cardState;

    private int cardId;

    /// <summary>
    /// Getters and Setters
    /// Выполняют функции метод для получения и записи данных в поля класса.
    /// </summary>
    public string Name { get => name; set => name = value; }
    public string Description { get => description; set => description = value; }

    public Sprite ArtworkImage { get => artworkImage; set => artworkImage = value; }
    public Sprite RarityImage { get => rarityImage; set => rarityImage = value; }
    public Sprite UniverseImage { get => universeImage; set => universeImage = value; }

    public int TimeCost { get => timeCost; set => timeCost = value; }
    public int Attack { get => attack; set => attack = value; }
    public int Health { get => health; set => health = value; }

    public CardType CardType { get => cardType; set => cardType = value; }
    public CardUniverse CardUniverse { get => cardUniverse; set => cardUniverse = value; }
    public CardRarity CardRarity { get => cardRarity; set => cardRarity = value; }
    public CardState CardState { get => cardState; set => cardState = value; }

    public int CardId { get => cardId; set => cardId = value; }

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="cardInfo">ScriptableObject Карты</param>
    public CardData(CardScriptableObject cardInfo)
    {
        name = cardInfo.Name;
        description = cardInfo.Description;

        artworkImage = cardInfo.ArtworkImage;
        rarityImage = cardInfo.RarityImage;
        universeImage = cardInfo.UniverseImage;

        timeCost = cardInfo.TimeCost;
        attack = cardInfo.Attack;
        health = cardInfo.Health;

        cardType = cardInfo.CardType;
        cardUniverse = cardInfo.CardUniverse;
        cardRarity = cardInfo.CardRarity;
        cardState = CardState.OnHand;

        cardId = cardInfo.CardId;
    }

    //деструктор. Надо сделать, но позже
}
