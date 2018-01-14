using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private bool gameEnded = false;
    public GameObject hideAll;
    public GameObject gameOver;
    public Text title;
    public Text score;
    public Text money;
    public Text wavesLives;

    public static GameManager singleton;

    void Awake()
    {
        if (singleton != null)
        {
            Debug.LogError("Multiple GameManagers");
            return;
        }

        singleton = this;
    }

    void Update()
    {
        if (gameEnded)
        {
            return;
        }

        if (PlayerManager.lives <= 0)
        {
            LoseGame();
        }
    }

    void LoseGame()
    {
        hideAll.SetActive(false);
        gameOver.SetActive(true);
        title.text = "GameOver!";
        score.text = string.Format("Score:{0}", Mathf.RoundToInt(PlayerManager.score).ToString());
        money.text = string.Format("Money:{0}", Mathf.RoundToInt(PlayerManager.money).ToString());
        wavesLives.text = string.Format("Waves:{0}", Mathf.RoundToInt(WaveSpawner.waveNumber).ToString());
        gameEnded = true;
        Time.timeScale = 0;
    }

    public void WinGame()
    {
        hideAll.SetActive(false);
        gameOver.SetActive(true);
        title.text = "Victory!";
        score.text = string.Format("Score:{0}", Mathf.RoundToInt(PlayerManager.score).ToString());
        money.text = string.Format("Money:{0}", Mathf.RoundToInt(PlayerManager.money).ToString());
        wavesLives.text = string.Format("Lives:{0}", Mathf.RoundToInt(PlayerManager.lives).ToString());
        gameEnded = true;
        Time.timeScale = 0;
    }
}
