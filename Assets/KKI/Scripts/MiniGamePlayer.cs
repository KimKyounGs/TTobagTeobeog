using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGamePlayer : MonoBehaviour
{
    private int health = 15;
    private int defense = 0;
    void Start()
    {
        health = 15;
        MiniGameManager.instance.player = this; 
    }

    public void DecreaseHealth(int amount)
    {
        if (defense >= amount)
        {
            DecreaseDefense(amount);
            return;
        } 
        else
        {
            amount -= defense;
        }

        health -= amount;
        if (health <= 0)
        {
            MiniGameManager.instance.EndGame(false);
        }

        MiniGameManager.instance.miniGameUIManager.SetHealthText(true, health);
    }

    public void DecreaseDefense(int amount)
    {
        defense -= amount;
        if (defense < 0) defense = 0; // 최소값 0

        MiniGameManager.instance.miniGameUIManager.SetShieldList(true, false, amount);
    }

    public void IncreaseDefense(int amount)
    {
        defense += amount;
        if (defense > 8) defense = 8; // 최대값 8

        MiniGameManager.instance.miniGameUIManager.SetShieldList(true, true, amount);
    }

    public int GetHealth() => health;
    public int GetDefense() => defense;
    public void SetHealth(int health)
    {
        this.health = health;
    }

    public void SetDefense(int defense)
    {
        this.defense = defense;
    }

    
}
