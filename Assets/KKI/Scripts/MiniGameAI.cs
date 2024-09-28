using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameAI : MonoBehaviour
{
    private int ID;
    private int diceCnt = 20;
    private int health = 20;
    private int defense = 0;
    private bool IsDead = false;
    void Start()
    {
        MiniGameManager.instance.ai = this; 
    }

    public void RollAIDice()
    {

    }

    public void CanRollDice()
    {

    }

    public void DecreaseHealth(int amount)
    {
        health -= amount;
        if (health <= 0)
        {
            MiniGameManager.instance.EndGame(true);
        }
    }

    public void DecreaseDefense(int amount)
    {
        defense -= amount;
        if (defense < 0) defense = 0; // 최소값 0
    }

    public void IncreaseDefense(int amount)
    {
        defense += amount;
        if (defense > 8) defense = 8; // 최대값 8
    }

    public void IncreaseDiceCnt(int amount)
    {
        diceCnt += amount;
    }
}
