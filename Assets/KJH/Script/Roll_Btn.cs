using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UI;

public class Roll_Btn : MonoBehaviour
{
    public DiceRoll diceRoll;
    public GameController controller;
    public Text turnTXT;
    public Text[] playerTXT;
    public GameObject eventObj;
    public Text eventTXT;

    public GameObject btn;

    private void Start()
    {
        eventTXT.text = "You Are Number" + (controller.player + 1);
        Time.timeScale = 0;
        for(int i = 0; i < playerTXT.Length; i++)
        {
            if(i == controller.player)
                playerTXT[i].text = "You";
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            eventObj.SetActive(false);
            Time.timeScale = 1;
            controller.isPaused = false;
        }

        // �� �����϶��� ��ư ǥ��
        if (controller.isPlaying || controller.curPlayer != controller.player)
        {
            btn.gameObject.SetActive(false);
        }
        else if (controller.curPlayer == controller.player)
        {
            btn.gameObject.SetActive(true);
        }

        // ���� ������ ǥ��
        if(controller.curPlayer == controller.player)
        {
            
        }
        else
        {
            
        }
        
    }

    public void RollStartCo()
    {
        StartCoroutine(RollCo());
    }

    public IEnumerator RollCo()
    {
        yield return diceRoll.StartCoroutine(diceRoll.Roll());
    }

}
