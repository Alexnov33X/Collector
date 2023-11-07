using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHero : MonoBehaviour
{
    [SerializeField] private PlayerHeroDisplay heroDisplay;
    [SerializeField] private PlayerDamageTaken damageTaken;

    [SerializeField] private int health = 20;
    [SerializeField] private bool mainPlayer = false; //��� ����� ������, ���� ����� ���
    public int Health { get => health; set => health = value; }
    public bool MainPlayer { get => mainPlayer; set => mainPlayer = value; }

    void Start()
    {
        heroDisplay.updateInformation(health.ToString());
    }

    /// <summary>
    /// ��������� ����� ������
    /// ���� �� ���������� <=0 �� ����������� ����
    /// </summary>
    public void OnHit(int damage)
    {
        Health -= damage;
        damageTaken.takeDamage("-"+damage.ToString());
        if (Health <= 0)
        {
            Health = 0;
            heroDisplay.updateInformation(Health.ToString());
            if (MainPlayer)
                EventBus.OnGameVictory?.Invoke();
            else
                EventBus.OnGameLoss?.Invoke();
        }
        heroDisplay.updateInformation(Health.ToString());
    }

}
