using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_PesanLevel : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI _tempatPesan;
    [SerializeField] private GameObject _winOption;
    [SerializeField] private GameObject _loseOption;

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

