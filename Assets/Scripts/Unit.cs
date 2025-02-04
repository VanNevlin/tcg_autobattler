using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public string unitName;
    public int attack;
    public int health;
    public GameObject unitVisual;

    public Unit(string name, int attack, int health, GameObject visualPrefab, Transform spawnPosition)
    {
        this.unitName = name;
        this.attack = attack;
        this.health = health;
        this.unitVisual = GameObject.Instantiate(visualPrefab, spawnPosition.position, Quaternion.identity);
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Debug.Log($"{name} unit has been defeated!");
            if (unitVisual) Destroy(unitVisual); // Remove the unit's visual when defeated
        }
    }

    public bool IsDefeated()
    {
        return health <= 0;
    }
}
