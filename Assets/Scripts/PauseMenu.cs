using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    public Button resumeButton;
    public Button menuButton;
    public Button quitButton;

    void OnEnable() {
        resumeButton.onClick.AddListener(delegate { Resume(); });
        menuButton.onClick.AddListener(delegate { LoadMenu(); });
        quitButton.onClick.AddListener(delegate { QuitGame(); });
    }

    // Update is called once per frame
    void Update() {
        // If the escape key is pressed, check if the game is currently paused, then call the correct function
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (GameIsPaused) {
                Resume();
            }
            else {
                Pause();
            }
        }
    }
    // Unpauses the game
    public void Resume() {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }
    // Pauses the game
    void Pause() {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }
    // Goes back to the main menu
    public void LoadMenu()
    {
        Time.timeScale = 1f;
        GameIsPaused = false;
        Debug.Log("Loading menu...");
        SceneManager.LoadScene("Menu");
        Coin.CoinCount = 0;
    }
    // Quits the game
    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit();
    }
}
