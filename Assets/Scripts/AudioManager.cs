using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour {

    [SerializeField] AudioMixer masterMixer = null;

    void Awake() {
        if (FindObjectsOfType<MusicPlayer>().Length > 1) {
            gameObject.SetActive(false);
            Destroy(gameObject);
        } else {
            DontDestroyOnLoad(gameObject);
        }
    }

    public void SetMusicVolume(float volume) {
        masterMixer.SetFloat(PredefinedStrings.EXPOSED_MUSIC_VOLUME, volume);
    }

    public void SetSFXVolume(float volume) {
        masterMixer.SetFloat(PredefinedStrings.EXPOSED_SFX_VOLUME, volume);
    }

    void Start() {
        SetMusicVolume(SettingsPlayerPrefs.LoadMusicVolume());
        SetSFXVolume(SettingsPlayerPrefs.LoadSoundVolume());
    }
}
