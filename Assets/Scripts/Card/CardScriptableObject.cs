using UnityEngine;
using static Enums;

[CreateAssetMenu(fileName = "New Card", menuName = "Card")]
public class CardScriptableObject : ScriptableObject
{
    public string Name;
 
    [TextArea]
    public string Description;

    public Sprite ArtworkImage;
    public Sprite RarityImage;
    public Sprite UniverseImage;

    public int TimeCost = 1;
    public int Attack;
    public int Health;

    public CardType CardType;
    public CardUniverse CardUniverse;
    public CardRarity CardRarity;

    public int CardId;

    public void Print()
    {
        Debug.Log(Name + ": " + Description + " The card costs: " + TimeCost);
    }
}
