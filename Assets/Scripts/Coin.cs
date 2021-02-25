using UnityEngine;

public class Coin : MonoBehaviour {
    public static int CoinCount = 0; // Keeps track of total coin count
    public static bool hasWon;

    void Start() {
        hasWon = false;
    }

    // Start is called before the first frame update
    void Awake() {
        // Adds to the coin count when a coin object is created
        ++CoinCount;
    }



    void Won() {
        GameObject Timer = GameObject.Find("LevelTimer");
        Destroy(Timer);
        GameObject[] FireworkSystems = GameObject.FindGameObjectsWithTag("Fireworks");
        foreach (GameObject Go in FireworkSystems)
        {
            Go.GetComponent<ParticleSystem>().Play();
        }
        hasWon = true;
    }

    void OnTriggerEnter(Collider Col) {
        if (Col.CompareTag("Player")) {
            Debug.Log("Player has touched coin. Hiding object");
            gameObject.SetActive(false);
            --CoinCount;
            Debug.Log("Coins Remaining: " + CoinCount);

            if (CoinCount <= 0) {
                // Won
                Won();
            }
        }
    }
}
