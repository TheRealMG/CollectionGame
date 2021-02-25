using UnityEngine;
using UnityEngine.UI;

public class UserInterface : MonoBehaviour {
    public TMPro.TMP_Text timeReamaining;
    public TMPro.TMP_Text coinsRemaining;
    public GameObject winText;

    void Start() {
        winText.SetActive(false);
        //Debug.Log("Has won? " + Coin.hasWon);
        //Debug.Log("win_box Active: " + win_box.activeSelf);
    }

    // Update is called once per frame
    void Update() {
        coinsRemaining.text = "Coins Remaining: " + Coin.CoinCount;
        if (!Coin.hasWon) {
            timeReamaining.text = "Time: " + LevelTimer.GetCountDown().ToString("0") + " Seconds"; // Constantly updates the timer displayed in the UI
        }
        else if (PauseMenu.GameIsPaused) {
            winText.SetActive(false);
        }
        else {
            winText.SetActive(true); // End text is made visible.
        }
    }
}
