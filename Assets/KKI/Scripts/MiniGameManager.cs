using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameManager : MonoBehaviour
{
    public static MiniGameManager instance;
    public MiniGameDiceController diceController;
    public MiniGameDiceManager diceManager;
    public MiniGamePlayer player;
    public MiniGameAI ai;
    public MiniGameUIManager miniGameUIManager;
    public bool IsPlayerTurn;
    void Awake() 
    {
        if (instance == null)
        {
            instance = this;
        }    
        IsPlayerTurn = true;
    }
    void Update()
    {
        // 플레이어 턴일 때 Space바를 눌러 주사위를 굴림
        if (IsPlayerTurn && Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log($"player Health = {player.GetHealth()}, player defense = {player.GetDefense()}");
            Debug.Log("플레이어가 Space바를 눌렀습니다.");
            diceManager.RollPlayerDice();  // 플레이어 주사위 던지기
        }
    }
    public void TurnChange()
    {
        Debug.Log("턴 변경!");
        // 턴이 끝나면 던진 주사위를 회수
        diceManager.CollectDice();
        IsPlayerTurn = !IsPlayerTurn;
        
        if (IsPlayerTurn)
        {
            Debug.Log("플레이어 턴! Space바를 눌러서 주사위를 굴리세요.");
        }
        else
        {
            Debug.Log("AI 턴! 2초 후 주사위를 굴립니다.");
            StartCoroutine(AIDiceRollDelay());  // AI 턴 지연 코루틴 시작
        }
    }

    // AI 주사위 던지기 전에 2초 텀을 주는 코루틴
    IEnumerator AIDiceRollDelay()
    {
        yield return new WaitForSeconds(2f);  // 2초 대기
        Debug.Log($"ai Health = {ai.GetHealth()}, ai defense = {ai.GetDefense()}");
        diceManager.RollAIDice();  // AI 주사위 던지기
    }

    public void EndGame(bool flag)
    {
        if (flag)
        {
            Debug.Log("플레이어 승!");
        }
        else 
        {
            Debug.Log("플레이어 패배!");
        }
    }
    
}
