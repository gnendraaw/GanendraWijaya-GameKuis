using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUI : MonoBehaviour {
    public static LevelUI Instance { get; private set; }

    [SerializeField] private SelectLevelManager _selectLevelManager;
    [SerializeField] private LevelPackUI _levelPackUI;
    [SerializeField] private Button _closeButton;
    [SerializeField] private Transform _levelContainer;
    [SerializeField] private Transform _levelTemplate;

    private void Awake() {
        Instance = this;

        _closeButton.onClick.AddListener(() => {
            _levelPackUI.Show();
            Hide();
        });
    }

    private void Start() {
        _levelTemplate.gameObject.SetActive(false);

        Hide();
    }

    private void UpdateLevelVisual() {
        foreach (Transform child in _levelContainer) {
            if (child == _levelTemplate) continue;
            Destroy(child.gameObject);
        }

        int levelCount = _selectLevelManager.SelectedLevelSO.SelectedLevelPack.BanyakLevel;
        for (int i = 0; i < levelCount; i++) {
            int highestLevel = PlayerProgressManager.Instance.GetLevelPackLevelProgressByName(_selectLevelManager.SelectedLevelSO.SelectedLevelPack.LevelPackName);
            bool isLocked = i > highestLevel;

            Transform level = Instantiate(_levelTemplate, _levelContainer);
            level.GetComponent<LevelSingleUI>().SetLevelData(i, isLocked);
            level.gameObject.SetActive(true);
        }
    }

    public void Show() {
        gameObject.SetActive(true);
    }

    public void ShowLoadedLevel() {
        UpdateLevelVisual();
        Show();
    }

    private void Hide() {
        gameObject.SetActive(false);
    }
}

