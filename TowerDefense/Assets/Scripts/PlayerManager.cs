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
<<<<<<< HEAD

<<<<<<< HEAD
    public static int score;
    public int startScore;
    [SerializeField] private Text scoreText;
=======
>>>>>>> 1133206852a5749fd5f2b763f24b5a6745149ebf

=======
>>>>>>> parent of a2f158d... Basic Score System implimentation
    private void Start()
    {
        money = walletMoney;
        lives = initialLives;
        healthText.gameObject.SetActive(false);
        addCostText.gameObject.SetActive(false);
    }

	void Update ()
    {
		walletText.text = string.Format("${0}", Mathf.RoundToInt(money).ToString());
        healthText.text = string.Format("♥{0}", Mathf.RoundToInt(lives).ToString());
    }
    public void RewardCurrency(int rewardValue)
    {
        money += rewardValue;
        addCostText.text = string.Format("+${0}", Mathf.RoundToInt(rewardValue).ToString());
        addCostText.gameObject.SetActive(true);
        addCostText.gameObject.GetComponent<Animation>().Play();
    }
<<<<<<< HEAD
<<<<<<< HEAD

    public void addScore(int scoreValue)
    {
        score += scoreValue;
    }
}

=======
}
>>>>>>> 1133206852a5749fd5f2b763f24b5a6745149ebf
=======
}
>>>>>>> parent of a2f158d... Basic Score System implimentation
