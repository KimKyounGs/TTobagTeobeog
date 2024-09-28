using System.Collections;
using UnityEngine;

public class MiniGameDiceRoller : MonoBehaviour
{
    private Rigidbody diceRigidbody;
    public Transform[] diceSideTransforms; // 주사위 면 정보
    public float throwForce = 6f;   // 주사위를 던질 힘
    void OnEnable()
    {
        diceRigidbody = GetComponent<Rigidbody>();
        diceRigidbody.isKinematic = true;  // 활성화될 때마다 초기화
        throwForce = 6f;
    }

    public void RollDice()
    {
        diceRigidbody.isKinematic = false;

        // 주사위를 던지는 힘과 회전 적용
        diceRigidbody.AddForce(Vector3.down * throwForce, ForceMode.Impulse);
        diceRigidbody.AddTorque(Random.Range(200, 500), Random.Range(200, 500), Random.Range(200, 500));

        StartCoroutine(CheckDiceResult());
    }

    IEnumerator CheckDiceResult()
    {
        yield return new WaitForSeconds(3f);

        if (IsDiceStopped())
        {
            int result = CheckTopFace();

            // DiceController에 결과 전달
            MiniGameManager.instance.diceController.SaveDiceResult(result);
        }
    }

    bool IsDiceStopped()
    {
        return diceRigidbody.velocity.magnitude < 0.1f && diceRigidbody.angularVelocity.magnitude < 0.1f;
    }

    int CheckTopFace()
    {
        float maxValue = -100.0f;
        int maxIndex = -1;
        for (int i = 0; i < diceSideTransforms.Length; i++)
        {
            float tempValue = diceSideTransforms[i].position.y;
            if (tempValue > maxValue)
            {
                maxValue = tempValue;
                maxIndex = i;
            }
        }
        return maxIndex + 1; // 1~6
    }
}
