using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBoard : MonoBehaviour
{
    [Header("Slots")]
    public Transform[] cardSlots;
    
    public Entity[] occupants;
    // Start is called before the first frame update
    void Start()
    {
    
    }

    public void AddEntity(Entity e, int index)
    {
        occupants[index]=e;
        return;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
