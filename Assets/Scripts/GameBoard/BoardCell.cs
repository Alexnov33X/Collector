using UnityEditor.UI;
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
    public Vector2 cellPosition;
    public bool cellSide = false; //true if player-cell
    private float summonCardAnimation = 0.5f;

    private void Start()
    {
        InitializeCell();
    }

    /// <summary>
    /// �� ������ ��� ������ ������
    /// </summary>
    private void InitializeCell()
    {
        isOccupied = false;
        occupant = null;
        summonCardAnimation = AnimationAndDelays.instance.summonCardAnimation;
    }

    /// <summary>
    /// ������������� ����� ������
    /// </summary>
    /// <param name="card"></param>
    public void SetCardinCell(CardEntity card)
    {
        occupant = card;
        isOccupied = true;
        card.cellHost = this;
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
