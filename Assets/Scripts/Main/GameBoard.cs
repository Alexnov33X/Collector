using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBoard : MonoBehaviour
{
    [Header("Slots")]
    public Transform[] cardSlots;

    public Entity[] occupants;

    public void AddEntity(Entity e, int index)
    {
        occupants[index] = e;
        return;
    }
}
