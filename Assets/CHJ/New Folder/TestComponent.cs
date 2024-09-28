using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // UI 관련 네임스페이스 추가

public class ButtonToggle : MonoBehaviour
{

    public Button buttonA; // A 버튼
    public Button buttonB; // B 버튼

    void Start()
    {
        // 초기 설정: A 버튼만 활성화, B 버튼은 비활성화
        buttonA.gameObject.SetActive(true);
        buttonB.gameObject.SetActive(false);

        // A 버튼 클릭 이벤트 설정
        buttonA.onClick.AddListener(OnButtonAClicked);

        // B 버튼 클릭 이벤트 설정
        buttonB.onClick.AddListener(OnButtonBClicked);
    }

    // A 버튼을 클릭했을 때 호출되는 메서드
    void OnButtonAClicked()
    {
        buttonA.gameObject.SetActive(false); // A 버튼 비활성화
        buttonB.gameObject.SetActive(true);  // B 버튼 활성화
    }

    // B 버튼을 클릭했을 때 호출되는 메서드
    void OnButtonBClicked()
    {
        buttonB.gameObject.SetActive(false); // B 버튼 비활성화
        buttonA.gameObject.SetActive(true);  // A 버튼 활성화
    }
}
