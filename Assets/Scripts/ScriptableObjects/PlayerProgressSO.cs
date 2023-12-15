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

    public void SimpanProgress() {
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

