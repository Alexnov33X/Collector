using UnityEngine;
using static Enums;

[CreateAssetMenu(fileName = "New Card", menuName = "Card")]
public class CardScriptableObject : ScriptableObject
{
    [Tooltip("Название карты")]
    public string Name;
    [Tooltip("Описание карты"), TextArea]
    public string Description;

    [Tooltip("Спрайт Карты. Арт сущности")]
    public Sprite ArtworkImage;
    [Tooltip("Спрайт Кристалла Редкости карты")]
    public Sprite RarityImage;
    [Tooltip("Спрайт Вселенной к которой принадлежит персонаж")]
    public Sprite UniverseImage;

    [Tooltip("Стоимость карты.\r\n Количество ходов, необходимое для розыгрыша карты")]
    public int CardCost;
    [Tooltip("Значение атаки карты")]
    public int Attack;
    [Tooltip("Значение здоровья карты")]
    public int Health;

    [Tooltip("Тип карты: Существо\\Заклинание\\Артифакт\\Поле")]
    public CardType CardType;
    [Tooltip("Вселенная карты")]
    public CardUniverse CardUniverse;
    [Tooltip("Редкость карты: Common,Rare, Epic,Legendary")]
    public CardRarity CardRarity;

    [Tooltip("Id карты. Пока не юзаем, так как не нужен.")]
    public int CardId;

    public void Print()
    {
        Debug.Log(Name + ": " + Description + " The card costs: " + CardCost);
    }
}
