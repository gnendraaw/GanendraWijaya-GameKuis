using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour {
    [SerializeField] private Button _startButton;

    private void Awake() {
        _startButton.onClick.AddListener(() => {
            Loader.LoadScene(Loader.TargetScene.SelectLevelScene);
        });
    }
}

