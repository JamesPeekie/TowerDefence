using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class TurretManager : MonoBehaviour
{
	[SerializeField] private GameObject placePath;
	[SerializeField] private Text inUseText;
	[SerializeField] private Text brokeText;
	[SerializeField] private Text removeCostText;

	public static TurretManager singleton;
	private TurretStats turretToBuild;
	
	void Awake ()
	{
		if (singleton != null)
		{
			Debug.LogError("Multiple TurretManagers");
			return; 
		}

		singleton = this; 
	}
	
	void Start()
	{
		placePath.SetActive(false);
		inUseText.gameObject.SetActive(false);
		brokeText.gameObject.SetActive(false);
		removeCostText.gameObject.SetActive(false);
	}

	public void ShowInUseMessage() // Is called when a node is already occupied by a turret
	{
		inUseText.gameObject.SetActive(true); // Activates text telling the player the turret space is in use
		inUseText.gameObject.GetComponent<Animation>().Play(); // Plays the animation for the text activated
		HidePlacePath();
	}

	public void BuildTurretOn(Node node) 
	{
		removeCostText.text = string.Format("-${0}", Mathf.RoundToInt(turretToBuild.cost).ToString()); // Sets text showing the player how much money has been removed

		if (PlayerManager.money < turretToBuild.cost) // Checks if you have no money left.
		{
			brokeText.gameObject.SetActive(true);
			brokeText.gameObject.GetComponent<Animation>().Play();
			HidePlacePath();
			return;
		}
		
		PlayerManager.money -= turretToBuild.cost;
		removeCostText.gameObject.SetActive(true);
		removeCostText.gameObject.GetComponent<Animation>().Play();
		GameObject turret = Instantiate(turretToBuild.turretPrefab, node.GetBuildPosition(), Quaternion.identity);
		node.turret = turret;
		HidePlacePath();
	}

	public bool CanBuild // Variable set to result of whether the turret is not empty
	{
		get
		{
			return turretToBuild != null;
		}
	}

	public bool HasMoney
	{
		get
		{
			return PlayerManager.money >= turretToBuild.cost;
		}
	}

	public void SelectTurretToBuild(TurretStats turret) // Vets the turret currently defined by this script as the listing for the stats script
	{
		turretToBuild = turret;
		placePath.SetActive(true);
	}

    public void HidePlacePath()
    {
        placePath.SetActive(false);
        FindObjectOfType<ShopManager>().DeActivateTurretPlacement(); // Searches the scene for an object that has the script on it, and returns it with all its assigned values
    }
}
