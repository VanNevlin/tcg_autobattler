using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public DeckController deckController;
    public int health = 20;
    public int maxHealth = 20; 

    [Header("UI Elements")]
    public Slider healthBar;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI cardInfoText; // Reference to the Card Info UI Text


    public void Setup()
    {
        deckController.ShuffleDeck();
        UpdateCardInfoText("Card Played: None"); // Initialize with default text
    }

    public void TakeTurn(Action onTurnComplete)
    {
        Card drawnCard = deckController.DrawCard();
        if (drawnCard != null)
        {
            //Debug.Log($"Drew card: {drawnCard.cardName}");
            UpdateCardInfoText($"{this.tag} Played: {drawnCard.cardName}"); // Update the UI
            drawnCard.PlayCard(this, FindOpponent());
            deckController.DiscardCard(drawnCard);
        }

        onTurnComplete?.Invoke();
    }

    public void TakeDamage(int amount)
    {
        health -= amount;
        if(health <= 0) health = 0;
        UpdateHealthUI();
        if (health <= 0)
        {
            UpdateCardInfoText($"{this.tag} Has Been Defeated:");
        }
    }

    public void Heal(int amount)
    {
        health += amount;
        if (health > maxHealth) health = maxHealth;
        UpdateHealthUI();
    }

    private PlayerController FindOpponent()
    {
        if(gameObject.CompareTag("Player")) return FindObjectOfType<GameManager>().enemy; 
        else return FindObjectOfType<GameManager>().player;
    }
    private void UpdateCardInfoText(string message)
    {
        if (cardInfoText != null)
        {
            cardInfoText.text = message;
        }
    }
    public void UpdateHealthUI()
    {
        if (healthBar != null)
            healthBar.value = (float)health / maxHealth;

        if (healthText != null)
            healthText.text = $"{health}/{maxHealth}";
    }
}
