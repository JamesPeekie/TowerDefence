using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
	public static int money;
	public int walletMoney; 
	[SerializeField] private Text walletText;
	[SerializeField] private Text addCostText;

	public static int lives;
	public int initialLives;
	[SerializeField] private Text healthText;

	public static int score;
	public int startScore;
	[SerializeField] private Text scoreText;

	private void Start()
	{
		money = walletMoney;
		lives = initialLives;
		score = startScore;
		healthText.gameObject.SetActive(false);
		addCostText.gameObject.SetActive(false);
	}

	void Update ()
	{
		walletText.text = string.Format("${0}", Mathf.RoundToInt(money).ToString());
		healthText.text = string.Format("♥{0}", Mathf.RoundToInt(lives).ToString());
		scoreText.text = string.Format("Score = {0}", Mathf.RoundToInt(score).ToString());
	}
	
	public void RewardCurrency(int rewardValue)
	{
		money += rewardValue;
		addCostText.text = string.Format("+${0}", Mathf.RoundToInt(rewardValue).ToString());
		addCostText.gameObject.SetActive(true);
		addCostText.gameObject.GetComponent<Animation>().Play();
	}

	public void addScore(int scoreValue)
	{
		score += scoreValue;
	}
}
