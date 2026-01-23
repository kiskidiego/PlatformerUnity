using UnityEngine;
using UnityEngine.UI;

public class SfxSlider : MonoBehaviour
{
    Slider slider;
    void Start()
    {
        slider = GetComponent<Slider>();
        slider.value = AudioManager.Instance.GetSfxVolume();
    }
    public void OnSfxSliderChanged()
    {
        Debug.Log("SFX volume changed to: " + slider.value);
        AudioManager.Instance.SetSfxVolume(slider.value);
    }
}
