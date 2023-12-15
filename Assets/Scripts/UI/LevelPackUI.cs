using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelPackUI : MonoBehaviour {
    [SerializeField] private SelectLevelManager _selectLevelManager;
    [SerializeField] private Transform _levelPackContainer;
    [SerializeField] private Transform _levelPackTemplate;

    private void Start() {
        _levelPackTemplate.gameObject.SetActive(false);

        UpdateLevelPackVisual();
    }

    private void UpdateLevelPackVisual() {
        foreach (Transform child in _levelPackContainer) {
            if (child == _levelPackTemplate) continue;
            Destroy(child.gameObject);
        }

        foreach (LevelPackKuisSO levelPackSO in _selectLevelManager.GetLevelPackSOList()) {
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

