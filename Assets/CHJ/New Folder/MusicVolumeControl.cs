using UnityEngine;

using UnityEngine;
using UnityEngine.UI;

public class MusicVolumeControl : MonoBehaviour
{
    public AudioSource backgroundMusic; // 배경음악 AudioSource
    public Slider volumeSlider;          // 볼륨 조절 슬라이더
    public Button stopButton;           // 음악 정지 버튼

    void Start()
    {
        // 슬라이더 값 변경 시 SetVolume 함수 호출
        volumeSlider.onValueChanged.AddListener(SetVolume);
        // 슬라이더 초기값 설정
        volumeSlider.value = backgroundMusic.volume;
    }

    // 슬라이더 값에 따라 배경음악 볼륨 조절
    void SetVolume(float volume)
    {
        backgroundMusic.volume = volume;
    }

    // 버튼 클릭 시 음악 정지
    public void StopMusic()
    {
        backgroundMusic.Stop();
    }
}
