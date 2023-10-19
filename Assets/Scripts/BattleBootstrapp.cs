using UnityEngine;

/// <summary>
/// ����� �����. ����� ����� ����� ��������� ������� ������������� ���� ��������.(�� ������������ ����, �� ����������)
/// </summary>
public class BattleBootstrapp : MonoBehaviour
{
    void Awake()
    {
        //��������� ���� � ������������� �������(� ������� ������� � MainBootstrapp)
        PlayerStats.LoadPlayerData();

        //�������������� ������ ������ ������
        PlayerDeck.BattleDeck = PlayerDeck.CurrentDeck;
    }
}
