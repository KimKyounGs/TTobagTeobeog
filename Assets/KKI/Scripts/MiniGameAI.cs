using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameAI : MonoBehaviour
{
    private int ID;
    private int health = 10;
    private int defense = 0;
    void Start()
    {
        health = 10;
        MiniGameManager.instance.ai = this; 
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
            DecreaseDefense(defense);
        }
        
        health -= amount;
        if (health <= 0)
        {
            health = 0;
        }

        MiniGameManager.instance.miniGameUIManager.SetHealthText(false, health);
    }

    public void DecreaseDefense(int amount)
    {
        defense -= amount;
        if (defense < 0) defense = 0; // 최소값 0

        MiniGameManager.instance.miniGameUIManager.SetShieldList(false, false, amount);
    }

    public void IncreaseDefense(int amount)
    {
        defense += amount;
        if (defense > 6) defense = 6; // 최대값 8

        MiniGameManager.instance.miniGameUIManager.SetShieldList(false, true, amount);
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
