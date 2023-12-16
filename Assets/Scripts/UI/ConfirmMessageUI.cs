using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ConfirmMessageUI : MonoBehaviour {
    [SerializeField] private GameObject _successOptions;
    [SerializeField] private GameObject _failedOptions;
    [SerializeField] private Button _confirmButton;
    [SerializeField] private Button _cancelButton;
    [SerializeField] private Button _closeButton;
    [SerializeField] private TextMeshProUGUI _messageText;

    private void Awake() {
        _cancelButton.onClick.AddListener(() => Hide());
        _closeButton.onClick.AddListener(() => Hide());
    }

    private void Start() {
        SelectLevelManager.OnUnlockLevel += SelectLevelManager_OnUnlockLevel;
        SelectLevelManager.OnUnlockSucceess += SelectLevelManager_OnUnlockSuccess;

        Hide();
    }

    private void OnDestroy() {
        SelectLevelManager.OnUnlockLevel -= SelectLevelManager_OnUnlockLevel;
        SelectLevelManager.OnUnlockSucceess -= SelectLevelManager_OnUnlockSuccess;
    }

    private void SelectLevelManager_OnUnlockSuccess() {
        Hide();
    }

    private void SelectLevelManager_OnUnlockLevel(string message, Action confirmCallback, bool isSuccess) {
        _messageText.text = message;

        _failedOptions.SetActive(!isSuccess);
        _successOptions.SetActive(isSuccess);

        _confirmButton.onClick.RemoveAllListeners();
        _confirmButton.onClick.AddListener(() => confirmCallback());

        Show();
    }

    private void Show() {
        gameObject.SetActive(true);
    }

    private void Hide() {
        gameObject.SetActive(false);
    }
}

