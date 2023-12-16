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
    [SerializeField] private int _price;

    public int BanyakLevel => _isiLevel.Length;
    public string LevelPackName => _levelPackName;
    public int Price => _price;

    public LevelKuisSO AmbilLevelKe(int index) {
        return _isiLevel[index];
    }
}

