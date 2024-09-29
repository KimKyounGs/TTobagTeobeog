using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditorInternal;
using Unity.Properties;

public class GameController : MonoBehaviour
{
    const int startMoney = 500;
    const int decreaseMoney = 400;

    public DiceRoll diceRoll;
    public Roll_Btn rollBtn;

    public GameObject Pos;
    public GameObject[] players;    // 플레이어 4명 (플레이어 + AI 3명)

    public GameObject[] Arrow;

    public Text[] moneyTXT;
    public Text roundTXT;

    public int[] curPos;
    public int player;       // 플레이어 번호
    public int[] money;
    public int curPlayer = 0;
    public int randEvent;
    public int round = 1;
    public int stake = 0;
    public int num1;
    public int winner;
    public int max;

    public bool isPlaying = false;  // false = 진행중X(주사위 굴릴 수 있는 상태) true = 진행중O(주사위 못굴리는 상태)
    public bool isPaused = false;
    public bool isMainGame = true;
    public bool playerWin;

    public void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        DontDestroyOnLoad(Arrow[0]);
        DontDestroyOnLoad(Arrow[1]);
        DontDestroyOnLoad(Arrow[2]);
        DontDestroyOnLoad(Arrow[3]);
        DontDestroyOnLoad(Pos);
        DontDestroyOnLoad(players[0]);
        DontDestroyOnLoad(players[1]);
        DontDestroyOnLoad(players[2]);
        DontDestroyOnLoad(players[3]);
    }

    void Start()
    {
        player = Random.Range(0, players.Length);
        Debug.Log("순서 : "+player);

        for(int i = 0; i < players.Length; i++)
        {
            moneyTXT[i].text = "Money:" + money[i];
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            isMainGame = false;
            SceneManager.LoadScene("MiniGame");
        }

        if(!isMainGame)
        {
            return;
        }

        for(int i = 0; i < players.Length; i++)
        {
            if (money[i] > max)
            {
                max = money[i];
                winner = i;
            }
        }
        if (round >= 10 && !isPaused)
        {
            rollBtn.eventObj.SetActive(true);
            rollBtn.eventTXT.text = "Player" + (winner + 1) + " WIN!";
            Time.timeScale = 0;
        }

        for(int i = 0;i < players.Length;i++)
        {
            if(curPlayer != i)
            {
                Arrow[i].SetActive(false);
                Debug.Log("사라짐!");
            }
            else
            {
                Arrow[curPlayer].SetActive(true);
                Debug.Log("생김!");
            }
        }

        for (int i = 0; i < players.Length; i++)
        {
            moneyTXT[i].text = "Money:" + money[i];
        }


        if (!isPlaying && curPlayer != player)
        {
            StartCoroutine(RollCo());
            isPlaying = true;
        }

        roundTXT.text = "Round:" + round; 
    }

    public IEnumerator RollCo()
    {
        yield return diceRoll.StartCoroutine(diceRoll.Roll());
    }

    public void eventStage()
    {
        for(int i = 0; i < 4; i++)
        {
            if (curPos[curPlayer] == curPos[i] && curPlayer != i)
            {
                battle(i);
                return;
            }
        }
        
        randEvent = Random.Range(0, 100);
        if (randEvent < 30 && round > 1)
        {
            money[curPlayer] += startMoney;
            rollBtn.eventObj.SetActive(true);
            rollBtn.eventTXT.text = "+" + startMoney + " Money\n(Player" + (curPlayer + 1) + ")";
            Time.timeScale = 0;
            isPaused = true;
        }
        else if (randEvent < 45 && round > 1)
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
            rollBtn.eventObj.SetActive(true);
            rollBtn.eventTXT.text = "All Player -" + decreaseMoney + " Money\n(Except Player" + (curPlayer + 1) + ")";
            Time.timeScale = 0;
            isPaused = true;
        }
        else if (randEvent < 60 && round > 2)
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

                rollBtn.eventObj.SetActive(true);
                rollBtn.eventTXT.text = "Teleport To Player" + (battlePlayer + 1) + "\n(Player" + (curPlayer+1)+")";
                Time.timeScale = 0;
                isPaused = true;

            }
        }
        else if(randEvent < 65 && round > 3)
        {
            int rand = Random.Range(0, players.Length);
            while(rand == curPlayer)
            {
                rand = Random.Range(0, players.Length);
            }

            num1 = money[curPlayer];
            money[curPlayer] = money[rand];
            money[rand] = num1;

            rollBtn.eventObj.SetActive(true);
            rollBtn.eventTXT.text = "Change Money\n(Player" + (curPlayer+1) + "and Player"+(rand+1)+")";
            Time.timeScale = 0;
            isPaused = true;
        }

        curPlayer++;
        if (curPlayer > 3)
        {
            curPlayer = 0;
            round++;
        }
            
        isPlaying = false;
    }

    public void battle(int i)
    {
        rollBtn.eventObj.SetActive(true);
        rollBtn.eventTXT.text = "Player"+(curPlayer+1)+" vs Player"+(i+1);
        stake = (money[i] / 2) + (money[curPlayer] / 2);
        money[i] -= money[i] / 2;
        money[curPlayer] -= money[curPlayer] / 2;

        if (curPlayer == player || i == player)
        {
            Debug.Log("플레이어 전투");
            SceneManager.LoadScene("MiniGame");
            Debug.Log("Move Scene");
        }
        else
        {
            
            if (Random.Range(0, 2) == 0)
            {
                money[i] += stake;
                rollBtn.eventTXT.text += "\nPlayer" + (i + 1) + " WIN!";
            }
            else
            {
                money[curPlayer] += stake;
                rollBtn.eventTXT.text += "\nPlayer" + (curPlayer + 1) + " WIN!";
            }
        }

        for(i = 0; i < players.Length; i++)
        {
            moneyTXT[i].text = "Money:" + money[i];
        }

        isPlaying = false;
        Time.timeScale = 0;
    }

}
