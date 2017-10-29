using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ShopManager : MonoBehaviour
{
    [SerializeField] private GameObject turretInfoPanel;

    private List<GameObject> displayTurrets;

    [SerializeField] private Text turretNameText;
    [SerializeField] private Text turretPriceText;
    [SerializeField] private Text turretDamageText;
    [SerializeField] private Text turretRangeText;
    [SerializeField] private Text turretRateOfFireText;
    [SerializeField] private Text turretTurnSpeedText;

    public TurretStats standardTurret;
    public TurretStats advancedTurret;
    public TurretStats standardMissileTurret;
    public TurretStats advancedMissileTurret;
    public TurretStats standardLaserTurret;
    public TurretStats advancedLaserTurret;

    private TurretManager turretManager;
    public bool turretPlacementActive = false;

    public static ShopManager instance;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one ShopManager in the scene!");
        }

        instance = this;
    }

    void Start()
    {
        InitializeDisplayTurrets();
        turretManager = TurretManager.instance;
        turretInfoPanel.SetActive(false);
        HideAllTurretDisplays();
    }

    void InitializeDisplayTurrets()
    {
        displayTurrets = new List<GameObject>();
        displayTurrets.Add(standardTurret.turretDisplay);
        displayTurrets.Add(advancedTurret.turretDisplay);
        displayTurrets.Add(standardMissileTurret.turretDisplay);
        displayTurrets.Add(advancedMissileTurret.turretDisplay);
        //displayTurrets.Add(standardLaserTurret.turretDisplay);
        //displayTurrets.Add(advancedLaserTurret.turretDisplay);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            turretManager.HidePlacePath();
        }
    }

    public void TurretPlaced()
    {
        turretPlacementActive = false;
        HideAllTurretDisplays();
    }

    public void HoverOverTurret(TurretStats turret)
    {
        if (turretPlacementActive)
        {
            return;
        }

        turretInfoPanel.SetActive(true);
        turretNameText.text = turret.displayName;
        turretPriceText.text = string.Format("Price: ${0}", turret.cost);
        turretDamageText.text = string.Format("Damage: {0}", turret.damage.ToString());
        turretRangeText.text = string.Format("Range: {0}", turret.range.ToString());
        turretRateOfFireText.text = string.Format("Rate of Fire: {0}", turret.rateOfFire.ToString());
        turretTurnSpeedText.text = string.Format("Turn Speed: {0}", turret.turnSpeed.ToString());
    }

    public void StandardTurretIconPointerEnter()
    {
        HoverOverTurret(standardTurret);
    }

    public void AdvancedTurretIconPointerEnter()
    {
        HoverOverTurret(advancedTurret);
    }

    public void MissileTurretIconPointerEnter()
    {
        HoverOverTurret(standardMissileTurret);
    }

    public void AdvancedMissileTurretIconPointerEnter()
    {
        HoverOverTurret(advancedMissileTurret);
    }

    public void LaserTurretIconPointerEnter()
    {
        HoverOverTurret(standardLaserTurret);
    }

    public void AdvancedLaserTurretIconPointerEnter()
    {
        HoverOverTurret(advancedLaserTurret);
    }

    public void IconPointerExit()
    {
        turretInfoPanel.SetActive(false);
    }

    void HideAllTurretDisplays()
    {
        displayTurrets.ForEach(g => g.SetActive(false));
    }

    void ActivatePlacement(TurretStats turret)
    {
        HideAllTurretDisplays();
        turret.turretDisplay.SetActive(true);

        turretPlacementActive = true;
        turretInfoPanel.SetActive(false);
        turretManager.SelectTurretToBuild(turret);
    }

    public void ActivateStandardTurretPlacement()
    {
        ActivatePlacement(standardTurret);
    }

    public void ActivateAdvancedTurretPlacement()
    { 
        ActivatePlacement(advancedTurret);
    }

    public void ActivateMissileTurretPlacement()
    {
        ActivatePlacement(standardMissileTurret);
    }

    public void ActivateAdvancedMissileTurretPlacement()
    {
        ActivatePlacement(advancedMissileTurret);
    }

    public void ActivateLaserTurretPlacement()
    {
        ActivatePlacement(standardLaserTurret);
    }

    public void ActivateAdvancedLaserTurretPlacement()
    {
        ActivatePlacement(advancedLaserTurret);
    }

    public void DeActivateTurretPlacement()
    {
        HideAllTurretDisplays();
        turretPlacementActive = false;
    }
}