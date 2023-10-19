using System.Collections.Generic;

/// <summary>
/// ��������. ������ � ���� ������ � ������ � ����� ������. ������������ ���� � ������ ��� ����������� �������� �����.
/// </summary>
public static class PlayerDecks
{
    /// <summary>
    /// ���������, �� ��������:
    /// ������������ ���-�� ����
    /// � ������ ����� ���� �� ����� 1 ����������� ����� �� 1 ���������
    /// ... � �.�.
    /// </summary>
    #region Consts
    private const int maxCardNumber = 14;
    #endregion

    /// <summary>
    /// ����������� ������ ������.
    /// �� ������������ ��� ���
    /// </summary>
    private static List<CardScriptableObject> currentDeck = new List<CardScriptableObject>();
    public static List<CardScriptableObject> CurrentDeck { get { return currentDeck; } set => currentDeck = value; }

    /// <summary>
    /// ���������� ����� � ������
    /// </summary>
    public static void AddCard(CardScriptableObject card) 
    {
        ValidateCardAddition(card);
    }

    /// <summary>
    /// �������� ����� �� ������
    /// </summary>
    public static void RemoveCard()
    {

    }

    /// <summary>
    /// 
    /// </summary>
    private static bool ValidateCardAddition(CardScriptableObject cardToValidate)
    {
        //����� ��������� ����� �� �������� ��� ����� � ����, �������� ���� ����������� �������� �����������
        return true;
    }

    //������ ����������� ������� �����������(� ���� ����� ������ ����� ���������� bool true, ���� ����� ������������ ����������� ������� � ��������)
    
}
