using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelPackUI : MonoBehaviour {
    public static LevelPackUI Instance { get; private set; }

    [SerializeField] private Transform _levelPackContainer;
    [SerializeField] private Transform _levelPackTemplate;

    private void Awake() {
        Instance = this;
    }

    private void Start() {
        LevelPackSingleUI.OnAnyLevelPackSelected += LevelPackSingleUI_OnAnyLevelPackSelected;
        LevelUI.OnCloseButtonClicked += LevelUI_OnCloseButtonClicked;

        _levelPackTemplate.gameObject.SetActive(false);

        UpdateLevelPackVisual();
    }

    private void LevelUI_OnCloseButtonClicked() {
        Show();
    }

    private void LevelPackSingleUI_OnAnyLevelPackSelected() {
        Hide();
    }

    private void UpdateLevelPackVisual() {
        foreach (Transform child in _levelPackContainer) {
            if (child == _levelPackTemplate) continue;
            Destroy(child.gameObject);
        }

        foreach (LevelPackKuisSO levelPackSO in SelectLevelManager.Instance.GetLevelPackSOList()) {
            Transform levelPack = Instantiate(_levelPackTemplate, _levelPackContainer);
            levelPack.GetComponent<LevelPackSingleUI>().SetLevelPackData(levelPackSO);
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

