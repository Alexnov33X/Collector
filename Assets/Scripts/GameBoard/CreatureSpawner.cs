using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureSpawner : MonoBehaviour
{
    public GameObject CreaturePrefab;
    public List<CardScriptableObject> creaturesList;
    Dictionary<string, CardScriptableObject> creatures;
    public static CreatureSpawner instance = null;
    private GameBoardRegulator gameBoardRegulator;

    void Awake()
    {
        // ������, ��������� ������������� ����������
        if (instance == null)
        { // ��������� ��������� ��� ������
            instance = this; // ������ ������ �� ��������� �������
        }
        else if (instance == this)
        { // ��������� ������� ��� ���������� �� �����
            Destroy(gameObject); // ������� ������
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

    public void spawnCreature(string creatureName, bool forPlayer)
    {
        var creature = Instantiate(CreaturePrefab);
        CardEntity newCardEntity = creature.GetComponent<CardEntity>(); 
        newCardEntity.InitializeCard(creatures[creatureName], forPlayer);
        gameBoardRegulator.TrySummonCardToPlayerBoard(newCardEntity, forPlayer);
    }
}
