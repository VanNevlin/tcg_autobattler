using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public PlayerController player;
    public PlayerController enemy;

    [Header("UI Elements")]
    public Slider playerHealthbar;
    public Slider enemyHealthbar;

    [Header("Health Text")]
    public TextMeshProUGUI playerHealthText;
    public TextMeshProUGUI enemyHealthText;

    private bool playerTurn = true;

    private void Start()
    {
        playerHealthbar.maxValue = player.health;
        playerHealthbar.value = player.health;
        UpdateHealthText(playerHealthText, player.health, player.maxHealth);

        enemyHealthbar.maxValue = enemy.health;
        enemyHealthbar.value = enemy.health;
        UpdateHealthText(enemyHealthText, enemy.health, enemy.maxHealth);

        StartBattle();
    }
    private void UpdateHealthUI()
    {
        playerHealthbar.value = player.health;
        enemyHealthbar.value = enemy.health;

        UpdateHealthText(playerHealthText, player.health, player.maxHealth);
        UpdateHealthText(enemyHealthText, enemy.health, enemy.maxHealth);
    }
    private void UpdateHealthText(TextMeshProUGUI healthText, int healthValue, int maxHealth)
    {
        healthText.text = $"{healthValue}/{maxHealth} HP"; // Example: "20 HP"
    }

    private void StartBattle()
    {
        player.Setup();
        enemy.Setup();
        StartTurn();
    }

    private void StartTurn()
    {
        if (playerTurn)
        {
            //Debug.Log("Player's Turn");   
            StartCoroutine(TurnDelay(player, enemy, onPlayerTurnComplete));
        }
        else
        {
            //Debug.Log("Enemy's Turn");
            StartCoroutine(TurnDelay(enemy, player, onEnemyTurnComplete));
        }
    }

    private void onPlayerTurnComplete()
    {
        UpdateHealthUI();
        if (enemy.health <= 0)
        {
            EndBattle("Player wins!");
            return;
        }
        playerTurn = false;
        StartTurn(); // Pass to enemy's turn.
    }

    private void onEnemyTurnComplete()
    {
        UpdateHealthUI();
        if (player.health <= 0)
        {
            EndBattle("Enemy wins!");
            return;
        }
        playerTurn = true;
        StartTurn(); // Pass back to player's turn.
    }

    private IEnumerator TurnDelay(PlayerController current, PlayerController opponent, System.Action onTurnComplete)
    {
        yield return new WaitForSeconds(2f); // Delay of 1 seconds
        current.TakeTurn(() =>
        {
            onTurnComplete?.Invoke();
        });
    }

    private void EndBattle(string winnerMessage)
    {
        Debug.Log(winnerMessage);
        // Here you can add any additional game over logic (e.g., stopping the game or showing UI)
    }
}

/*
    Notes :
    The start turn is hard coded rn, i can try to make it something like :
    StartCoroutine(TurnDelay(this.tag,  ()

    But it maybe won't work beacuase tag is "Player" instead of "player", and it's only 2 players also, 
    so i think it's good for now.

 */