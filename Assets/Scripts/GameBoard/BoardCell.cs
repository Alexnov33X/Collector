using System.Collections;
using UnityEditor.UI;
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
    public Vector2 cellPosition;
    public bool cellSide = false; //true if player-cell
    private float summonCardAnimation = 0.5f;

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
        summonCardAnimation = AnimationAndDelays.instance.summonCardAnimation;
    }

    /// <summary>
    /// Устанавливает карту клетку
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
    /// Устанавливает Карте позицию клетки
    /// </summary>
    /// <param name="card"></param>
    private void SetCardCellTransform(CardEntity card)
    {
        card.ChangeCardState();
        LeanTween.move(card.gameObject, gameObject.transform.position, summonCardAnimation).setEase(LeanTweenType.easeInSine);
        // Прикрепляем теперь карту к доске, чтобы она перекрывала игрыове ячейки
        //card.gameObject.transform.SetParent(gameObject.transform); 
        card.gameObject.transform.position = card.gameObject.transform.position;
    }

    public void DestroyCardinCell()
    {
        if (occupant.isEnemyEntity)
            FindObjectOfType<GameBoardRegulator>().EnemyUnitsCount--;
        else
            FindObjectOfType<GameBoardRegulator>().PlayerUnitsCount--;

        StartCoroutine(DeathAnimation(occupant.gameObject));
        occupant = null;
        isOccupied = false;

    }

    private IEnumerator DeathAnimation(GameObject creature)
    {
        //LeanTween.shake()
        LeanTween.scale(occupant.gameObject, new Vector3(0.5f, 0.5f, 0.5f), AnimationAndDelays.instance.deathAnimation).setEaseShake();
        yield return new WaitForSeconds(AnimationAndDelays.instance.deathAnimation);
        Destroy(creature);
    }

}
