using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBoard : MonoBehaviour
{
    [Header("Slots")]
    public Transform[] cardSlots;

    public Entity[] occupants;
    int playerEntities = 0;
    int enemyEntities = 0;
    int playerSlots = 6;
    int enemySlots = 6;

    public Entity ent;
    public GameObject EPrefab;

    public void AddEntityToPosition(Entity e, int index)
    {
        occupants[index] = e;
        playerEntities++;
        return;
    }
    public void AddEntityToPlayerSide(Entity e, bool isPlayer) //случайно выбираем не занятую позицию и вставляет туда существо
    {
        if (isPlayer && playerEntities < 6)
        {
            int i = Random.Range(0, playerSlots);
            while (occupants[i] != null)
                i = Random.Range(0, playerSlots);
            occupants[i] = e;
            e.transform.position = cardSlots[i].position;
        }
        else if (!isPlayer && enemyEntities < 6)
        {
            int i = Random.Range(playerSlots, playerSlots + enemySlots);
            while (occupants[i] != null)
                i = Random.Range(playerSlots, playerSlots + enemySlots);
            occupants[i] = e;
            e.transform.position = cardSlots[i].position;
        }
    }
    private void Start()
    {
        Debug.Log(occupants[0]);
        Debug.Log(occupants[0] == null);
        AddEntityToPlayerSide(ent, false);
    }
}
