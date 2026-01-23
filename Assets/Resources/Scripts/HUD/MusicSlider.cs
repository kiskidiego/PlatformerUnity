using UnityEngine;
using UnityEngine.UI;

public class MusicSlider : MonoBehaviour
{
    Slider slider;
    void Start()
    {
        slider = GetComponent<Slider>();
        slider.value = AudioManager.Instance.GetMusicVolume();
    }
    public void OnMusicSliderChanged()
    {
        Debug.Log("Music volume changed to: " + slider.value);
        AudioManager.Instance.SetMusicVolume(slider.value);
    }
}
