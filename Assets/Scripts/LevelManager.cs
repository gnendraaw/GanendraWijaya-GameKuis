using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {
    [SerializeField] private SelectedLevelSO _selectedLevelSO;
    [SerializeField] private PlayerProgressSO _playerProgress;
    [SerializeField] private LevelPackKuisSO _soalSoal;
    [SerializeField] private UI_Pertanyaan _tempatPertanyaan;
    [SerializeField] private UI_Jawaban[] _tempatPilihanJawaban;

    private int _indexSoal = 0;

    private void Start() {
        // SAVE AND LOAD WITH BINARY FORMATTER
        // Check apabila tidak berhasil memuat progress
        if (!_playerProgress.MuatProgress()) {
            // Membuat simpanan progress atau mengganti dengan yang baru
            _playerProgress.SimpanProgress();
        }

        InitLevelData();
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

    public void NextLevel() {
        // Soal index selanjutnya
        _indexSoal++;

        // Jika index melampaui soal terakhir, ulangi dari awal
        if (_indexSoal >= _soalSoal.BanyakLevel) {
            _indexSoal = 0;
        }

        SetLevelQuestion();
    }
}

