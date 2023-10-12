using UnityEngine;

public class GameBoardView : MonoBehaviour
{
    [Header("Slots")]
    public Transform[] playerCardSlots;

    [Header("")]
    public Entity[] occupants;

    public void AddEntity(Entity e, int index)
    {
        occupants[index] = e;
        return;
    }
}