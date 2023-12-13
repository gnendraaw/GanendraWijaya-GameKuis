using UnityEngine.SceneManagement;

public static class Loader {
    public enum TargetScene {
        MainMenuScene,
        SelectLevelScene,
        GameScene,
    }

    public static void LoadScene(TargetScene targetScene) {
        SceneManager.LoadScene(targetScene.ToString());
    }
}

