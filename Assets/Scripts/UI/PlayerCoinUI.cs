using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerCoinUI : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI coinText;

    private void Start() {
        PlayerProgressManager.OnPlayerProgressUpdated += PlayerProgressManager_OnPlayerProgressUpdated;

        UpdateCoinVisual();
    }

    private void OnDestroy() {
        PlayerProgressManager.OnPlayerProgressUpdated -= PlayerProgressManager_OnPlayerProgressUpdated;
    }

    private void PlayerProgressManager_OnPlayerProgressUpdated() {
        UpdateCoinVisual();
    }

    private void UpdateCoinVisual() {
        coinText.text = PlayerProgressManager.Instance.PlayerProgress.coins.ToString();
    }
}

