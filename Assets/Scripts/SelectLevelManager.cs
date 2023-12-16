using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectLevelManager : MonoBehaviour {
    public static event Action<string, Action, bool> OnUnlockLevel;
    public static event Action OnUnlockSucceess; 

    [SerializeField] private SelectedLevelSO _selectedLevelSO;
    [SerializeField] private List<LevelPackKuisSO> _levelPackSOList;

    public SelectedLevelSO SelectedLevelSO { get => _selectedLevelSO; }

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

    public void UnlockLevel(string levelPackName, int price) {
        string message;

        // Check if player has enough coin to unlock level
        if (PlayerProgressManager.Instance.PlayerProgress.coins < price) {
            // Show unlock level failed message due to no enough coins
            message = "NO ENOUGH COINS";
            OnUnlockLevel?.Invoke(message, () => {}, false);
            return;
        }

        // Player has enough coins to unlock level
        message = "PURCHASE THIS LEVEL?";
        OnUnlockLevel?.Invoke(message, () => ProcessUnlockLevel(levelPackName, price), true);
    }

    private void ProcessUnlockLevel(string levelPackName, int price) {
        PlayerProgressManager.Instance.PlayerProgress.coins -= price;
        PlayerProgressManager.Instance.PlayerProgress.levelProgress[levelPackName] = 0;
        PlayerProgressManager.Instance.SavePlayerProgress();

        OnUnlockSucceess?.Invoke();
    }

    public SelectedLevelSO GetSelectedLevelSO() {
        return _selectedLevelSO;
    }
}

