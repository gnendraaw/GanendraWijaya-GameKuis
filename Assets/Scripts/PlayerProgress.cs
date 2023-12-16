using System;
using System.Collections;
using System.Collections.Generic;

[Serializable]
public class PlayerProgress {
    public PlayerProgress(int coins) {
        this.coins = coins;
        levelProgress = new Dictionary<string, int>();
    }

    public int coins;
    public Dictionary<string, int> levelProgress;
}
