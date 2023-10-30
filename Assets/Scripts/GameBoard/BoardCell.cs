using UnityEngine;

public class BoardCell : MonoBehaviour
{
    /// <summary>
    /// ������ ������ ��� ���
    /// </summary>
    [HideInInspector] public bool isOccupied;
    /// <summary>
    /// �����, ������� �������� ������
    /// </summary>
    [HideInInspector] public CardEntity occupant;
    private float summonCardAnimation = 0.5f;

    private void Start()
    {
        summonCardAnimation = AnimationAndDelays.instance.summonCardAnimation;
        InitializeCell();
    }

    /// <summary>
    /// �� ������ ��� ������ ������
    /// </summary>
    private void InitializeCell()
    {
        isOccupied = false;
        occupant = null;
    }

    /// <summary>
    /// ������������� ����� ������
    /// </summary>
    /// <param name="card"></param>
    public void SetCardinCell(CardEntity card)
    {
        occupant = card;
        isOccupied = true;

        SetCardCellTransform(card);
    }

    /// <summary>
    /// ������������� ����� ������� ������
    /// </summary>
    /// <param name="card"></param>
    private void SetCardCellTransform(CardEntity card)
    {
        card.ChangeCardState();
        LeanTween.move(card.gameObject, gameObject.transform.position, summonCardAnimation).setEase(LeanTweenType.easeInSine);
        // ����������� ������ ����� � �����, ����� ��� ����������� ������� ������
        //card.gameObject.transform.SetParent(gameObject.transform); 
        card.gameObject.transform.position = card.gameObject.transform.position;
    }

    public void DestroyCardinCell()
    {
        Destroy(occupant.gameObject);
        occupant = null;
        isOccupied = false;
    }
}
