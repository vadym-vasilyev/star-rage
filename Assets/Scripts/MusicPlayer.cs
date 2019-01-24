using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class MusicPlayer : MonoBehaviour {
    [SerializeField] AudioClip menuMusic = null;
    [SerializeField] AudioClip gameMusic = null;
    [SerializeField] AudioClip winMusic = null;
    [SerializeField] AudioClip looseMusic = null;

    private Dictionary<string, AudioClip> musicMap = new Dictionary<string, AudioClip>();
    private AudioSource musicPlayer;
    private AudioClip activeClip;

    void OnEnable() {
        musicMap.Add(PredefinedStrings.SCENE_MENU, menuMusic);
        musicMap.Add(PredefinedStrings.SCENE_GAME, gameMusic);
        musicMap.Add(PredefinedStrings.SCENE_WIN, winMusic);
        musicMap.Add(PredefinedStrings.SCENE_LOOSE, looseMusic);
        musicPlayer = GetComponent<AudioSource>();
        SceneManager.activeSceneChanged += ChangedActiveScene;
    }

    void ChangedActiveScene(Scene current, Scene next) {
        if (musicMap.ContainsKey(next.name)) {
            AudioClip nextAudioClip = musicMap[next.name];
            if (activeClip == nextAudioClip) {
                return;
            }
            activeClip = nextAudioClip;
            musicPlayer.Stop();
            musicPlayer.clip = nextAudioClip;
            musicPlayer.Play();
        }
    }
}
