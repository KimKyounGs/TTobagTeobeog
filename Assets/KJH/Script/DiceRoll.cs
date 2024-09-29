using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceRoll : MonoBehaviour
{
    public GameController controller;
    public Transform[] DiceNum; // 주사위 눈

    Rigidbody rigid;
    
    public int num; // 주사위 결과값
    public int nextPos;

    //public bool isRolled = false;
    

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
    }

    public IEnumerator Roll()
    {
        yield return null;
        controller.isPlaying = true;
        rigid.AddForce(new Vector3 (0, 4, 0), ForceMode.Impulse);
        transform.localEulerAngles = new Vector3(Random.Range(-90f, 90), Random.Range(-90f, 90), Random.Range(-90f, 90));
        rigid.angularVelocity = Random.insideUnitSphere * Random.Range(-1000, 1000);

        yield return new WaitForSeconds(3);
        while(true)
        {
            yield return null;
            if (rigid.velocity.sqrMagnitude < 0.001f)
                break;
        }
        for(int i = 0; i < 6; i++)
        {
            if (DiceNum[i].position.y > 0.1f)
            {
                num = i+1;

                Debug.Log("주사위 : "+num);
                nextPos = num + controller.curPos[controller.curPlayer];
                if (nextPos > 31)
                    nextPos -= 32;
                controller.curPos[controller.curPlayer] = nextPos;
                Debug.Log(nextPos);
                controller.players[controller.curPlayer].transform.position = controller.Pos.transform.GetChild(nextPos).transform.position;
                if (controller.curPlayer == 0)
                {
                    controller.players[controller.curPlayer].transform.position = controller.Pos.transform.GetChild(controller.curPos[controller.curPlayer]).transform.position + new Vector3(-0.25f, 0, 0.25f);
                }
                else if (controller.curPlayer == 1)
                {
                    controller.players[controller.curPlayer].transform.position = controller.Pos.transform.GetChild(controller.curPos[controller.curPlayer]).transform.position + new Vector3(0.25f, 0, 0.25f);
                }
                else if (controller.curPlayer == 2)
                {
                    controller.players[controller.curPlayer].transform.position = controller.Pos.transform.GetChild(controller.curPos[controller.curPlayer]).transform.position + new Vector3(-0.25f, 0, -0.25f);
                }
                else if (controller.curPlayer == 3)
                {
                    controller.players[controller.curPlayer].transform.position = controller.Pos.transform.GetChild(controller.curPos[controller.curPlayer]).transform.position + new Vector3(0.25f, 0, -0.25f);
                }
                controller.eventStage();
                break;
            }
            controller.isPlaying = false;
        }
    }
}
