using UnityEngine;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    string healthKey = "playerHealth"; // Player Health Key
    string chancesKey = "playerChances"; // Player Chances Key
    string coinsKey = "coinsCollected"; // Coins Collected Key
    string enemiesKey = "enemiesDefeated"; // Enemies Defeated Key
    string durationKey = "timePlayed"; // Duration Key

    [SerializeField] Text coinText;
    [SerializeField] Text livesLeft;
    [SerializeField] Transform whereToStart;

    private void Awake()
    {
        int health = PlayerPrefs.GetInt(healthKey);
        int chances = PlayerPrefs.GetInt(chancesKey);
        int coins = PlayerPrefs.GetInt(coinsKey);
        int enemies = PlayerPrefs.GetInt(enemiesKey);
        int duration = PlayerPrefs.GetInt(durationKey);

        // Print all PlayerPrefs data for debugging purposes
        Debug.Log("-------------------------------------");
        Debug.Log("------Printing PlayerPrefs data------");
        Debug.Log("Player health:  " + health);
        Debug.Log("Player chances:  " + chances);
        Debug.Log("Total coins:  " + coins);
        Debug.Log("Enemies defeated:  " + enemies);
        Debug.Log("Time so far:  " + duration);
        Debug.Log("-------------------------------------");

        PlayerLife.instance.chances = chances;
        PlayerLife.instance.currentHealth = health;
        PlayerLife.instance.transform.position = whereToStart.position;

        coinText.text = "Coins: " + coins;

        if (chances - 1 == -1)
        {
            livesLeft.text = "x0";
        }
        else
        {
            livesLeft.text = "x" + (PlayerLife.instance.chances - 1);
        }
    }
}
