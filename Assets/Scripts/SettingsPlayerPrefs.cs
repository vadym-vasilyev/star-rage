using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsPlayerPrefs: PlayerPrefs {

    private const string MUSIC_VOLUME_KEY = "MusicVolume_";

    private const string SOUND_VOLUME_KEY = "SoundVolume_";

    public static void SetMusicVolume(float volume) {
        PlayerPrefs.SetFloat(MUSIC_VOLUME_KEY, volume);
    }

    public static float LoadMusicVolume() {
        return PlayerPrefs.GetFloat(MUSIC_VOLUME_KEY);
    }

    public static void SetSoundVolume(float volume) {
        PlayerPrefs.SetFloat(SOUND_VOLUME_KEY, volume);
    }

    public static float LoadSoundVolume() {
        return PlayerPrefs.GetFloat(SOUND_VOLUME_KEY);
    }
}
