using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // UI 관련 네임스페이스 추가

public class ButtonToggle : MonoBehaviour
{

    public List<Button> buttonAList; // A 버튼들 리스트로 관리
    public Button buttonB; // B 버튼
    public Button quitButton; // 프로그램 종료를 위한 버튼
    public Text messageText; // 두 번째 A 버튼 클릭 시 나타날 텍스트

    void Start()
    {
        // 초기 설정: A 버튼들만 활성화, B 버튼은 비활성화, 텍스트 비활성화
        foreach (Button buttonA in buttonAList)
        {
            buttonA.gameObject.SetActive(true);
            // 각 A 버튼에 클릭 이벤트 등록
            buttonA.onClick.AddListener(OnButtonAClicked);
        }
        buttonB.gameObject.SetActive(false);
        messageText.gameObject.SetActive(false); // 텍스트 비활성화

        // B 버튼 클릭 이벤트 설정
        buttonB.onClick.AddListener(OnButtonBClicked);

        // 종료 버튼 클릭 이벤트 설정
        quitButton.onClick.AddListener(QuitApplication);
    }

    // A 버튼을 클릭했을 때 호출되는 메서드
    void OnButtonAClicked()
    {
        // 모든 A 버튼을 비활성화
        foreach (Button buttonA in buttonAList)
        {
            buttonA.gameObject.SetActive(false);
        }

        // 두 번째 A 버튼 클릭 시 텍스트 활성화
        if (buttonAList[1].IsActive())
        { // 두 번째 버튼의 인덱스는 1
            messageText.gameObject.SetActive(true); // 텍스트 활성화
        }

        // B 버튼 활성화
        buttonB.gameObject.SetActive(true);
    }

    // B 버튼을 클릭했을 때 호출되는 메서드
    void OnButtonBClicked()
    {
        // B 버튼 비활성화
        buttonB.gameObject.SetActive(false);
        // 모든 A 버튼을 다시 활성화
        foreach (Button buttonA in buttonAList)
        {
            buttonA.gameObject.SetActive(true);
        }
        // 텍스트 비활성화
        messageText.gameObject.SetActive(false);
    }

    // 프로그램 종료 메서드
    void QuitApplication()
    {
        print("프로그램 종료");
        Application.Quit();
    }
}