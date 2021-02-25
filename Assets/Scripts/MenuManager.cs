using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {
    public Button playButton;
    public Button optionsButton;
    public Button quitButton;
    public Button backButton;

    public GameObject MainMenu;
    public GameObject OptionsMenu;

    void Start() {
        // Allows cursor movement
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    void OnEnable() {
        playButton.onClick.AddListener(delegate { PlayGame(); });
        optionsButton.onClick.AddListener(delegate { MenuSwitcher(1); });
        backButton.onClick.AddListener(delegate { MenuSwitcher(2); });
        quitButton.onClick.AddListener(delegate { QuitGame(); });
    }

    // Loads into the game scene
    void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void MenuSwitcher(int i) {
        switch (i) {
            case 1:
                MainMenu.SetActive(false);
                OptionsMenu.SetActive(true);
                break;
            case 2:
                OptionsMenu.SetActive(false);
                MainMenu.SetActive(true);
                break;
            default:
                break;
        }
    }
    // Quits the game
    void QuitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit();
    }
}
