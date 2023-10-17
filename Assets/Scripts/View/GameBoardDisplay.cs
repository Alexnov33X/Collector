using UnityEngine;

public class GameBoardView : MonoBehaviour
{
    [Header("Slots")]
    public Transform[] playerCardSlots;
    public Player mainPlayer;
    public Player enemyPlayer;

    [Header("")]
    public Entity[] occupants;
    int playerEntities = 0;
    int enemyEntities = 0;
    int playerSlots = 6;
    int enemySlots = 6;

    [Header("For Testing")]
    public Entity ent;
    public Entity ent2;
    public GameObject EPrefab;
    public GameObject entityPrefab;

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
            GameObject newEntity = Instantiate(entityPrefab);
            Entity update = newEntity.GetComponent<Entity>();
            update.transform.SetParent(transform);
            update.transform.localScale = new Vector3(1, 1, 1);
            update.health = e.health;
            update.artwork = e.artwork;
            update.attack = e.attack;
            int i = Random.Range(0, playerSlots);
            while (occupants[i] != null)
                i = Random.Range(0, playerSlots);
            occupants[i] = update;
            update.transform.position = playerCardSlots[i].position;
            playerEntities++;
        }
        else if (!isPlayer && enemyEntities < 6)
        {
            GameObject newEntity = Instantiate(entityPrefab);
            Entity update = newEntity.GetComponent<Entity>();
            update.transform.SetParent(transform);
            update.transform.localScale = new Vector3(1, 1, 1);
            update.health = e.health;
            update.artwork = e.artwork;
            update.attack = e.attack;
            int i = Random.Range(playerSlots, playerSlots + enemySlots);
            while (occupants[i] != null)
                i = Random.Range(playerSlots, playerSlots + enemySlots);
            occupants[i] = update;
            update.transform.position = playerCardSlots[i].position;
            enemyEntities++;
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
    private void Start() // для тестов
    {
        //Debug.Log(occupants[0]);
        //Debug.Log(occupants[0] == null);
        // AddEntityToPlayerSide(ent, false);
        // AddEntityToPlayerSide(ent2, true);
    }
}

