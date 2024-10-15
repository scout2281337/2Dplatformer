using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Slider masterSlider;

    private void Start()
    {
        
        if (PlayerPrefs.HasKey("Volume"))
        {
            float savedVolume = PlayerPrefs.GetFloat("Volume");
            masterSlider.value = savedVolume;
            SetVolume(savedVolume);
        }
        else
        {
            masterSlider.value = 0.75f; 
            SetVolume(0.75f);
        }

        
        masterSlider.onValueChanged.AddListener(SetVolume);
    }

    public void SetVolume(float volume)
    {
        
        float dbVolume = Mathf.Log10(volume) * 20;
        audioMixer.SetFloat("MyExposedParam", dbVolume);

        
        PlayerPrefs.SetFloat("Volume", volume);
        PlayerPrefs.Save();
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
        PlayerPrefs.SetInt("Fullscreen", isFullscreen ? 1 : 0);
        PlayerPrefs.Save(); 
    }
}
