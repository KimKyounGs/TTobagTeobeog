using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // 씬 관리를 위한 네임스페이스 추가

public class ButtonController : MonoBehaviour
{
    public Button[] buttonsA; // A 버튼 배열
    public Button buttonB; // B 버튼
    public Button musicButton; // 음악 버튼
    public Slider volumeSlider; // 볼륨 슬라이더
    public Text messageText; // 텍스트 출력을 위한 Text 컴포넌트
    public AudioSource backgroundMusic; // 배경 음악을 위한 AudioSource

    void Start()
    {
        // 모든 버튼 초기화
        foreach (Button button in buttonsA)
        {
            button.onClick.AddListener(() => OnButtonAClicked(button));
            button.gameObject.SetActive(true); // 모든 A 버튼 활성화
        }

        buttonB.onClick.AddListener(OnButtonBClicked);
        buttonB.gameObject.SetActive(false); // B 버튼 비활성화
        musicButton.gameObject.SetActive(false); // 음악 버튼 비활성화
        volumeSlider.gameObject.SetActive(false); // 볼륨 슬라이더 비활성화
        messageText.gameObject.SetActive(false); // 텍스트 비활성화

        // 볼륨 슬라이더 초기 설정
        volumeSlider.onValueChanged.AddListener(OnVolumeChange);
    }

    void OnButtonAClicked(Button button)
    {
        // 첫 번째 버튼 클릭 시 음악 정지 및 씬 전환
        if (button == buttonsA[0])
        {
            backgroundMusic.Stop(); // 배경 음악 멈추기
            SceneManager.LoadScene("MainGame"); // "NextScene"을 원하는 씬 이름으로 바꿔주세요
            return; // 이후 로직을 수행하지 않도록 종료
        }

        // 모든 A 버튼 비활성화 후 B 버튼 활성화
        foreach (Button btn in buttonsA)
        {
            btn.gameObject.SetActive(false);
        }
        buttonB.gameObject.SetActive(true);

        // 두 번째 버튼 클릭 시 텍스트 활성화 및 내용 변경
        if (button == buttonsA[1])
        {
            messageText.gameObject.SetActive(true);
            messageText.text = "이 게임은 주사위를 통해 말을 이동시키며 여러 이벤트 속에서 상대 말들과 경쟁하는 게임입니다. 상대 말들의 돈이 모두 다 떨어지면 승리, 플레이어의 돈이 먼저 다 떨어지면 패배입니다.";
        }

        // 세 번째 버튼 클릭 시 음악 버튼과 볼륨 슬라이더 활성화
        if (button == buttonsA[2])
        {
            musicButton.gameObject.SetActive(true);
            volumeSlider.gameObject.SetActive(true); // 볼륨 슬라이더 활성화
        }

        // 네 번째 버튼 클릭 시 프로그램 종료
        if (button == buttonsA[3])
        {
            Application.Quit();
        }
    }

    void OnButtonBClicked()
    {
        // B 버튼 비활성화 후 모든 A 버튼 활성화
        buttonB.gameObject.SetActive(false);
        foreach (Button button in buttonsA)
        {
            button.gameObject.SetActive(true);
        }

        // 음악 버튼과 볼륨 슬라이더 비활성화
        musicButton.gameObject.SetActive(false);
        volumeSlider.gameObject.SetActive(false); // 볼륨 슬라이더 비활성화

        // 텍스트 비활성화
        messageText.gameObject.SetActive(false);
    }

    void OnVolumeChange(float value)
    {
        backgroundMusic.volume = value; // 슬라이더 값으로 볼륨 조절
    }
}
