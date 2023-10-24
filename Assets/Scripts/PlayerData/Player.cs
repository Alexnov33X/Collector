using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerDisplay pd;
    private int health = 90;
    public int Health { get => health; set => health = value; }
    // Start is called before the first frame update
    void Start()
    {
        pd.updateInformation(health.ToString());
    }

    public void OnHit(int damage)
    {
        Health -= damage;
        pd.updateInformation(Health.ToString());
    }

}
