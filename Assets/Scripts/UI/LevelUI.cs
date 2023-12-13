using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUI : MonoBehaviour {
    public static LevelUI Instance { get; private set; }

    public static event Action OnCloseButtonClicked;

    [SerializeField] private Button _closeButton;
    [SerializeField] private Transform _levelContainer;
    [SerializeField] private Transform _levelTemplate;

    private void Awake() {
        Instance = this;

        _closeButton.onClick.AddListener(() => {
            OnCloseButtonClicked?.Invoke();
            Hide();
        });
    }

    private void Start() {
        LevelPackSingleUI.OnAnyLevelPackSelected += LevelPackSingleUI_OnAnyLevelPackSelected;

        _levelTemplate.gameObject.SetActive(false);

        Hide();
    }

    private void LevelPackSingleUI_OnAnyLevelPackSelected() {
        UpdateLevelVisual();
        Show();
    }

    private void UpdateLevelVisual() {
        foreach (Transform child in _levelContainer) {
            if (child == _levelTemplate) continue;
            Destroy(child.gameObject);
        }

        int levelCount = SelectLevelManager.Instance.SelectedLevelSO.SelectedLevelPack.BanyakLevel;
        for (int i = 0; i < levelCount; i++) {
            Transform level = Instantiate(_levelTemplate, _levelContainer);
            level.GetComponent<LevelSingleUI>().SetLevelData(i);
            level.gameObject.SetActive(true);
        }
    }

    public void Show() {
        gameObject.SetActive(true);
    }

    private void Hide() {
        gameObject.SetActive(false);
    }
}

