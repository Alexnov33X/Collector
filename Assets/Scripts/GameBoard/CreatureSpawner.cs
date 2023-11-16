using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureSpawner : MonoBehaviour
{
    public GameObject CreaturePrefab;
    public List<CardScriptableObject> creaturesList;
    Dictionary<string, CardScriptableObject> creatures = new Dictionary<string, CardScriptableObject>();
    public static CreatureSpawner instance = null;
    private GameBoardRegulator gameBoardRegulator;

    void Awake()
    {
        // “еперь, провер€ем существование экземпл€ра
        if (instance == null)
        { // Ёкземпл€р менеджера был найден
            instance = this; // «адаем ссылку на экземпл€р объекта
        }
        else if (instance == this)
        { // Ёкземпл€р объекта уже существует на сцене
            Destroy(gameObject); // ”дал€ем объект
        }
        gameBoardRegulator = GetComponent<GameBoardRegulator>();
        creatures = new Dictionary<string, CardScriptableObject>();
        foreach (CardScriptableObject so in creaturesList)
            creatures.Add(so.Name, so);

    }

    // Update is called once per frame
    public GameObject spawnCard(Transform where)
    {
        return Instantiate(CreaturePrefab, where);
    }

    //public GameObject spawnCreatureByName()
    //{
    //    var creature = Instantiate(CreaturePrefab);
    //    CardEntity newCardEntity = creature.GetComponent<CardEntity>();
    //    newCardEntity.InitializeCard(creatures[creatureName], forPlayer);
    //    return creature;
    //}

    public CardEntity spawnCreatureByName(string creatureName, bool forPlayer)
    {
        var creature = Instantiate(CreaturePrefab);
        CardData.selectController(creature, creatureName);
        CardEntity newCardEntity = creature.GetComponent<CardEntity>();
        newCardEntity.InitializeCard(creatures[creatureName], forPlayer);
        newCardEntity.UpdateStatus();
        return newCardEntity;
    }

    public void spawnCreatureByNameOnField(string creatureName, bool forPlayer)
    {
        GameObject creature = Instantiate(CreaturePrefab, transform.parent);
        CardData.selectController(creature, creatureName);
        CardEntity newCardEntity = creature.GetComponent<CardEntity>();
        Debug.Log(creatureName);
        newCardEntity.InitializeCard(creatures[creatureName], forPlayer);
        gameBoardRegulator.TrySummonCardToPlayerBoard(newCardEntity, forPlayer);
        newCardEntity.UpdateStatus();
    }

    public void spawnPartnerFromDeck(string creatureName, bool forPlayer, Transform deck)
    {
        GameObject creature = Instantiate(CreaturePrefab, deck); //transform.parent
        CardData.selectController(creature, creatureName);
        CardEntity newCardEntity = creature.GetComponent<CardEntity>();
        newCardEntity.InitializeCard(creatures[creatureName], forPlayer);
        StartCoroutine(MoveWithDelay(newCardEntity.gameObject, transform.parent, 0.5f));
        gameBoardRegulator.TrySummonCardToPlayerBoard(newCardEntity, forPlayer);
        newCardEntity.UpdateStatus();
    }

    private IEnumerator MoveWithDelay(GameObject go, Transform position, float time)
    {
        LeanTween.move(go, position, time);
        yield return new WaitForSecondsRealtime(time);
        go.transform.SetParent(transform.parent, true);
    }

    //public void spawnCopy(CardEntity original, bool forPlayer)
    //{
    //    GameObject creature = Instantiate(CreaturePrefab, transform.parent);
    //    creature.transform.position = original.transform.position;
    //    CardData.selectController(creature, original.cardData.Name);
    //    CardEntity newCardEntity = creature.GetComponent<CardEntity>();      
    //    newCardEntity.InitializeCard(creatures[original.cardData.Name], forPlayer);
    //    gameBoardRegulator.TrySummonCardToPlayerBoard(newCardEntity, forPlayer);
    //}
}
