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

    public GameObject btn;

    private void Update()
    {
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
            turnTXT.text = "turn : You";
        }
        else
        {
            turnTXT.text = "turn : Player" + (controller.curPlayer + 1);
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
