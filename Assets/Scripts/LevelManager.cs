using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {
    [System.Serializable]
    private struct DataSoal {
        public Sprite hint;
        public string pertanyaan;

        public string[] jawabanTeks;
        public bool[] adalahBenar;
    }

    [SerializeField] private PlayerProgressSO _playerProgress;
    [SerializeField] private LevelPackKuisSO _soalSoal;
    [SerializeField] private UI_Pertanyaan _tempatPertanyaan;
    [SerializeField] private UI_Jawaban[] _tempatPilihanJawaban;

    private int _indexSoal = -1;

    private void Start() {
        // SAVING DATA EXERCISE
        // _playerProgress.SavePlainProgressExercise();


        // SAVE AND LOAD WITH BINARY WRITER READER
        // if (!_playerProgress.LoadProgressWithBinaryReader()) {
        //     _playerProgress.SaveProgressWithBinaryWriter();
        // }


        // SAVE AND LOAD WITH BINARY FORMATTER
        // Check apabila tidak berhasil memuat progress
        if (!_playerProgress.MuatProgress()) {
            // Membuat simpanan progress atau mengganti dengan yang baru
            _playerProgress.SimpanProgress();
        }


        NextLevel();
    }

    public void NextLevel() {
        // Soal index selanjutnya
        _indexSoal++;

        // Jika index melampaui soal terakhir, ulangi dari awal
        if (_indexSoal >= _soalSoal.BanyakLevel) {
            _indexSoal = 0;
        }

        // Ambil data pertanyaan
        LevelKuisSO soal = _soalSoal.AmbilLevelKe(_indexSoal);

        String judulLevel = $"Level {_indexSoal+1}";
        // Set informasi soal
        _tempatPertanyaan.SetPertanyaan(judulLevel, soal.pertanyaan, soal.hint);

        for (int i = 0; i < _tempatPilihanJawaban.Length; i++) {
            UI_Jawaban poin = _tempatPilihanJawaban[i];
            LevelKuisSO.OpsiJawaban opsi = soal.opsiJawaban[i];
            poin.SetJawaban(opsi.jawabanTeks, opsi.adalahBenar);
        }
    }
}

