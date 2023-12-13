using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectLevelManager : MonoBehaviour {
    public static SelectLevelManager Instance { get; private set; }

    [SerializeField] private SelectedLevelSO _selectedLevelSO;
    [SerializeField] private List<LevelPackKuisSO> _levelPackSOList;

    public SelectedLevelSO SelectedLevelSO { get => _selectedLevelSO; }

    public void Awake() {
        Instance = this;
    }

    public List<LevelPackKuisSO> GetLevelPackSOList() {
        return _levelPackSOList;
    }

    public void ChangeSelectedLevelPack(LevelPackKuisSO selectedLevelPack) {
        _selectedLevelSO.SelectedLevelPack = selectedLevelPack;
        Debug.Log("Selected Level Pack: " + _selectedLevelSO.SelectedLevelPack.LevelPackName);
        Debug.Log("Level Count: " + _selectedLevelSO.SelectedLevelPack.BanyakLevel);
    }

    public void ChangeSelectedLevel(int levelIndex) {
        _selectedLevelSO.levelIndex = levelIndex;

        Loader.LoadScene(Loader.TargetScene.GameScene);
    }

    public SelectedLevelSO GetSelectedLevelSO() {
        return _selectedLevelSO;
    }
}

