using static Enums;
using UnityEngine;

public class CardData
{
    #region Class Fields
    /// <summary>
    /// Название карты
    /// </summary>
    private string name;
    /// <summary>
    /// Описание карты
    /// </summary>
    private string description;

    /// <summary>
    /// Спрайт Карты. Арт сущности
    /// </summary>
    private Sprite artworkImage;
    /// <summary>
    /// Спрайт Кристалла Редкости карты
    /// </summary>
    private Sprite rarityImage;
    /// <summary>
    /// Спрайт Вселенной к которой принадлежит персонаж
    /// </summary>
    private Sprite universeImage;

    /// <summary>
    /// Стоимость карты.
    /// Количество ходов, необходимое для розыгрыша карты
    /// </summary>
    private int cardCost;
    /// <summary>
    /// Значение атаки карты
    /// </summary>
    private int attack;
    /// <summary>
    /// Значение здоровья карты
    /// </summary>
    private int health;

    /// <summary>
    /// Тип карты: Существо\Заклинание\Артифакт\Поле
    /// </summary>
    private CardType cardType;
    /// <summary>
    /// Вселенная карты
    /// </summary>
    private CardUniverse cardUniverse;
    /// <summary>
    /// Редкость карты: Common,Rare, Epic,Legendary
    /// </summary>
    private CardRarity cardRarity;
    /// <summary>
    /// Состояние карты: В руке\На доске
    /// </summary>
    private CardState cardState;

    /// <summary>
    /// Id карты. Пока не юзаем, так как не нужен.
    /// </summary>
    private int cardId;
    #endregion

    #region Class Properties
    public string Name { get => name; set => name = value; }
    public string Description { get => description; set => description = value; }

    public Sprite ArtworkImage { get => artworkImage; set => artworkImage = value; }
    public Sprite RarityImage { get => rarityImage; set => rarityImage = value; }
    public Sprite UniverseImage { get => universeImage; set => universeImage = value; }

    public int CardCost { get => cardCost; set => cardCost = value; }
    public int Attack { get => attack; set => attack = value; }
    public int Health { get => health; set => health = value; }

    public CardType CardType { get => cardType; set => cardType = value; }
    public CardUniverse CardUniverse { get => cardUniverse; set => cardUniverse = value; }
    public CardRarity CardRarity { get => cardRarity; set => cardRarity = value; }
    public CardState CardState { get => cardState; set => cardState = value; }

    public int CardId { get => cardId; set => cardId = value; }
    #endregion

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

        cardCost = cardInfo.CardCost;
        attack = cardInfo.Attack;
        health = cardInfo.Health;

        cardType = cardInfo.CardType;
        cardUniverse = cardInfo.CardUniverse;
        cardRarity = cardInfo.CardRarity;
        cardState = CardState.OnHand;

        cardId = cardInfo.CardId;
    }

    public void PrintCardData()
    {
        Debug.Log(
            "CD_PCD: \t " +
            "name:" + name + "\t " +
            "description:" + description + "\t " +
            "artworkImage:" + artworkImage + "\t " +
            "rarityImage:" + rarityImage + "\t " +
            "universeImage:" + universeImage + "\t " +
            "cardCost:" + cardCost + "\t " +
            "attack:" + attack + "\t " +
            "health:" + health + "\t " +
            "cardType:" + cardType + "\t " +
            "cardUniverse:" + cardUniverse + "\t " +
            "cardRarity:" + cardRarity + "\t " +
            "cardState:" + cardState + "\t " +
            "cardId:" + cardId
            );
    }
}
