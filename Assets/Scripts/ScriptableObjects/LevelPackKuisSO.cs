using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(
    fileName = "Level Pack Baru",
    menuName = "Game Kuis/Level Pack"
)]
public class LevelPackKuisSO : ScriptableObject {
    [SerializeField] private string _levelPackName;
    [SerializeField] private LevelKuisSO[] _isiLevel = new LevelKuisSO[0];

    public int BanyakLevel => _isiLevel.Length;
    public string LevelPackName { get => _levelPackName; }

    public LevelKuisSO AmbilLevelKe(int index) {
        return _isiLevel[index];
    }
}

