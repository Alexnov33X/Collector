using UnityEngine;
using static Enums;

[CreateAssetMenu(fileName = "New Card", menuName = "Card")]
public class CardInfo : ScriptableObject
{
    public string Name;
    [TextArea]
    public string Description;

    public Sprite Artwork;
    public Sprite Rarity;
    public Sprite Universe;

    public int TimeCost = 1;
    public int Attack;
    public int Health;

    public CardType CardType;
    public CardAffinity CardAffinity;
    public CardRarity CardRarity;

    public int CardId;

    public void Print()
    {
        Debug.Log(Name + ": " + Description + " The card costs: " + TimeCost);
    }
}
