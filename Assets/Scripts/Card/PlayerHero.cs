using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHero : MonoBehaviour
{
    public PlayerHeroDisplay pd;

    [SerializeField] private int health = 90;
    [SerializeField] private bool mainPlayer = false; //��� ����� ������, ���� ����� ���
    public int Health { get => health; set => health = value; }
    public bool MainPlayer { get => mainPlayer; set => mainPlayer = value; }

    void Start()
    {
        pd.updateInformation(health.ToString());
    }

    /// <summary>
    /// ��������� ����� ������
    /// ���� �� ���������� <=0 �� ����������� ����
    /// </summary>
    public void OnHit(int damage)
    {
        Health -= damage;
        if (Health < 0)
        {
            Health = 0;
            pd.updateInformation(Health.ToString());
            if (MainPlayer)
                EventBus.OnGameVictory?.Invoke();
            else
                EventBus.OnGameLoss?.Invoke();
        }
        pd.updateInformation(Health.ToString());
    }

}
