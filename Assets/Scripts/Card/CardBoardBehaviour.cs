using UnityEngine;

/// <summary>
/// ����� ����� ����������� ������ ��������� � ����, ������ ����� �� ����� � �����������.
/// </summary>
public class CardBoardBehaviour : MonoBehaviour
{
    private Creature creature;

    private void Start()
    {
        creature = GetComponent<Creature>();
    }

    public void ChangeTimeCost(int change)
    {
        creature.cardData.TimeCost += change;
        EventBus.OnCardsInfoChanged?.Invoke();
    }
}
