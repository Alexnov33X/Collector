using UnityEngine;

public class BoardCell : MonoBehaviour
{
    /// <summary>
    /// Занята клетка или нет
    /// </summary>
    [HideInInspector] public bool isOccupied;
    /// <summary>
    /// Карта, которая занимает клетку
    /// </summary>
    [HideInInspector] public CardEntity occupant;

    private void Start()
    {
        InitializeCell();
    }

    /// <summary>
    /// На старте все клетки пустые
    /// </summary>
    private void InitializeCell()
    {
        isOccupied = false;
        occupant = null;
    }

    /// <summary>
    /// Устанавливает карту клетку
    /// </summary>
    /// <param name="card"></param>
    public void SetCardinCell(CardEntity card)
    {
        occupant = card;
        isOccupied = true;
        
        SetCardCellTransform(card);
    }

    /// <summary>
    /// Устанавливает Карте позицию клетки
    /// </summary>
    /// <param name="card"></param>
    private void SetCardCellTransform(CardEntity card)
    {
        card.ChangeCardState();
        LeanTween.move(card.gameObject, gameObject.transform.position, 0.5f).setEase(LeanTweenType.easeInSine);
        // Прикрепляем теперь карту к доске, чтобы она перекрывала игрыове ячейки
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
