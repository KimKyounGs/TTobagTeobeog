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

    // 플레이어 턴 문구
    public Text turnText;  // "내 턴" 문구를 표시할 텍스트 UI
    public float fadeDuration = 1.0f; // 페이드 인/아웃 시간
    public float displayTime = 2.0f;  // 텍스트가 화면에 표시되는 시간

    void Start()
    {
        MiniGameManager.instance.miniGameUIManager = this;
        playerShieldIndex = 0;
        aiShieldIndex = 0;
        playerDiceIndex = 0;
        aiDiceIndex = 0;

        // 텍스트를 처음에 투명하게 설정
        SetTextAlpha(0);
    }

    // flag가 true면 플레이어 dice sprite 고치기, flag2에 따라 goodSprite와 badSprite 교체
    public void SetDiceList(bool flag, bool flag2, int cnt)
    {
        Sprite targetSprite = flag2 ? goodDiceSprite : badDiceSprite;  // flag2에 따라 스프라이트 선택

        if (flag)
        {
            if (flag2)
            {
                for (int i = playerDiceIndex - cnt; i < playerDiceIndex; i ++)
                {
                    if (i >= 0 && i <= 14)
                    {
                        playerDiceList[i].sprite = targetSprite;
                    }
                }
                playerDiceIndex -= cnt;
                if (playerDiceIndex < 0) playerDiceIndex = 0;
            }
            else 
            {
                for (int i = playerDiceIndex; i < playerDiceIndex + cnt; i ++)
                {
                    if (i >= 0 && i <= 14)
                    {
                        playerDiceList[i].sprite = targetSprite;
                    }
                }
                playerDiceIndex += cnt;
                if (playerDiceIndex > 14) playerDiceIndex = 14;
            }
        }
        else
        {
            if (flag2)
            {
                for (int i = aiDiceIndex - cnt; i < aiDiceIndex; i ++)
                {
                    if (i >= 0 && i <= 14)
                    {
                        aiDiceList[i].sprite = targetSprite;
                    }
                }
                aiDiceIndex -= cnt;
                if (aiDiceIndex < 0) aiDiceIndex = 0;
            }
            else 
            {
                for (int i = aiDiceIndex; i < aiDiceIndex + cnt; i ++)
                {
                    if (i >= 0 && i <= 14)
                    {
                        aiDiceList[i].sprite = targetSprite;
                    }
                }
                aiDiceIndex += cnt;
                if (aiDiceIndex > 14) aiDiceIndex = 14;
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
                        playerShieldList[i].sprite = targetSprite;
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

    // "내 턴" 문구를 표시하고 페이드 인/아웃 적용
    public void ShowTurnNotification()
    {
        StartCoroutine(FadeInOutRoutine());
    }

    // 페이드 인/아웃 루틴
    private IEnumerator FadeInOutRoutine()
    {
        // 텍스트를 페이드 인
        yield return StartCoroutine(FadeTextIn());

        // 텍스트가 일정 시간 동안 표시됨
        yield return new WaitForSeconds(displayTime);

        // 텍스트를 페이드 아웃
        yield return StartCoroutine(FadeTextOut());
    }

    // 페이드 인 함수
    private IEnumerator FadeTextIn()
    {
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Clamp01(elapsedTime / fadeDuration);
            SetTextAlpha(alpha);
            yield return null;
        }
    }

    // 페이드 아웃 함수
    private IEnumerator FadeTextOut()
    {
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Clamp01(1 - (elapsedTime / fadeDuration));
            SetTextAlpha(alpha);
            yield return null;
        }
    }

    // 텍스트의 알파(투명도)를 설정하는 함수
    private void SetTextAlpha(float alpha)
    {
        Color color = turnText.color;
        color.a = alpha;
        turnText.color = color;
    }
}
