using System.Collections.Generic;
using UnityEngine;

public class MiniGameDiceManager : MonoBehaviour
{
    public GameObject dicePrefab;   // 주사위 프리팹
    public int poolSize = 10;       // 오브젝트 풀의 크기
    private Queue<GameObject> dicePool;  // 주사위를 저장하는 큐
    private List<GameObject> currentTurnDice = new List<GameObject>();  // 현재 턴에 던진 주사위 리스트
    public int playerDiceCount = 20;  // 플레이어가 가진 주사위 수
    public int aiDiceCount = 20;      // AI가 가진 주사위 수
    public int maxDicePerTurn = 6;    // 한 턴에 굴릴 수 있는 최대 주사위 수
    public Transform diceSpawnPoint;  // 주사위를 던질 위치

    void Start()
    {
        MiniGameManager.instance.diceManager = this;
        InitializeDicePool();
        playerDiceCount = 20;
        aiDiceCount = 20;
    }

    // 주사위 풀 초기화
    void InitializeDicePool()
    {
        dicePool = new Queue<GameObject>();

        for (int i = 0; i < poolSize; i++)
        {
            GameObject dice = Instantiate(dicePrefab);
            dice.SetActive(false);  // 사용하지 않을 때는 비활성화
            dicePool.Enqueue(dice);  // 오브젝트 풀에 주사위 추가
        }
    }

    // 주사위를 오브젝트 풀에서 가져오기
    public GameObject GetDiceFromPool(Vector3 position)
    {
        GameObject dice;
        
        if (dicePool.Count > 0)
        {
            dice = dicePool.Dequeue();  // 풀에서 주사위를 가져옴
            dice.SetActive(true);  // 활성화
        }
        else
        {
            dice = Instantiate(dicePrefab);  // 새로운 주사위 생성
        }

        // 주사위를 주어진 위치에 배치
        dice.transform.position = position;
        dice.transform.rotation = Random.rotation;  // 무작위 회전

        return dice;
    }

    // 사용한 주사위를 다시 오브젝트 풀로 반환
    public void ReturnDiceToPool(GameObject dice)
    {
        dice.SetActive(false);  // 비활성화
        dicePool.Enqueue(dice);  // 오브젝트 풀로 반환
    }

    // 플레이어가 주사위를 굴리려고 할 때 호출
    public void RollPlayerDice()
    {
        int diceToThrow = Mathf.Min(playerDiceCount, maxDicePerTurn);  // 굴릴 주사위 개수 결정
        Debug.Log($"플레이어가 {diceToThrow}개의 주사위를 던집니다.");

        for (int i = 0; i < diceToThrow; i++)
        {
            GameObject dice = GetDiceFromPool(diceSpawnPoint.position);
            MiniGameDiceRoller roller = dice.GetComponent<MiniGameDiceRoller>();
            roller.RollDice();

            // 이번 턴에 던진 주사위 리스트에 추가
            currentTurnDice.Add(dice);
        }

        playerDiceCount -= diceToThrow;  // 굴린 주사위 개수 차감
        Debug.Log($"플레이어가 주사위를 굴린 후 남은 주사위: {playerDiceCount}");
    }

    // AI가 주사위를 굴리려고 할 때 호출
    public void RollAIDice()
    {
        int diceToThrow = Mathf.Min(aiDiceCount, maxDicePerTurn);  // 굴릴 주사위 개수 결정
        Debug.Log($"AI가 {diceToThrow}개의 주사위를 던집니다.");

        for (int i = 0; i < diceToThrow; i++)
        {
            GameObject dice = GetDiceFromPool(diceSpawnPoint.position);
            MiniGameDiceRoller roller = dice.GetComponent<MiniGameDiceRoller>();
            roller.RollDice();

            // 이번 턴에 던진 주사위 리스트에 추가
            currentTurnDice.Add(dice);
        }

        aiDiceCount -= diceToThrow;  // 굴린 주사위 개수 차감
        Debug.Log($"AI가 주사위를 굴린 후 남은 주사위: {aiDiceCount}");
    }

    // 턴이 끝나면 던진 주사위 회수
    public void CollectDice()
    {
        foreach (var dice in currentTurnDice)
        {
            ReturnDiceToPool(dice);  // 오브젝트 풀로 주사위 반환
        }

        // 리스트 초기화
        currentTurnDice.Clear();
    }

    public int GetPlayerDiceCount() => playerDiceCount;
    public int GetAIDiceCount() => aiDiceCount;
}
