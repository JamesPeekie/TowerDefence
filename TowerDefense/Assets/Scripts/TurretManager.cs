using UnityEngine;
using UnityEngine.UI;

public class TurretManager : MonoBehaviour
{
	[SerializeField] private GameObject placePath;
	[SerializeField] private Text brokeText;
	[SerializeField] private Text removeCostText;

    public TurretSelection turretSelection;
	public static TurretManager singleton;
	private TurretStats turretToBuild;
    private Node selectedNode;

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
		brokeText.gameObject.SetActive(false);
		removeCostText.gameObject.SetActive(false);
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
		Turret turretComponent = turret.GetComponent<Turret>();
		turretComponent.SetNode(node);
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

    public void SelectNode(Node node)
    {
        selectedNode = node;
        placePath.SetActive(false);
        FindObjectOfType<ShopManager>().DeActivateTurretPlacement();
    }

    public void SelectTurretToBuild(TurretStats turret) // Vets the turret currently defined by this script as the listing for the stats script
	{
		turretToBuild = turret;
		placePath.SetActive(true);
        selectedNode = null;
    }

	public void HidePlacePath()
	{
		placePath.SetActive(false);
		FindObjectOfType<ShopManager>().DeActivateTurretPlacement();
        selectedNode = null;
	}
}