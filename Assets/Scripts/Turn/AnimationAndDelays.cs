using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationAndDelays : MonoBehaviour
{
    public static AnimationAndDelays instance = null;
    [Header("����� ����� �������� ��������� ��������� �����")]
    [SerializeField]
    public float cardCostChangeAnimation = 0.25f;
    [Header("����� ����� �������� ����� ����")]
    [SerializeField]
    public float attackAnimation = 0.8f;
    [Header("�������� ����� ������ ����� � ����")]
    [SerializeField]
    public float gameStartDelay = 1f;
    [Header("�������� �������� ���� � �����")]
    [SerializeField]
    public float delayBetweenPlayerAndEnemy = 2f;
    [Header("�������� ������ ����� (��� ��������, ������ ��������)")]
    [SerializeField]
    public float cardReceiveDelay = 1f;
    [Header("�������� ����� ������� ������� ����")]
    [SerializeField]
    public float delayBeforeSummon = 1f;
    [Header("�������� ������� ����")]
    [SerializeField]
    public float summonCardAnimation = 0.5f;
    [Header("�������� ����� ������� �� ��������� ��� (����� ��� ������ ��������)")]
    [SerializeField]
    public float delayBetweenTurns = 2f;
    void Start()
    {
        // ������, ��������� ������������� ����������
        if (instance == null)
        { // ��������� ��������� ��� ������
            instance = this; // ������ ������ �� ��������� �������
        }
        else if (instance == this)
        { // ��������� ������� ��� ���������� �� �����
            Destroy(gameObject); // ������� ������
        }
    }
}
