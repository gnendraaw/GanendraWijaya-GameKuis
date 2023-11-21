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

    [SerializeField] private DataSoal[] _soalSoal;
    [SerializeField] private UI_Pertanyaan _tempatPertanyaan;
    [SerializeField] private UI_Jawaban[] _tempatPilihanJawaban;

    private int _indexSoal = -1;

    private void Start() {
        NextLevel();
    }

    public void NextLevel() {
        // Soal index selanjutnya
        _indexSoal++;

        // Jika index melampaui soal terakhir, ulangi dari awal
        if (_indexSoal >= _soalSoal.Length) {
            _indexSoal = 0;
        }

        // Ambil data pertanyaan
        DataSoal soal = _soalSoal[_indexSoal];

        String judulLevel = $"Level {_indexSoal+1}";
        // Set informasi soal
        _tempatPertanyaan.SetPertanyaan(judulLevel, soal.pertanyaan, soal.hint);

        for (int i = 0; i < _tempatPilihanJawaban.Length; i++) {
            UI_Jawaban poin = _tempatPilihanJawaban[i];
            poin.SetJawaban(soal.jawabanTeks[i], soal.adalahBenar[i]);
        }
    }
}

