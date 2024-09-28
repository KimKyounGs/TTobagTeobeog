using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    const int startMoney = 500;
    const int decreaseMoney = 400;

    public DiceRoll diceRoll;

    public GameObject Pos;
    public GameObject[] players;    // �÷��̾� 4�� (�÷��̾� + AI 3��)

    public int[] curPos;
    public int player;       // �÷��̾� ��ȣ
    public int[] money;
    public int curPlayer = 0;

    public bool isPlaying = false;  // false = ������X(�ֻ��� ���� �� �ִ� ����) true = ������O(�ֻ��� �������� ����)


    void Start()
    {
        player = Random.Range(0, players.Length);
        Debug.Log("���� : "+player);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isPlaying && curPlayer != player)
        {
            StartCoroutine(RollCo());
            isPlaying = true;
        }
    }

    public IEnumerator RollCo()
    {
        yield return diceRoll.StartCoroutine(diceRoll.Roll());
    }

    public void eventStage()
    {
        if (curPos[curPlayer] == 0)
        {
            money[curPlayer] += startMoney;
        }
        else if (curPos[curPlayer] == 8)
        {
            for (int i = 0; i < 4; i++)
            {
                if(curPlayer == i)
                {
                    continue;
                }
                if (money[i] < decreaseMoney)
                    money[i] = 0;
                else
                    money[i] -= decreaseMoney;
            }
        }
        else if (curPos[curPlayer] == 16)
        {
            if (curPlayer != player)
            {
                int battlePlayer = Random.Range(0, players.Length);
                while (true)
                {
                    if (battlePlayer != curPlayer)
                        break;
                    battlePlayer = Random.Range(0, players.Length);
                }
                    
                players[curPlayer].transform.position = Pos.transform.GetChild(curPos[battlePlayer]).transform.position;
                curPos[curPlayer] = curPos[battlePlayer];
            }
        }

        curPlayer++;
        if (curPlayer > 3)
            curPlayer = 0;
        isPlaying = false;
    }

}
