using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(
    fileName = "Selected Level",
    menuName = "Game Kuis/Selected Level"
)]
public class SelectedLevelSO : ScriptableObject {
    public LevelPackKuisSO SelectedLevelPack;
    public int levelIndex;
}

