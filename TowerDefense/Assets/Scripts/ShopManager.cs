using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class ShopManager : MonoBehaviour
{
    [SerializeField] private GameObject turretInfoPanel;

    [SerializeField] private GameObject standardTurretDisplay;
    [SerializeField] private GameObject advancedTurretDisplay;
    [SerializeField] private GameObject missileTurretDisplay;
    [SerializeField] private GameObject advancedMissileTurretDisplay;
    [SerializeField] private GameObject laserTurretDisplay;
    [SerializeField] private GameObject advancedLaserTurretDisplay;

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
        turretManager = TurretManager.instance;
        turretInfoPanel.SetActive(false);
        standardTurretDisplay.SetActive(false);
        advancedTurretDisplay.SetActive(false);
        missileTurretDisplay.SetActive(false);
        advancedMissileTurretDisplay.SetActive(false);
        laserTurretDisplay.SetActive(false);
        advancedLaserTurretDisplay.SetActive(false);

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
        standardTurretDisplay.SetActive(false);
        advancedTurretDisplay.SetActive(false);
        missileTurretDisplay.SetActive(false);
        advancedMissileTurretDisplay.SetActive(false);
        laserTurretDisplay.SetActive(false);
        advancedLaserTurretDisplay.SetActive(false);
    }

    public void StandardTurretIconPointerEnter()
    {
        if (turretPlacementActive)
        {
            return;
        }

        turretInfoPanel.SetActive(true);
        turretNameText.text = standardTurret.displayName;
        turretPriceText.text = string.Format("Price: ${0}", standardTurret.cost);
        turretDamageText.text = string.Format("Damage: {0}", standardTurret.damage.ToString());
        turretRangeText.text = string.Format("Range: {0}", standardTurret.range.ToString());
        turretRateOfFireText.text = string.Format("Rate of Fire: {0}", standardTurret.rateOfFire.ToString());
        turretTurnSpeedText.text = string.Format("Turn Speed: {0}", standardTurret.turnSpeed.ToString());
    }

    public void AdvancedTurretIconPointerEnter()
    {
        if (turretPlacementActive)
        {
            return;
        }

        turretInfoPanel.SetActive(true);
        turretNameText.text = advancedTurret.displayName;
        turretPriceText.text = string.Format("Price: ${0}", advancedTurret.cost);
        turretDamageText.text = string.Format("Damage: {0}", advancedTurret.damage.ToString());
        turretRangeText.text = string.Format("Range: {0}", advancedTurret.range.ToString());
        turretRateOfFireText.text = string.Format("Rate of Fire: {0}", advancedTurret.rateOfFire.ToString());
        turretTurnSpeedText.text = string.Format("Turn Speed: {0}", advancedTurret.turnSpeed.ToString());
    }

    public void MissileTurretIconPointerEnter()
    {
        if (turretPlacementActive)
        {
            return;
        }

        turretInfoPanel.SetActive(true);
        turretNameText.text = standardMissileTurret.displayName;
        turretPriceText.text = string.Format("Price: ${0}", standardMissileTurret.cost);
        turretDamageText.text = string.Format("Damage: {0}", standardMissileTurret.damage.ToString());
        turretRangeText.text = string.Format("Range: {0}", standardMissileTurret.range.ToString());
        turretRateOfFireText.text = string.Format("Rate of Fire: {0}", standardMissileTurret.rateOfFire.ToString());
        turretTurnSpeedText.text = string.Format("Turn Speed: {0}", standardMissileTurret.turnSpeed.ToString());
    }

    public void AdvancedMissileTurretIconPointerEnter()
    {
        if (turretPlacementActive)
        {
            return;
        }

        turretInfoPanel.SetActive(true);
        turretNameText.text = advancedMissileTurret.displayName;
        turretPriceText.text = string.Format("Price: ${0}", advancedMissileTurret.cost);
        turretDamageText.text = string.Format("Damage: {0}", advancedMissileTurret.damage.ToString());
        turretRangeText.text = string.Format("Range: {0}", advancedMissileTurret.range.ToString());
        turretRateOfFireText.text = string.Format("Rate of Fire: {0}", advancedMissileTurret.rateOfFire.ToString());
        turretTurnSpeedText.text = string.Format("Turn Speed: {0}", advancedMissileTurret.turnSpeed.ToString());
    }

    public void LaserTurretIconPointerEnter()
    {
        if (turretPlacementActive)
        {
            return;
        }

        turretInfoPanel.SetActive(true);
        turretNameText.text = standardLaserTurret.displayName;
        turretPriceText.text = string.Format("Price: ${0}", standardLaserTurret.cost);
        turretDamageText.text = string.Format("Damage: {0}", standardLaserTurret.damage.ToString());
        turretRangeText.text = string.Format("Range: {0}", standardLaserTurret.range.ToString());
        turretRateOfFireText.text = string.Format("Rate of Fire: {0}", standardLaserTurret.rateOfFire.ToString());
        turretTurnSpeedText.text = string.Format("Turn Speed: {0}", standardLaserTurret.turnSpeed.ToString());
    }

    public void AdvancedLaserTurretIconPointerEnter()
    {
        if (turretPlacementActive)
        {
            return;
        }

        turretInfoPanel.SetActive(true);
        turretNameText.text = advancedLaserTurret.displayName;
        turretPriceText.text = string.Format("Price: ${0}", advancedLaserTurret.cost);
        turretDamageText.text = string.Format("Damage: {0}", advancedLaserTurret.damage.ToString());
        turretRangeText.text = string.Format("Range: {0}", advancedLaserTurret.range.ToString());
        turretRateOfFireText.text = string.Format("Rate of Fire: {0}", advancedLaserTurret.rateOfFire.ToString());
        turretTurnSpeedText.text = string.Format("Turn Speed: {0}", advancedLaserTurret.turnSpeed.ToString());
    }


    public void IconPointerExit()
    {
        turretInfoPanel.SetActive(false);
    }


    public void ActivateStandardTurretPlacement()
    {
        standardTurretDisplay.SetActive(true);
        advancedTurretDisplay.SetActive(false);
        missileTurretDisplay.SetActive(false);
        advancedMissileTurretDisplay.SetActive(false);
        laserTurretDisplay.SetActive(false);
        advancedLaserTurretDisplay.SetActive(false);
        turretPlacementActive = true;
        turretInfoPanel.SetActive(false);
        turretManager.SelectTurretToBuild(standardTurret);
    }

    public void ActivateAdvancedTurretPlacement()
    { 
        standardTurretDisplay.SetActive(false);
        advancedTurretDisplay.SetActive(true);
        missileTurretDisplay.SetActive(false);
        advancedMissileTurretDisplay.SetActive(false);
        laserTurretDisplay.SetActive(false);
        advancedLaserTurretDisplay.SetActive(false);
        turretPlacementActive = true;
        turretInfoPanel.SetActive(false);
        turretManager.SelectTurretToBuild(advancedTurret);
    }

    public void ActivateMissileTurretPlacement()
    {
        standardTurretDisplay.SetActive(false);
        advancedTurretDisplay.SetActive(false);
        missileTurretDisplay.SetActive(true);
        advancedMissileTurretDisplay.SetActive(false);
        laserTurretDisplay.SetActive(false);
        advancedLaserTurretDisplay.SetActive(false);
        turretPlacementActive = true;
        turretInfoPanel.SetActive(false);
        turretManager.SelectTurretToBuild(standardMissileTurret);
    }

    public void ActivateAdvancedMissileTurretPlacement()
    {
        standardTurretDisplay.SetActive(false);
        advancedTurretDisplay.SetActive(false);
        missileTurretDisplay.SetActive(false);
        advancedMissileTurretDisplay.SetActive(true);
        laserTurretDisplay.SetActive(false);
        advancedLaserTurretDisplay.SetActive(false);
        turretPlacementActive = true;
        turretInfoPanel.SetActive(false);
        turretManager.SelectTurretToBuild(advancedMissileTurret);
    }

    public void ActivateLaserTurretPlacement()
    {
        standardTurretDisplay.SetActive(false);
        advancedTurretDisplay.SetActive(false);
        missileTurretDisplay.SetActive(false);
        advancedMissileTurretDisplay.SetActive(false);
        laserTurretDisplay.SetActive(true);
        advancedLaserTurretDisplay.SetActive(false);
        turretPlacementActive = true;
        turretInfoPanel.SetActive(false);
        turretManager.SelectTurretToBuild(advancedMissileTurret);
    }

    public void ActivateAdvancedLaserTurretPlacement()
    {
        standardTurretDisplay.SetActive(false);
        advancedTurretDisplay.SetActive(false);
        missileTurretDisplay.SetActive(false);
        advancedMissileTurretDisplay.SetActive(false);
        laserTurretDisplay.SetActive(false);
        advancedLaserTurretDisplay.SetActive(true);
        turretPlacementActive = true;
        turretInfoPanel.SetActive(false);
        turretManager.SelectTurretToBuild(advancedMissileTurret);
    }

    public void DeActivateTurretPlacement()
    {
        standardTurretDisplay.SetActive(false);
        advancedTurretDisplay.SetActive(false);
        missileTurretDisplay.SetActive(false);
        advancedMissileTurretDisplay.SetActive(false);
        laserTurretDisplay.SetActive(false);
        advancedLaserTurretDisplay.SetActive(false);
        turretPlacementActive = false;
    }


}