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

    public void PlayCard(PlayerController player, PlayerController enemy)
    {
        // Logic for playing a card.
        if (damage > 0)
        {
            enemy.TakeDamage(damage);
        }
        if (heal > 0)
        {
            player.Heal(heal);
        }
    }

}
