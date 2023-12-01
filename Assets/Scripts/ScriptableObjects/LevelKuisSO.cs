using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(
    fileName = "Soal Baru",
    menuName = "Game Kuis/Level Soal Kuis"
)]
public class LevelKuisSO : ScriptableObject {
    [System.Serializable]
    public struct OpsiJawaban {
        public string jawabanTeks;
        public bool adalahBenar;
    }

    public Sprite hint;
    public string pertanyaan;

    public OpsiJawaban[] opsiJawaban = new OpsiJawaban[0];
}

