using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_Jawaban : MonoBehaviour {
    public static event Action<string, bool> OnAnyAnswerSelected;

    [SerializeField] private UI_PesanLevel _tempatPesan;
    [SerializeField] private TextMeshProUGUI _teksJawaban;
    [SerializeField] private bool _adalahBenar = false;

    public void PilihJawaban() {
        OnAnyAnswerSelected?.Invoke(_teksJawaban.text, _adalahBenar);
    }

    public void SetJawaban(string teksJawaban, bool adalahBenar) {
        _teksJawaban.text = teksJawaban;
        _adalahBenar = adalahBenar;
    }
}

