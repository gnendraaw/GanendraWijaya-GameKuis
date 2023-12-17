using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour {
    [SerializeField] private Button _startButton;

    private void Start() {
        _startButton.onClick.AddListener(() => {
            SFX.Instance.PlayButtonClickSound();
            Loader.LoadScene(Loader.TargetScene.SelectLevelScene);
        });
    }
}

