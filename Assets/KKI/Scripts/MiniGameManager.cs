using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MiniGameManager : MonoBehaviour
{
    public static MiniGameManager instance;
    public MiniGameDiceController diceController;
    public MiniGameDiceManager diceManager;
    public MiniGamePlayer player;
    public MiniGameAI ai;
    public MiniGameUIManager miniGameUIManager;
    public bool IsPlayerTurn;
    public bool hasRolled;

    public AudioSource audioSource;  // 오디오 소스 컴포넌트
    public AudioClip diceClip;  
    public AudioClip attackClip;  
    public AudioClip defenseClip;  
    public AudioClip HitClip;  
    public AudioClip windClip;  

    void Awake() 
    {
        if (instance == null)
        {
            instance = this;
        }    
        IsPlayerTurn = true;
        hasRolled = false;

        // 오브젝트에 AudioSource 컴포넌트가 연결되어 있는지 확인
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    void Start()
    {
        miniGameUIManager.ShowTurnNotification();
    }
     
    void Update()
    {
        // 플레이어 턴일 때 Space바를 눌러 주사위를 굴림
        if (IsPlayerTurn && Input.GetKeyDown(KeyCode.Space) && !hasRolled)
        {
            hasRolled = true;
            diceManager.RollPlayerDice();  // 플레이어 주사위 던지기
        }
    }
     
    public void TurnChange()
    {
        Debug.Log("턴 변경!");
        // 턴이 끝나면 던진 주사위를 회수
        diceManager.CollectDice();
        if (IsPlayerTurn)
        {
            hasRolled = false;
        }
        IsPlayerTurn = !IsPlayerTurn;
        
        if ((diceManager.playerDiceCount == 0 && diceManager.aiDiceCount == 0) || (player.GetHealth() == 0) || (ai.GetHealth() == 0))
        {
            if (player.GetHealth() > ai.GetHealth())
            {
                EndGame(1);
            }
            else if (player.GetHealth() < ai.GetHealth())
            {
                EndGame(2);
            }
            else {
                EndGame(3);
            }
            return;
        }

        if (IsPlayerTurn)
        {
            miniGameUIManager.ShowTurnNotification();
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
        diceManager.RollAIDice();  // AI 주사위 던지기
    }

    public void EndGame(int flag)
    {
        if (flag == 1)
        {
            miniGameUIManager.turnText.text = "플레이어 승리!";
            miniGameUIManager.ShowTurnNotification();
            SceneManager.LoadScene("MainGame");
        }
        else if(flag == 2)
        {
            miniGameUIManager.turnText.text = "플레이어 패배!";
            miniGameUIManager.ShowTurnNotification();
            SceneManager.LoadScene("MainGame");
        }
        else if(flag == 3)
        {
            miniGameUIManager.turnText.text = "무승부!";
            miniGameUIManager.ShowTurnNotification();
            SceneManager.LoadScene("MainGame");
        }
    }
    
}
