using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsManager : MonoBehaviour {

    [SerializeField] Slider musicVolume = null;
    [SerializeField] Slider sfxVolume = null;

    private GameSceneManager sceneManager;
    private AudioManager audioManager;

    void Start() {
        sceneManager = FindObjectOfType<GameSceneManager>();
        audioManager = FindObjectOfType<AudioManager>();
        musicVolume.value = MixerValueToSlider(SettingsPlayerPrefs.LoadMusicVolume());
        sfxVolume.value = MixerValueToSlider(SettingsPlayerPrefs.LoadSoundVolume());
    }

    public void SetMusicVolume(float volume) {
        float mixerVolume = SliderToMixerValue(volume);
        audioManager.SetMusicVolume(mixerVolume);
        SettingsPlayerPrefs.SetMusicVolume(mixerVolume);
    }

    public void SetSFXVolume(float volume) {
        float mixerVolume = SliderToMixerValue(volume);
        audioManager.SetSFXVolume(mixerVolume);
        SettingsPlayerPrefs.SetSoundVolume(mixerVolume);
    }

    private float MixerValueToSlider(float volume) {
        float volume1 = Mathf.Pow(10, volume / 20);
        return volume1;
    }

    private float SliderToMixerValue(float volume) {
        return Mathf.Log10(volume) * 20;
    }

    public void SaveAndBackToMenu() {
        SettingsPlayerPrefs.Save();
        sceneManager.MainMenu();
    }
}
