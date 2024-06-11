using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Player : MonoBehaviour
{
    public int maxHealth = 100;
    public int health = 100;
    public TextMeshProUGUI healthText;
    public Slider healthSlider;
    private MenuNavigation menuNavigation;

    void Start()
    {
        health = maxHealth;
        healthSlider.maxValue = maxHealth;
        UpdateHealthUI();
        menuNavigation = FindObjectOfType<MenuNavigation>();
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            health = 0;
            Defeated();
        }
        UpdateHealthUI();
    }

    public void Heal(int amount)
    {
        health += amount;
        if (health > maxHealth)
        {
            health = maxHealth;
        }
        UpdateHealthUI();
    }

    void UpdateHealthUI()
    {
        if (healthText != null)
        {
            healthText.text = health.ToString();
        }
        if (healthSlider != null)
        {
            healthSlider.value = health;
        }
    }

    void Defeated()
    {
        Debug.Log("Player has been defeated.");
        GameSceneManager.Instance.RestartScene();
    }
}