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

    private PlayerProgress playerProgress = new PlayerProgress(0);
    public PlayerProgress PlayerProgress {
        get => playerProgress;
        set {
            playerProgress = value;
        }
    }

    [SerializeField] private List<LevelPackKuisSO> _levelPackSOList;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
        } else {
            Destroy(gameObject);
        }

        Debug.Log($"{Application.persistentDataPath}/{SAVE_FILE_DIRECTORY}/{SAVE_FILE_NAME}");

        DontDestroyOnLoad(gameObject);

        LoadPlayerProgress();
    }

    private void SetInitialPlayerProgress() {
        // Add level pack progress into player progress
        foreach (LevelPackKuisSO levelPack in _levelPackSOList) {
            PlayerProgress.levelProgress.Add(levelPack.LevelPackName, -1);
        }

        // Save player progress in external file
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

    public void SetLevelPackLevelProgress(string levelPackName, int highestLevelProgress) {
        PlayerProgress.levelProgress[levelPackName] = highestLevelProgress;
    }

    public void AddPlayerCoin(int coinToAdd) {
        PlayerProgress.coins += coinToAdd;
    }

    public int GetLevelPackLevelProgressByName(string name) {
        return PlayerProgress.levelProgress[name];
    }
}

