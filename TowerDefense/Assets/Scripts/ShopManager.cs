using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

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
    [SerializeField] private RawImage turretDisplayRenderTextureImage;
    private RenderTexture turretDisplayRenderTexture;

    public TurretStats standardTurret;
    public TurretStats advancedTurret;
    public TurretStats standardMissileTurret;
    public TurretStats advancedMissileTurret;
    public TurretStats standardLaserTurret;
    public TurretStats advancedLaserTurret;
    public TurretStats standardIceTurret;
    public TurretStats advancedIceTurret;

    private List<TurretStats> allTurrets;

    private TurretManager turretManager;
    public bool turretPlacementActive = false;

    public static ShopManager singleton;

    void Awake()
    {
        if (singleton != null)
        {
            Debug.LogError("Multiple ShopManagers");
            return;
        }

        singleton = this;
    }

    void Start()
    {
        InitializeLists();
        turretManager = TurretManager.singleton;
        turretDisplayRenderTexture = turretDisplayRenderTextureImage.texture as RenderTexture;
        turretInfoPanel.SetActive(false);
        HideAllTurretDisplays();
    }

    void InitializeLists()
    {
        // Display Turrets
        displayTurrets = new List<GameObject>();
        displayTurrets.Add(standardTurret.turretDisplay);
        displayTurrets.Add(advancedTurret.turretDisplay);
        displayTurrets.Add(standardMissileTurret.turretDisplay);
        displayTurrets.Add(advancedMissileTurret.turretDisplay);
        displayTurrets.Add(standardLaserTurret.turretDisplay);
        displayTurrets.Add(advancedLaserTurret.turretDisplay);
        displayTurrets.Add(standardIceTurret.turretDisplay);
        displayTurrets.Add(advancedIceTurret.turretDisplay);

        // TurretStats List
        allTurrets = new List<TurretStats>();
        allTurrets.Add(standardTurret);
        allTurrets.Add(advancedTurret);
        allTurrets.Add(standardMissileTurret);
        allTurrets.Add(advancedMissileTurret);
        allTurrets.Add(standardLaserTurret);
        allTurrets.Add(advancedLaserTurret);
        allTurrets.Add(standardIceTurret);
        allTurrets.Add(advancedIceTurret);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            turretManager.HidePlacePath();
        }

        if (turretPlacementActive)
        {
            turretDisplayRenderTexture.Release();
        }
    }

    public void TurretPlaced()
    {
        turretPlacementActive = false;
        HideAllTurretDisplays();
    }

    public TurretStats GetTurretStatsFor(GameObject turretPrefab)
    {
        return allTurrets.Where(x => x.turretPrefab == turretPrefab).FirstOrDefault();
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

    public void IceTurretIconPointerEnter()
    {
        HoverOverTurret(standardIceTurret);
    }

    public void AdvancedIceTurretIconPointerEnter()
    {
        HoverOverTurret(advancedIceTurret);
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

        turretDisplayRenderTextureImage.gameObject.SetActive(true);

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

    public void ActivateIceTurretPlacement()
    {
        ActivatePlacement(standardIceTurret);
    }

    public void ActivateAdvancedIceTurretPlacement()
    {
        ActivatePlacement(advancedIceTurret);
    }

    public void DeActivateTurretPlacement()
    {
        HideAllTurretDisplays();
        turretPlacementActive = false;
        turretDisplayRenderTextureImage.gameObject.SetActive(false);
    }
}
