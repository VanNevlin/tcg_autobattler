using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCard", menuName = "Card")]
public class Card : ScriptableObject
{
    public string cardName;
    public string description;

    public int damage;  
    public int heal;

    public bool isUnit;
    public int attack;
    public int health;

    public GameObject unitPrefab; // Prefab reference for unit visualization

    public void PlayCard(PlayerController player, PlayerController enemy)
    {
        if (isUnit)
        {
            if (unitPrefab != null)
            {
                // Instantiate the unit prefab at the player's spawn position
                GameObject unitObject = GameObject.Instantiate(unitPrefab, player.spawnPosition.position, Quaternion.identity);
                Unit newUnit = unitObject.GetComponent<Unit>();

                // Set unit stats
                newUnit.unitName = cardName;
                newUnit.attack = attack;
                newUnit.health = health;

                player.AddUnitToField(newUnit);
                Debug.Log($"Summoned unit {cardName} with {attack} attack and {health} health.");
            }
            else
            {
                Debug.LogError($"Unit card {cardName} is missing a prefab reference!");
            }
        }
        else
        {
            if (damage > 0)
            {
                if (enemy.HasUnitsOnField())
                {
                    Unit targetUnit = enemy.GetRandomUnit();
                    targetUnit.TakeDamage(damage);
                    Debug.Log($"{cardName} dealt {damage} damage to enemy unit {targetUnit.name}.");
                }
                else
                {
                    enemy.TakeDamage(damage);
                }
            }
            if (heal > 0)
            {
                player.Heal(heal);
            }
        }
    }

}
