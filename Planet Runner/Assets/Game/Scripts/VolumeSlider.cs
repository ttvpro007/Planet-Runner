using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    SoundManager soundManager;

    [SerializeField] Slider volumeSlider;

    private void OnEnable()
    {
        soundManager = SoundManager.instance;

        if (PlayerPrefs.HasKey("VolumeSlider"))
        {
            volumeSlider.value = PlayerPrefs.GetFloat("VolumeSlider");
        }
        else
        {
            volumeSlider.value = soundManager.GetVolume();
        }

        volumeSlider.onValueChanged.AddListener(soundManager.SetVolume);
    }

    private void OnDisable()
    {
        volumeSlider.onValueChanged.RemoveAllListeners();
        PlayerPrefs.SetFloat("VolumeSlider", volumeSlider.value);
    }
}
