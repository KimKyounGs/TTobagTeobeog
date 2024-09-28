using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniGameUIManager : MonoBehaviour
{   
    // Sprite
    private int playerShieldIndex = 0;
    private int aiShieldIndex = 0;
    public Sprite goodShieldSprite;
    public Sprite badShieldSprite;
    public Sprite goodDiceSprite;
    public Sprite badDiceSprite;

    // Image List
    private int playerDiceIndex = 0;
    private int aiDiceIndex = 0;
    public List<Image> playerDiceList;
    public List<Image> playerShieldList;
    public List<Image> aiDiceList;
    public List<Image> aiShieldList;

    // Health Text
    public Text playerHealthText;
    public Text aiHealthText;

    void Start()
    {
        MiniGameManager.instance.miniGameUIManager = this;
        playerShieldIndex = 0;
        aiShieldIndex = 0;
        playerDiceIndex = 0;
        aiDiceIndex = 0;
    }

    // flag가 true면 플레이어 dice sprite 고치기, flag2에 따라 goodSprite와 badSprite 교체
    public void SetDiceList(bool flag, bool flag2, int cnt)
    {
        Sprite targetSprite = flag2 ? goodDiceSprite : badDiceSprite;  // flag2에 따라 스프라이트 선택

        if (flag)
        {
            if (flag2)
            {
                for (int i = playerDiceIndex; i < playerDiceIndex + cnt; i ++)
                {
                    if (i >= 0 && i <= 14)
                    {
                        playerDiceList[i].sprite = targetSprite;
                    }
                    playerDiceIndex += cnt;
                    if (playerDiceIndex > 14) playerDiceIndex = 14;
                }
            }
            else 
            {
                for (int i = playerDiceIndex - cnt; i < playerDiceIndex; i ++)
                {
                    if (i >= 0 && i <= 14)
                    {
                        playerDiceList[i].sprite = targetSprite;
                    }
                    playerDiceIndex -= cnt;
                    if (playerDiceIndex < 0) playerDiceIndex = 0;
                }
            }
        }
        else
        {
            if (flag2)
            {
                for (int i = aiDiceIndex; i <= aiDiceIndex + cnt; i ++)
                {
                    if (i >= 0 && i <= 14)
                    {
                        aiDiceList[i].sprite = targetSprite;
                    }
                    aiDiceIndex += cnt;
                    if (aiDiceIndex > 14) aiDiceIndex = 14;
                }
            }
            else 
            {
                for (int i = aiDiceIndex - cnt; i < aiDiceIndex; i ++)
                {
                    if (i >= 0 && i <= 14)
                    {
                        aiDiceList[i].sprite = targetSprite;
                    }
                }
                playerDiceIndex -= cnt;
                if (playerDiceIndex < 0) playerDiceIndex = 0;
            }
        }
    }
    
    // flag가 true면 플레이어 shield sprite 고치기, flag2에 따라 goodSprite와 badSprite 교체
    public void SetShieldList(bool flag, bool flag2, int cnt)
    {
        Sprite targetSprite = flag2 ? goodShieldSprite : badShieldSprite;  // flag2에 따라 스프라이트 선택

        if (flag)
        {
            if (flag2)
            {
                for (int i = playerShieldIndex; i < playerShieldIndex + cnt; i ++)
                {
                    if (i >= 0 && i <= 5)
                    {
                        playerShieldList[i].sprite = targetSprite;
                    }
                }
                playerShieldIndex += cnt;
                if (playerShieldIndex > 5) playerShieldIndex = 5;
            }
            else 
            {
                for (int i = playerShieldIndex - cnt; i < playerShieldIndex; i ++)
                {
                    if (i >= 0 && i <= 5)
                    {
                        playerDiceList[i].sprite = targetSprite;
                    }
                }
                playerShieldIndex -= cnt;
                if (playerShieldIndex < 0) playerShieldIndex = 0;
            }
        }
        else
        {
            if (flag2)
            {
                for (int i = aiShieldIndex; i < aiShieldIndex + cnt; i ++)
                {
                    if (i >= 0 && i <= 5)
                    {
                        aiShieldList[i].sprite = targetSprite;
                    }
                }
                aiShieldIndex += cnt;
                if (aiShieldIndex > 5) aiShieldIndex = 5;
            }
            else 
            {
                for (int i = aiShieldIndex - cnt; i < aiShieldIndex; i ++)
                {
                    if (i >= 0 && i <= 5)
                    {
                        aiShieldList[i].sprite = targetSprite;
                    }
                }
                aiShieldIndex -= cnt;
                if (aiShieldIndex > 5) aiShieldIndex = 0;
            }
        }
    }


    public void SetHealthText(bool flag, int Health)
    {
        if (flag) 
        {
            playerHealthText.text = Health.ToString();
        }
        else
        {
            aiHealthText.text = Health.ToString();
        }
    }
}
