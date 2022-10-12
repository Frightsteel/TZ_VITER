using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;

public class VolumeController : MonoBehaviour
{
    [SerializeField] private Slider _volumeSlider;
    [SerializeField] private TextMeshProUGUI _volumeValueText;

    [SerializeField] private AudioMixer _mixer;

    private void Start()
    {
        LoadVolume();
    }

    public void UpdateVolumeText(float volume)
    {
        _volumeValueText.text = volume.ToString("0.0");
        _mixer.SetFloat("MusicVol", Mathf.Log10(volume) * 20);
    }

    public void SaveVolume()
    {
        float volumeValue = _volumeSlider.value;
        PlayerPrefs.SetFloat("VolumeValue", volumeValue);
        LoadVolume();
    }

    public void LoadVolume()
    {
        float volumeValue = PlayerPrefs.GetFloat("VolumeValue");
        _volumeSlider.value = volumeValue;
        _mixer.SetFloat("MusicVol", Mathf.Log10(_volumeSlider.value) * 20);
    }
}
