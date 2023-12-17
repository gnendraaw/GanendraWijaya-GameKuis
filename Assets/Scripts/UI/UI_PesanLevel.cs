using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_PesanLevel : MonoBehaviour {
    private const string IS_WIN_PARAM = "IsWin";

    [SerializeField] private TextMeshProUGUI _tempatPesan;
    [SerializeField] private GameObject _winOption;
    [SerializeField] private GameObject _loseOption;

    [Header("LEVEL MESSAGE ANIMATION")]
    [SerializeField] private Animator _animator;

    public string Pesan {
        get => _tempatPesan.text;
        set => _tempatPesan.text = value;
    }

    private void Awake() {
        if (gameObject.activeSelf) gameObject.SetActive(false);

        UI_Timer.OnTimerRunsOut += UI_Timer_OnTimerRunsOut;
        UI_Jawaban.OnAnyAnswerSelected += UI_Jawaban_OnAnyAnswerSelected;
    }

    private void UI_Jawaban_OnAnyAnswerSelected(string answer, bool isCorrect) {
        Pesan = $"Your Answer is {isCorrect}! (Answer: {answer})";

        ToggleWinLoseOption(isCorrect);

        gameObject.SetActive(true);

        _animator.SetBool(IS_WIN_PARAM, isCorrect);

        // Play SFX
        if (!isCorrect) SFX.Instance.PlayWrongAnswerSound();
        else SFX.Instance.PlayCorrectAnswerSound();
        
    }

    private void ToggleWinLoseOption(bool isCorrect) {
        _winOption.SetActive(isCorrect);
        _loseOption.SetActive(!isCorrect);
    }

    private void OnDestroy() {
        UI_Timer.OnTimerRunsOut -= UI_Timer_OnTimerRunsOut;
        UI_Jawaban.OnAnyAnswerSelected -= UI_Jawaban_OnAnyAnswerSelected;
    }

    private void UI_Timer_OnTimerRunsOut() {
        Pesan = "Times UP!";
        ToggleWinLoseOption(false);
        gameObject.SetActive(true);
    }
}

