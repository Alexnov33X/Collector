using System.Collections.Generic;
using UnityEngine;
using static Enums;

[CreateAssetMenu(fileName = "New Card", menuName = "Card")]
public class CardScriptableObject : ScriptableObject
{
    [Header("Name and Description")]
    [Tooltip("�������� �����")]
    public string Name;
    [Tooltip("�������� �����"), TextArea]
    public string Description;

    [Header("Sprite Elements")]
    [Tooltip("������ ����� � ����")]
    public Sprite ArtworkHandImage;
    [Tooltip("������ ����� �� �����")]
    public Sprite ArtworkBoardImage;
    [Tooltip("������ ��������� �������� �����")]
    public Sprite RarityImage;
    [Tooltip("������ ��������� � ������� ����������� ��������")]
    public Sprite UniverseImage;

    [Header("Card Stats")]
    [Tooltip("��������� �����.\r\n ���������� �����, ����������� ��� ��������� �����")]
    public int CardCost;
    [Tooltip("�������� ����� �����")]
    public int Attack;
    [Tooltip("�������� �������� �����")]
    public int Health;

    [Header("Card Types")]
    [Tooltip("��� �����: ��������\\����������\\��������\\����")]
    public CardType CardType;
    [Tooltip("��������� �����")]
    public CardUniverse CardUniverse;
    [Tooltip("�������� �����: Common,Rare, Epic,Legendary")]
    public CardRarity CardRarity;

    [Header("Abilities")]
    [Tooltip("����������� � ���� �������. �������� ������ � 3 -> ������ �� 3 ����. ���� �� ���������� �� ��������")]
    public List<CardAbility> abilities = new List<CardAbility>();
    public List<int> abilityPotency = new List<int>();

    [Header("Card ID")]
    [Tooltip("Id �����. ���� �� �����, ��� ��� �� �����.")]
    public int CardId;
    public void Print()
    {
        Debug.Log(Name + ": " + Description + " The card costs: " + CardCost);
    }
}
