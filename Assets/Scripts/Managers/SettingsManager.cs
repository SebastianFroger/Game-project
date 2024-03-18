using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsManager : Singleton<SettingsManager>
{
    public AudioMixer mixer;

    public Slider masterVol;
    public Slider musicVol;
    public Slider effectsVol;

    private void Start()
    {
        masterVol.value = PlayerPrefs.GetFloat("MasterVol");
        musicVol.value = PlayerPrefs.GetFloat("MusicVol");
        effectsVol.value = PlayerPrefs.GetFloat("EffectsVol");
    }

    public void SetMasterVol(Slider slider)
    {
        mixer.SetFloat("MasterVol", Mathf.Log10(slider.value + 0.0001f) * 20);
        PlayerPrefs.SetFloat("MasterVol", slider.value);
    }

    public void SetMusicVol(Slider slider)
    {
        mixer.SetFloat("MusicVol", Mathf.Log10(slider.value + 0.0001f) * 20);
        PlayerPrefs.SetFloat("MusicVol", slider.value);
    }

    public void SetEffectsVol(Slider slider)
    {
        mixer.SetFloat("EffectsVol", Mathf.Log10(slider.value + 0.0001f) * 20);
        PlayerPrefs.SetFloat("EffectsVol", slider.value);
    }
}