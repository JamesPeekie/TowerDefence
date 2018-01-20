using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour
{

    public static int enemiesInGame;

    [SerializeField] private Transform spawnPoint;

    public WaveDetails[] waves;

    [SerializeField] private float timeBetweenWaves = 5f;
    private float countdown = 20f;
    public Text waveCountdownText;
    public static int waveNumber = 0;
    [SerializeField] private Color textColour;

    void Start()
    {
        textColour = waveCountdownText.color;
    }

    void Update()
    {
        if (enemiesInGame > 0)
        {
            waveCountdownText.text = " ";
            return;
        }

        if (countdown <= 3 && countdown > 0)
        {
            waveCountdownText.color = Color.red;
        }

        if (waveNumber == waves.Length)
        {
            GameManager.singleton.WinGame();
            this.enabled = false;
        }

        if (countdown <= 0)
        {
            StartCoroutine(SpawnWave());
            countdown = timeBetweenWaves;
            waveCountdownText.color = textColour;
            return;
        }

        countdown -= Time.deltaTime;
        countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity);
        waveCountdownText.text = Mathf.Round(countdown).ToString();

    }

    private IEnumerator SpawnWave()
    {
        WaveDetails wave = waves[waveNumber];
        enemiesInGame = wave.count;
        for (int i = 0; i < wave.count; i++)
        {
            SpawnEnemy(wave.enemy);
            yield return new WaitForSeconds(1f / wave.rate);
        }
        waveNumber++;
    }

    void SpawnEnemy(GameObject enemyType)
    {
        Instantiate(enemyType, spawnPoint.position, spawnPoint.rotation);
    }
}