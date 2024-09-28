using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // UI 관련 네임스페이스 추가
#if UNITY_EDITOR
using UnityEditor; // Unity 에디터에서 종료를 위한 네임스페이스
#endif

public class QuitButton : MonoBehaviour
{
    public Button quitButton; // 종료 버튼

    void Start()
    {
        // 종료 버튼에 클릭 이벤트 설정
        quitButton.onClick.AddListener(QuitApplication);
    }

    // 프로그램 종료 메서드
    void QuitApplication()
    {
        print("프로그램 종료");
#if UNITY_EDITOR
        // 에디터에서 플레이 모드 종료
        EditorApplication.isPlaying = false;
#else
        // 실제 빌드된 애플리케이션에서 종료
        Application.Quit();
#endif
    }
}