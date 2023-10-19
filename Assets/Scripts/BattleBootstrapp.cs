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
        PlayerBattleDeck.BattleDeck = PlayerDecks.CurrentDeck;
    }
}
