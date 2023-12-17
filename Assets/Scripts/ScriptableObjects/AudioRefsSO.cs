using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(
    fileName = "Audio Refs",
    menuName = "Game Kuis/Auio Refs"
)]
public class AudioRefsSO : ScriptableObject {
    public AudioClip[] GameBackgroundMusics;

    public AudioClip CorrectAnswerAudio;
    public AudioClip WrongAnswerAudio;
    public AudioClip ClickAudio;
    public AudioClip QuestionAudio;
    public AudioClip SuccessAudio;
}

