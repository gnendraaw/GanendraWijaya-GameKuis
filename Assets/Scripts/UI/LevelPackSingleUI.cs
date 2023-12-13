using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelPackSingleUI : MonoBehaviour {
    public static event Action OnAnyLevelPackSelected;

    [SerializeField] private TextMeshProUGUI _levelPackName;

    private LevelPackKuisSO levelPackSO;

    public void SetLevelPackData(LevelPackKuisSO levelPackKuisSO) {
        this.levelPackSO = levelPackKuisSO;
        _levelPackName.text = levelPackSO.LevelPackName;

        GetComponent<Button>().onClick.AddListener(() => {
            // Change selected level pack
            SelectLevelManager.Instance.ChangeSelectedLevelPack(levelPackKuisSO);

            OnAnyLevelPackSelected?.Invoke();
        });
    }
}

