using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ����� ����� ��� ������� ����� BattleScene. 
/// ����� ����� ����� ��������� ������� ������������� ���� ��������.(�� ����������� ����, �� ����������)
/// </summary>
public class BattleBootstrapp : MonoBehaviour
{
    void Awake()
    {
        //��������� ���� � ������������� �������(� ������� ������� � MainBootstrapp)
        PlayerStats.LoadPlayerData();
    }

}
