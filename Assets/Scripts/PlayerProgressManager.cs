using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class PlayerProgressManager : MonoBehaviour {
    public static PlayerProgressManager Instance { get; private set; }

    public static event Action OnPlayerProgressUpdated;

    private const string SAVE_FILE_NAME = "PlayerProgress.xyz";
    private const string SAVE_FILE_DIRECTORY = "Data";

    private PlayerProgress playerProgress = new PlayerProgress();
    public PlayerProgress PlayerProgress {
        get => playerProgress;
        set {
            playerProgress = value;
        }
    }

    private void Awake() {
        if (Instance == null) {
            Instance = this;
        } else {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);

        LoadPlayerProgress();
    }

    private void SetInitialPlayerProgress() {
        playerProgress.coins = 0;
        SavePlayerProgress();
    }

    public void LoadPlayerProgress() {
        string directory = $"{Application.persistentDataPath}/{SAVE_FILE_DIRECTORY}";
        string path = $"{Application.persistentDataPath}/{SAVE_FILE_DIRECTORY}/{SAVE_FILE_NAME}";

        if (!Directory.Exists(directory)) {
            Directory.CreateDirectory(directory);
            SetInitialPlayerProgress();
            return;
        }

        BinaryFormatter formatter = new BinaryFormatter();

        FileStream stream = new FileStream(path, FileMode.Open);

        playerProgress = formatter.Deserialize(stream) as PlayerProgress;

        stream.Close();

        OnPlayerProgressUpdated?.Invoke();
    }

    public void SavePlayerProgress() {
        string path = $"{Application.persistentDataPath}/{SAVE_FILE_DIRECTORY}/{SAVE_FILE_NAME}";

        BinaryFormatter formatter = new BinaryFormatter();

        FileStream stream = new FileStream(path, FileMode.Create);

        formatter.Serialize(stream, playerProgress);

        stream.Close();

        OnPlayerProgressUpdated?.Invoke();
    }
}

