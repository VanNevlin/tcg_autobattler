using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public string name;
    public int attack;
    public int health;

    public Unit(string name, int attack, int health)
    {
        this.name = name;
        this.attack = attack;
        this.health = health;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Debug.Log($"{name} unit has been defeated!");
        }
    }

    public bool IsDefeated()
    {
        return health <= 0;
    }
}
