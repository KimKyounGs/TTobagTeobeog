using System.Collections;
using UnityEngine;

public class MiniGameDiceController : MonoBehaviour
{
    // 주사위 결과를 저장하는 변수들
    public int side1Result = 0; // 상대에게 x2만큼의 데미지 입히기
    public int side2Result = 0; // 현재 턴 주인의 방어도 +1
    public int side3Result = 0; // 현재 턴 주인과 상대 방어도 각각 +1
    public int side4Result = 0; // 현재 턴 주인의 HP -1
    public int side5Result = 0; // 현재 턴 주인과 상대 HP 각각 -1
    public int side6Result = 0; // 주사위 다시 굴리기
    
    private int totalDiceCnt = 0;  // 굴려진 주사위 개수
    public int needDiceCnt;   // 굴려야 하는 총 주사위 개수

    void Start()
    {
        needDiceCnt = 6; // 굴려야 할 주사위 개수 (예시로 6개로 설정)
        MiniGameManager.instance.diceController = this; 
    }

    public void SetNeedDiceCnt(int cnt)
    {
        needDiceCnt = cnt;
    }

    // DiceRoller에서 전달받은 주사위 결과를 변수에 저장
    public void SaveDiceResult(int result)
    {
        switch (result)
        {
            case 1:
                side1Result++;
                break;
            case 2:
                side2Result++;
                break;
            case 3:
                side3Result++;
                break;
            case 4:
                side4Result++;
                break;
            case 5:
                side5Result++;
                break;
            case 6:
                side6Result++;
                break;
        }

        totalDiceCnt++;

        // 모든 주사위가 굴려졌으면 결과 시뮬레이션 시작
        if (totalDiceCnt >= needDiceCnt)
        {
            Debug.Log("모든 주사위가 굴려졌습니다!");
            StartCoroutine(SimulateResults()); // 모든 결과가 나온 후 실행
        }
    }

    // 결과값을 차례로 텀을 두고 처리하는 함수
    IEnumerator SimulateResults()
    {
        yield return new WaitForSeconds(1f); // 텀을 두고 실행

        // 주사위 6 처리
        if (side6Result > 0)
        {
            Debug.Log("주사위 6 결과 실행");
            if (MiniGameManager.instance.IsPlayerTurn)
            {
                MiniGameManager.instance.diceManager.AddReusableDice(true, side6Result);
            }
            else
            {
                MiniGameManager.instance.diceManager.AddReusableDice(false, side6Result);
            }
            yield return new WaitForSeconds(1.5f);
        }

        // 주사위 2 처리
        if (side2Result > 0)
        {
            Debug.Log("주사위 2 결과 실행");
            if (MiniGameManager.instance.IsPlayerTurn)
            {
                MiniGameManager.instance.player.IncreaseDefense(side2Result);
            }
            else
            {
                MiniGameManager.instance.ai.IncreaseDefense(side2Result);
            }
            yield return new WaitForSeconds(1.5f);
        }

        // 주사위 3 처리
        if (side3Result > 0)
        {
            Debug.Log("주사위 3 결과 실행");
            MiniGameManager.instance.ai.IncreaseDefense(side3Result);
            MiniGameManager.instance.player.IncreaseDefense(side3Result);
            yield return new WaitForSeconds(1.5f);
        }

        // 주사위 4 처리
        if (side4Result > 0)
        {
            Debug.Log("주사위 4 결과 실행");
            if (MiniGameManager.instance.IsPlayerTurn)
            {
                MiniGameManager.instance.player.DecreaseHealth(side4Result);
            }
            else
            {
                MiniGameManager.instance.ai.DecreaseHealth(side4Result);
            }
            yield return new WaitForSeconds(1.5f);
        }

        // 주사위 5 처리
        if (side5Result > 0)
        {
            Debug.Log("주사위 5 결과 실행");
            MiniGameManager.instance.ai.DecreaseHealth(side5Result);
            MiniGameManager.instance.player.DecreaseHealth(side5Result);
            yield return new WaitForSeconds(1.5f);
        }

        // 주사위 1 처리 (결과값이 모아진 후 한 번에 처리)
        if (side1Result > 0)
        {
            Debug.Log("주사위 1 결과 실행");
            if (MiniGameManager.instance.IsPlayerTurn)
            {
                MiniGameManager.instance.ai.DecreaseHealth(side1Result * 2);
            }
            else
            {
                MiniGameManager.instance.player.DecreaseHealth(side1Result * 2);
            }
            yield return new WaitForSeconds(1.5f);
        }

        // 턴 변경
        yield return new WaitForSeconds(1.5f); // 텀을 두고 실행
        MiniGameManager.instance.TurnChange();

        // 결과 처리 후 변수 초기화
        ResetResults();
    }

    // 변수 초기화
    public void ResetResults()
    {
        side1Result = 0;
        side2Result = 0;
        side3Result = 0;
        side4Result = 0;
        side5Result = 0;
        side6Result = 0;
        totalDiceCnt = 0;
    }
}
