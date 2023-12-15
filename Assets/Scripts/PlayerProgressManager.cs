using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProgressManager : MonoBehaviour {
    public static PlayerProgressManager Instance { get; private set; }

    public static event Action OnPlayerProgressUpdated;

    [SerializeField] private PlayerProgressSO _playerProgressSO;

    public PlayerProgressSO PlayerProgressSO { get => _playerProgressSO; }

    private void Awake() {
        if (Instance == null) {
            Instance = this;
        } else {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);

        // Check apabila tidak berhasil memuat progress
        if (!_playerProgressSO.MuatProgress()) {
            // Membuat simpanan progress atau mengganti dengan yang baru
            SavePlayerProgress();
        }
    }

    public void SavePlayerProgress() {
        _playerProgressSO.SimpanProgress();
        OnPlayerProgressUpdated?.Invoke();
    }
}

