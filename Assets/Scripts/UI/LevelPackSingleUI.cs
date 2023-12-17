using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelPackSingleUI : MonoBehaviour {
    public static event Action OnAnyLevelPackSelected;

    [SerializeField] private LevelUI _levelUI;
    [SerializeField] private LevelPackUI _levelPackUI;
    [SerializeField] private SelectLevelManager _selectLevelManager;
    [SerializeField] private TextMeshProUGUI _levelPackName;

    [Header("LOCKED LEVEL UI")]
    [SerializeField] private GameObject _lockedUI;
    [SerializeField] private TextMeshProUGUI _priceText;
    private bool isLevelLocked;

    private LevelPackKuisSO levelPackSO;

    public void SetLevelPackData(LevelPackKuisSO levelPackSO, bool isLevelLocked) {
        this.isLevelLocked = isLevelLocked;
        this.levelPackSO = levelPackSO;

        _levelPackName.text = this.levelPackSO.LevelPackName;
        _priceText.text = levelPackSO.Price.ToString();

        // Check if level is locked
        if (isLevelLocked) {
            _lockedUI.SetActive(true);
            SetButtonCallback(UnlockLevelPack);
            return;
        }

        // Level is unlocked
        _lockedUI.SetActive(false);
        SetButtonCallback(SelectLevelPack);
    }

    private void UnlockLevelPack() {
        _selectLevelManager.UnlockLevel(levelPackSO.LevelPackName, levelPackSO.Price);
    }

    private void SelectLevelPack() {
        // Change selected level pack
        _selectLevelManager.ChangeSelectedLevelPack(levelPackSO);

        OnAnyLevelPackSelected?.Invoke();
    }

    private void SetButtonCallback(Action callBack) {
        GetComponent<Button>().onClick.RemoveAllListeners();
        GetComponent<Button>().onClick.AddListener(() => callBack());
    }
}

