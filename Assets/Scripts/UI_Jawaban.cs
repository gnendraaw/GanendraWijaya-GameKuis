using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_Jawaban : MonoBehaviour {
    [SerializeField] private UI_PesanLevel _tempatPesan;
    [SerializeField] private TextMeshProUGUI _teksJawaban;
    [SerializeField] private bool _adalahBenar = false;

    public void PilihJawaban() {
        _tempatPesan.Pesan = $"Jawaban anda adalah {_teksJawaban.text} ({_adalahBenar})";
    }

    public void SetJawaban(string teksJawaban, bool adalahBenar) {
        _teksJawaban.text = teksJawaban;
        _adalahBenar = adalahBenar;
    }
}

