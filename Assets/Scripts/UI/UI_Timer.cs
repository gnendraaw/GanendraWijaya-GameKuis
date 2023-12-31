using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Timer : MonoBehaviour {
    public static event Action OnTimerRunsOut;

    [SerializeField] private Slider _timeBar;
    [SerializeField] private float _waktuJawab = 30f;
    [SerializeField] private UI_PesanLevel _tempatPesan;

    private float _siswaWaktu;
    private bool waktuBerjalan = false;

    public bool WaktuBerjalan {
        get => waktuBerjalan;
        set => waktuBerjalan = value;
    }

    private void Start() {
        UlangiWaktu();
        waktuBerjalan = true;
    }

    private void Update() {
        if (!waktuBerjalan) return;

        _siswaWaktu -= Time.deltaTime;
        _timeBar.value = _siswaWaktu / _waktuJawab;

        if (_siswaWaktu <= 0f) {
            OnTimerRunsOut?.Invoke();
            waktuBerjalan = false;
            return;
        }

        // Debug.Log(_siswaWaktu);
    }

    public void UlangiWaktu() {
        _siswaWaktu = _waktuJawab;
    }
}

