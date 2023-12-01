using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

[CreateAssetMenu(
    fileName = "Player Progress Baru",
    menuName = "Game Kuis/Player Progress"
)]
public class PlayerProgressSO : ScriptableObject {
    [System.Serializable]
    public struct MainData {
        public int coin;
        public Dictionary<string, int> progressLevel;
    }

    public MainData progressData = new MainData();
    public string fileNameLatihan = "playerprogressLatihan.txt";
    public string fileName = "playerprogress.txt";
    public string fileNameBinaryWriter = "playerProgressBinaryWriter.txt";

    public void SavePlainProgressExercise() {
        // Sampel Data
        progressData.coin = 200;
        if (progressData.progressLevel == null) progressData.progressLevel = new();
        progressData.progressLevel.Add("Level Pack 1", 3);
        progressData.progressLevel.Add("Level Pack 3", 5);

        string directory = Application.dataPath + "/Temporary/";
        string path = directory + fileNameLatihan;

        // Cek apakah directory belum tersedia
        if (!Directory.Exists(directory)) {
            // Membuat directory baru
            Directory.CreateDirectory(directory);
            Debug.Log("Directory Created: " + directory);
        }

        // Cek apakah file belum tersedia
        if (!File.Exists(path)) {
            File.Create(path).Dispose();
            Debug.Log("File Created: " + fileNameLatihan);
        }

        // Menulis data ke dalam file
        string data = $"{progressData.coin}\n";
        foreach (var item in progressData.progressLevel) {
            data += $"{item.Key}-{item.Value};";
        }
        File.WriteAllText(path, data);
    }

    public void SaveProgressWithBinaryWriter() {
        // Data to save
        progressData.coin = 200;
        if (progressData.progressLevel == null) progressData.progressLevel = new();
        progressData.progressLevel.Add("Level Pack 1", 3);
        progressData.progressLevel.Add("Level Pack 3", 5);

        // Directory & file path
        string directory = Application.dataPath + "/Temporary/";
        string path = directory + fileNameBinaryWriter;

        // Check if there is no saving directory
        if (!Directory.Exists(directory)) {
            // Create new saving directory
            Directory.CreateDirectory(directory);
        }

        // Check if there is no saving file
        if (!File.Exists(path)) {
            // Create new saving file
            File.Create(path).Dispose();
        }

        var fileStream = File.Open(path, FileMode.Open);
        fileStream.Flush();

        var writer = new BinaryWriter(fileStream);

        // Save player coin
        writer.Write(progressData.coin);
        Debug.Log("ProgressData.Coin Saved!");

        // Save progress level
        writer.Write(progressData.progressLevel.Count);
        Debug.Log("ProgressData.progressLevel.Count Saved!");
        foreach (var item in progressData.progressLevel) {
            writer.Write(item.Key);
            writer.Write(item.Value);
        }
        Debug.Log("ProgressData Saved!");

        // Dispose
        writer.Dispose();
        fileStream.Dispose();
    }

    public bool LoadProgressWithBinaryReader() {
        try {
            string directory = Application.dataPath + "/Temporary/";
            string path = directory + fileNameBinaryWriter;

            if (File.Exists(path)) {
                var fileStream = File.Open(path, FileMode.Open);
                var reader = new BinaryReader(fileStream);

                progressData.coin = reader.ReadInt32();
                Debug.Log($"PlayerCoin: {progressData.coin}");

                int progressLevelCount = reader.ReadInt32();
                Debug.Log($"ProgressLevelCount: {progressLevelCount}");

                progressData.progressLevel = new Dictionary<string, int>();
                for (int i = 0; i < progressLevelCount; i++) {
                    string levelName = reader.ReadString();
                    int levelProgress = reader.ReadInt32();
                    progressData.progressLevel.Add(levelName, levelProgress);
                }

                foreach (var item in progressData.progressLevel) {
                    Debug.Log($"{item.Key}; {item.Value}");
                }

                // Dispose
                reader.Dispose();
                fileStream.Dispose();

                return true;
            } else {
                Debug.Log("ERROR: File does not exist!");
                return false;
            }
        } catch (System.Exception e) {
            Debug.Log("ERROR: Failed to Load File!\n" + e.Message);
            return false;
        }
    }

    public void SimpanProgress() {
        // Sampel Data
        progressData.coin = 200;
        if (progressData.progressLevel == null) progressData.progressLevel = new();
        progressData.progressLevel.Add("Level Pack 1", 3);
        progressData.progressLevel.Add("Level Pack 3", 5);

        // Informasi penyimpanan data
        string directory = Application.dataPath + "/Temporary/";
        string path = directory + fileName;

        // Membuat Directory Temporary
        if (!Directory.Exists(directory)) {
            Directory.CreateDirectory(directory);
            Debug.Log("Directory Created!: " + directory);
        }

        // Membuat file baru
        if (!File.Exists(path)) {
            File.Create(path).Dispose();
            Debug.Log("File Created!: " + path);
        }

        // Menyimpan data ke dalam file menggunakan binari
        var fileStream = File.Open(path, FileMode.Open);
        var formatter = new BinaryFormatter();

        fileStream.Flush();
        formatter.Serialize(fileStream, progressData);

        // Putuskan aliran memori dengan file
        fileStream.Dispose();

        Debug.Log("Data saved to file: " + path);
    }

    public bool MuatProgress() {
        // Informasi untuk memuat data
        string directory = Application.dataPath + "/Temporary/";
        string path = directory + fileName;

        try {
            // Memuat data dari file menggunakan binari formatter
            var fileStream = File.Open(path, FileMode.Open);

            var formatter = new BinaryFormatter();

            progressData = (MainData)formatter.Deserialize(fileStream);
            fileStream.Dispose();

            Debug.Log($"{progressData.coin}; {progressData.progressLevel.Count}");

            foreach (var item in progressData.progressLevel) {
                Debug.Log($"{item.Key}:{item.Value}");
            }

            return true;
        } catch (System.Exception e) {
            Debug.Log($"ERROR: Terjadi kesalahan saat membuat progress\n{e.Message}");

            return false;
        }
    }
}

