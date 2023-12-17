using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {
    [SerializeField] private SelectedLevelSO _selectedLevelSO;
    [SerializeField] private LevelPackKuisSO _soalSoal;
    [SerializeField] private UI_Pertanyaan _tempatPertanyaan;
    [SerializeField] private UI_Jawaban[] _tempatPilihanJawaban;

    private int _indexSoal = 0;

    private void Start() {
        // Play BGM
        BGM.Instance.PlayKuisBGM();

        UI_Jawaban.OnAnyAnswerSelected += UI_Jawaban_OnAnyAnswerSelected;

        InitLevelData();
    }

    private void OnDestroy() {
        UI_Jawaban.OnAnyAnswerSelected -= UI_Jawaban_OnAnyAnswerSelected;
    }

    private void UI_Jawaban_OnAnyAnswerSelected(string jawaban, bool isCorrect) {
        if (!isCorrect) return;

        if (_indexSoal >= GetLatestLevelPlayed()) {
            int coinToAdd = 20;

            PlayerProgressManager.Instance.AddPlayerCoin(coinToAdd);
            PlayerProgressManager.Instance.SavePlayerProgress();
        }
    }

    private void InitLevelData() {
        _soalSoal = _selectedLevelSO.SelectedLevelPack;
        _indexSoal = _selectedLevelSO.levelIndex;

        SetLevelQuestion();
    }

    private void SetLevelQuestion() {
        // Ambil data pertanyaan
        LevelKuisSO soal = _soalSoal.AmbilLevelKe(_indexSoal);

        string judulLevel = $"Level {_indexSoal+1}";
        // Set informasi soal
        _tempatPertanyaan.SetPertanyaan(judulLevel, soal.pertanyaan, soal.hint);

        for (int i = 0; i < _tempatPilihanJawaban.Length; i++) {
            UI_Jawaban poin = _tempatPilihanJawaban[i];
            LevelKuisSO.OpsiJawaban opsi = soal.opsiJawaban[i];
            poin.SetJawaban(opsi.jawabanTeks, opsi.adalahBenar);
        }
    }

    private void SetLatestPlayerLevel() {
        _selectedLevelSO.levelIndex = _indexSoal;

        if (GetLatestLevelPlayed() < _indexSoal) {
            PlayerProgressManager.Instance.SetLevelPackLevelProgress(_selectedLevelSO.SelectedLevelPack.LevelPackName, _indexSoal);
            PlayerProgressManager.Instance.SavePlayerProgress();
        }
    }

    private int GetLatestLevelPlayed() {
        return PlayerProgressManager.Instance.GetLevelPackLevelProgressByName(_selectedLevelSO.SelectedLevelPack.LevelPackName);
    }

    public void NextLevel() {
        // Soal index selanjutnya
        _indexSoal++;
        SetLatestPlayerLevel();

        // Jika index melampaui soal terakhir, ulangi dari awal
        if (_indexSoal >= _soalSoal.BanyakLevel) {
            _indexSoal = 0;
        }

        SetLevelQuestion();
    }

    public void ReturnToLevelSelect() {
        Loader.LoadScene(Loader.TargetScene.SelectLevelScene);
    }

    public void RestartLevel() {
        Loader.LoadScene(Loader.TargetScene.GameScene);
    }
}

