using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour
{
	[SerializeField] private GameObject enemyPrefab;
	[SerializeField] private Transform spawnPoint;

	[SerializeField] private float timeBetweenWaves = 5f;
	private float countdown = 2f;
	public Text waveCountdownText;
	private int waveNumber = 0;
	[SerializeField] private Color textColour;

	void Start()
	{
		textColour = waveCountdownText.color;
	}

	void Update ()
	{
		countdown -= Time.deltaTime;

		if (countdown <= 3 && countdown > 0)
        {
			waveCountdownText.color = Color.red;
		}
        else if (countdown <= 0)
        {
			StartCoroutine(SpawnWave());
			countdown = timeBetweenWaves;
			waveCountdownText.color = textColour;
		}

		waveCountdownText.text = Mathf.Round(countdown).ToString();
	}

	private IEnumerator SpawnWave()
	{
		waveNumber++;

		for (int i = 0; i < waveNumber; i++) 
		{
			SpawnEnemy();
			yield return new WaitForSeconds(1);
		}
	}
		
	void SpawnEnemy ()
	{
		Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
	}
}