using UnityEngine;

/// <summary>
/// ����� �����. ����� ����� ����� ��������� ������� ������������� ���� ��������.(�� ������������ ����, �� ����������)
/// </summary>
public class Bootstrapp : MonoBehaviour
{
    void Awake()
    {
        PlayerStats.LoadPlayerData();       
    }
}
