using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // �� ������ ���� ���ӽ����̽� �߰�

public class ButtonController : MonoBehaviour
{
    public Button[] buttonsA; // A ��ư �迭
    public Button buttonB; // B ��ư
    public Button musicButton; // ���� ��ư
    public Slider volumeSlider; // ���� �����̴�
    public Text messageText; // �ؽ�Ʈ ����� ���� Text ������Ʈ
    public AudioSource backgroundMusic; // ��� ������ ���� AudioSource

    void Start()
    {
        // ��� ��ư �ʱ�ȭ
        foreach (Button button in buttonsA)
        {
            button.onClick.AddListener(() => OnButtonAClicked(button));
            button.gameObject.SetActive(true); // ��� A ��ư Ȱ��ȭ
        }

        buttonB.onClick.AddListener(OnButtonBClicked);
        buttonB.gameObject.SetActive(false); // B ��ư ��Ȱ��ȭ
        musicButton.gameObject.SetActive(false); // ���� ��ư ��Ȱ��ȭ
        volumeSlider.gameObject.SetActive(false); // ���� �����̴� ��Ȱ��ȭ
        messageText.gameObject.SetActive(false); // �ؽ�Ʈ ��Ȱ��ȭ

        // ���� �����̴� �ʱ� ����
        volumeSlider.onValueChanged.AddListener(OnVolumeChange);
    }

    void OnButtonAClicked(Button button)
    {
        // ù ��° ��ư Ŭ�� �� ���� ���� �� �� ��ȯ
        if (button == buttonsA[0])
        {
            backgroundMusic.Stop(); // ��� ���� ���߱�
            SceneManager.LoadScene("MainGame"); // "NextScene"�� ���ϴ� �� �̸����� �ٲ��ּ���
            return; // ���� ������ �������� �ʵ��� ����
        }

        // ��� A ��ư ��Ȱ��ȭ �� B ��ư Ȱ��ȭ
        foreach (Button btn in buttonsA)
        {
            btn.gameObject.SetActive(false);
        }
        buttonB.gameObject.SetActive(true);

        // �� ��° ��ư Ŭ�� �� �ؽ�Ʈ Ȱ��ȭ �� ���� ����
        if (button == buttonsA[1])
        {
            messageText.gameObject.SetActive(true);
            messageText.text = "�� ������ �ֻ����� ���� ���� �̵���Ű�� ���� �̺�Ʈ �ӿ��� ��� ����� �����ϴ� �����Դϴ�. ��� ������ ���� ��� �� �������� �¸�, �÷��̾��� ���� ���� �� �������� �й��Դϴ�.";
        }

        // �� ��° ��ư Ŭ�� �� ���� ��ư�� ���� �����̴� Ȱ��ȭ
        if (button == buttonsA[2])
        {
            musicButton.gameObject.SetActive(true);
            volumeSlider.gameObject.SetActive(true); // ���� �����̴� Ȱ��ȭ
        }

        // �� ��° ��ư Ŭ�� �� ���α׷� ����
        if (button == buttonsA[3])
        {
            Application.Quit();
        }
    }

    void OnButtonBClicked()
    {
        // B ��ư ��Ȱ��ȭ �� ��� A ��ư Ȱ��ȭ
        buttonB.gameObject.SetActive(false);
        foreach (Button button in buttonsA)
        {
            button.gameObject.SetActive(true);
        }

        // ���� ��ư�� ���� �����̴� ��Ȱ��ȭ
        musicButton.gameObject.SetActive(false);
        volumeSlider.gameObject.SetActive(false); // ���� �����̴� ��Ȱ��ȭ

        // �ؽ�Ʈ ��Ȱ��ȭ
        messageText.gameObject.SetActive(false);
    }

    void OnVolumeChange(float value)
    {
        backgroundMusic.volume = value; // �����̴� ������ ���� ����
    }
}
