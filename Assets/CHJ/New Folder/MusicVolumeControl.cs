using UnityEngine;

using UnityEngine;
using UnityEngine.UI;

public class MusicVolumeControl : MonoBehaviour
{
    public AudioSource backgroundMusic; // ������� AudioSource
    public Slider volumeSlider;          // ���� ���� �����̴�
    public Button stopButton;           // ���� ���� ��ư

    void Start()
    {
        // �����̴� �� ���� �� SetVolume �Լ� ȣ��
        volumeSlider.onValueChanged.AddListener(SetVolume);
        // �����̴� �ʱⰪ ����
        volumeSlider.value = backgroundMusic.volume;
    }

    // �����̴� ���� ���� ������� ���� ����
    void SetVolume(float volume)
    {
        backgroundMusic.volume = volume;
    }

    // ��ư Ŭ�� �� ���� ����
    public void StopMusic()
    {
        backgroundMusic.Stop();
    }
}
