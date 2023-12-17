using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFX : MonoBehaviour {
    public static SFX Instance { get; private set; }

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

    public void PlayAudio(AudioClip clip) {
        audioSource.PlayOneShot(clip);
    }

    public void PlayButtonClickSound() {
        PlayAudio(_audioRefsSO.ClickAudio);
    }

    public void PlayWrongAnswerSound() {
        PlayAudio(_audioRefsSO.WrongAnswerAudio);
    }

    public void PlayCorrectAnswerSound() {
        PlayAudio(_audioRefsSO.CorrectAnswerAudio);
    }

    public void PlayQuestionSound() {
        PlayAudio(_audioRefsSO.QuestionAudio);
    }

    public void PlaySuccessAudio() {
        PlayAudio(_audioRefsSO.SuccessAudio);
    }
}

