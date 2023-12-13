using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelSingleUI : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI _levelText;

    public void SetLevelData(int index) {
        // Set level data
        _levelText.text = (index+1).ToString();

        GetComponent<Button>().onClick.AddListener(() => {
            SelectLevelManager.Instance.ChangeSelectedLevel(index);
        });
    }
}

