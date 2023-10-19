using UnityEngine;
using static Enums;

[CreateAssetMenu(fileName = "New Card", menuName = "Card")]
public class CardScriptableObject : ScriptableObject
{
    [Tooltip("�������� �����")]
    public string Name;
    [Tooltip("�������� �����"), TextArea]
    public string Description;

    [Tooltip("������ �����. ��� ��������")]
    public Sprite ArtworkImage;
    [Tooltip("������ ��������� �������� �����")]
    public Sprite RarityImage;
    [Tooltip("������ ��������� � ������� ����������� ��������")]
    public Sprite UniverseImage;

    [Tooltip("��������� �����.\r\n ���������� �����, ����������� ��� ��������� �����")]
    public int CardCost;
    [Tooltip("�������� ����� �����")]
    public int Attack;
    [Tooltip("�������� �������� �����")]
    public int Health;

    [Tooltip("��� �����: ��������\\����������\\��������\\����")]
    public CardType CardType;
    [Tooltip("��������� �����")]
    public CardUniverse CardUniverse;
    [Tooltip("�������� �����: Common,Rare, Epic,Legendary")]
    public CardRarity CardRarity;

    [Tooltip("Id �����. ���� �� �����, ��� ��� �� �����.")]
    public int CardId;

    public void Print()
    {
        Debug.Log(Name + ": " + Description + " The card costs: " + CardCost);
    }
}
