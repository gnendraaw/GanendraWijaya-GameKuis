using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelPackUI : MonoBehaviour {
    private const string TO_LEVELPACKS_PARAM = "ToLevelPacks";
    private const string TO_LEVELS_PARAM = "ToLevels";

    [SerializeField] private SelectLevelManager _selectLevelManager;
    [SerializeField] private Transform _levelPackContainer;
    [SerializeField] private Transform _levelPackTemplate;

    [Header("LEVEL UI ANIMATIONS")]
    [SerializeField] private Animator _animator;

    private void Start() {
        LevelUI.OnLevelUIClosed += LevelUI_OnLevelUIClosed;

        LevelPackSingleUI.OnAnyLevelPackSelected += LevelPackSingleUI_OnAnyLevelPackSelected;

        PlayerProgressManager.OnPlayerProgressUpdated += PlayerProgressManager_OnPlayerProgressUpdated;

        _levelPackTemplate.gameObject.SetActive(false);

        UpdateLevelPackVisual();
    }

    private void LevelUI_OnLevelUIClosed() {
        _animator.SetTrigger(TO_LEVELPACKS_PARAM);
    }

    private void LevelPackSingleUI_OnAnyLevelPackSelected() {
        _animator.SetTrigger(TO_LEVELS_PARAM);
    }

    private void OnDestroy() {
        LevelUI.OnLevelUIClosed -= LevelUI_OnLevelUIClosed;

        LevelPackSingleUI.OnAnyLevelPackSelected -= LevelPackSingleUI_OnAnyLevelPackSelected;

        PlayerProgressManager.OnPlayerProgressUpdated -= PlayerProgressManager_OnPlayerProgressUpdated;
    }

    private void PlayerProgressManager_OnPlayerProgressUpdated() {
        UpdateLevelPackVisual();
    }

    private void UpdateLevelPackVisual() {
        foreach (Transform child in _levelPackContainer) {
            if (child == _levelPackTemplate) continue;
            Destroy(child.gameObject);
        }

        for (int i = 0; i < _selectLevelManager.GetLevelPackSOList().Count; i++) {
            Transform levelPack = Instantiate(_levelPackTemplate, _levelPackContainer);

            LevelPackKuisSO levelPackSO = _selectLevelManager.GetLevelPackSOList()[i];

            bool isLevelLocked = false;
            if (i != 0) isLevelLocked = PlayerProgressManager.Instance.GetLevelPackLevelProgressByName(levelPackSO.LevelPackName) < 0;

            // Set the first level pack to unlocked
            levelPack.GetComponent<LevelPackSingleUI>().SetLevelPackData(levelPackSO, isLevelLocked);
            levelPack.gameObject.SetActive(true);
        }
    }

    public void Show() {
        gameObject.SetActive(true);
    }

    public void Hide() {
        gameObject.SetActive(false);
    }
}

