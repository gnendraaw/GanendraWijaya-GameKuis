using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGM : MonoBehaviour {
    public static BGM Instance { get; private set; }

    [SerializeField] private AudioRefsSO _audioRefsSO;

    private AudioSource audioSource;   

    private void Awake() {
        if (Instance == null) {
            Instance = this;
        } else {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);

        audioSource = GetComponent<AudioSource>();
    }

    public void PlayMenuBGM() {
        PlayBGM(_audioRefsSO.GameBackgroundMusics[0]);
    }

    public void PlayKuisBGM() {
        PlayBGM(_audioRefsSO.GameBackgroundMusics[1]);
    }

    public void PlayBGM(AudioClip audioClip) {
        audioSource.clip = audioClip;
        audioSource.Play();
    }
}

