using UnityEngine;

public class GameBoardDisplay : MonoBehaviour
{
    [Header("Slots")]
    public Transform[] playerCardSlots;

    [Header("")]
    public CardEntity[] occupants;
    int playerEntities = 0;
    int enemyEntities = 0;
    int playerSlots = 6;
    int enemySlots = 6;

    [Header("For Testing")]
    public CardEntity ent;
    public CardEntity ent2;
    public GameObject EPrefab;

    public Player p;
    
    private void Start() // для тестов
    {
        //Debug.Log(occupants[0]);
        //Debug.Log(occupants[0] == null);
        //AddEntityToPlayerSide(ent, false);
        //AddEntityToPlayerSide(ent2, true);
    }
    
    public void AddEntityToPosition(CardEntity e, int index)
    {
        occupants[index] = e;
        playerEntities++;
        return;
    }

    public void AddEntityToPlayerSide(CardEntity e, bool isPlayer) //случайно выбираем не занятую позицию и вставляет туда существо
    {
        if (isPlayer && playerEntities < 6)
        {
            int i = Random.Range(0, playerSlots);
            while (occupants[i] != null)
                i = Random.Range(0, playerSlots);
            occupants[i] = e;
            e.transform.position = playerCardSlots[i].position;
        }
        else if (!isPlayer && enemyEntities < 6)
        {
            int i = Random.Range(playerSlots, playerSlots + enemySlots);
            while (occupants[i] != null)
                i = Random.Range(playerSlots, playerSlots + enemySlots);
            occupants[i] = e;
            e.transform.position = playerCardSlots[i].position;
        }
    }
    public void OrderAttack(bool isPlayer) //проходим по всем сущностям и говорим им атаковать
    {
        if (isPlayer)
        {
            for (int i = 0; i < playerSlots; i++)
                if (occupants[i] != null)
                    occupants[i].Attack(this, i);
        }
        else
        {
            for (int i = playerSlots; i < playerSlots + enemySlots; i++)
                if (occupants[i] != null)
                    occupants[i].Attack(this, i);
        }
    }
    
}

