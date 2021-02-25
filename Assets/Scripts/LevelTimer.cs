using UnityEngine.SceneManagement;
using UnityEngine;

public class LevelTimer : MonoBehaviour {
    public static float MaxTime = 60f;
    [SerializeField]
    private static float CountDown;

    // Start is called before the first frame update
    void Start() {
        CountDown = MaxTime;
    }

    // Update is called once per frame
    void Update() {
        CountDown -= Time.deltaTime;
        if (CountDown <= 0) {
            Coin.CoinCount = 0;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public static float GetCountDown() {
        return CountDown;
    }
}