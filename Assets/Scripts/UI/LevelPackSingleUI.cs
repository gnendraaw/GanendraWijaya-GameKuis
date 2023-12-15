using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelPackSingleUI : MonoBehaviour {
    [SerializeField] private LevelUI _levelUI;
    [SerializeField] private LevelPackUI _levelPackUI;
    [SerializeField] private SelectLevelManager _selectLevelManager;
    [SerializeField] private TextMeshProUGUI _levelPackName;

    private LevelPackKuisSO levelPackSO;

    public void SetLevelPackData(LevelPackKuisSO levelPackKuisSO) {
        this.levelPackSO = levelPackKuisSO;
        _levelPackName.text = levelPackSO.LevelPackName;

        GetComponent<Button>().onClick.AddListener(() => {
            // Change selected level pack
            _selectLevelManager.ChangeSelectedLevelPack(levelPackKuisSO);

            _levelUI.ShowLoadedLevel();
            _levelPackUI.Hide();
        });
    }
}

