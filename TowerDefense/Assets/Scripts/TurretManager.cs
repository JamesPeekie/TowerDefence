using System;
using UnityEngine;
using UnityEngine.UI;

public class TurretManager : MonoBehaviour
{
    [SerializeField] private GameObject placePath;
    [SerializeField] private Text brokeText;
    [SerializeField] private Text removeCostText;

    public TurretSelection turretSelection;
    public static TurretManager singleton;
    [HideInInspector] public TurretStats turretToBuild;

    void Awake()
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

    public void ShowPlacePath()
    {
        placePath.SetActive(true);
    }

    public void HidePlacePath()
    {
        placePath.SetActive(false);
        FindObjectOfType<ShopManager>().DeActivateTurretPlacement();
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

    public void HandleTurretPurchased(TurretStats turretToBuild)
    {
        removeCostText.text = string.Format("-${0}", Mathf.RoundToInt(turretToBuild.cost).ToString()); // Sets text showing the player how much money has been removed.
        removeCostText.gameObject.SetActive(true);
        removeCostText.gameObject.GetComponent<Animation>().Play();
    }

    public void HandleNotEnoughMoney()
    {
        brokeText.gameObject.SetActive(true);
        brokeText.gameObject.GetComponent<Animation>().Play();
        HidePlacePath();
    }

    public void SelectTurretToBuild(TurretStats turret) // Vets the turret currently defined by this script as the listing for the stats script
    {
        turretToBuild = turret;
        placePath.SetActive(true);
    }
}