using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLife : MonoBehaviour
{
    public int health = 100;

    void Start()
    {
        Debug.Log("Bob is alive!");
    }

    public void ReceiveDamage(int damage) 
    {
        health -= damage;
        Debug.Log("Bob took damage: " + damage);
        Debug.Log("Bob has " + health + " hp left!");
    }
}
