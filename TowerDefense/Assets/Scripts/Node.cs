﻿using UnityEngine;

public class Node : MonoBehaviour
{
    [SerializeField] private Color hoverColour;
    [SerializeField] private Color cannotPlaceColour;
    [SerializeField] private Vector3 positionOffset;

    [HideInInspector] public GameObject turret;
    [HideInInspector] public TurretStats turretStats;
    [HideInInspector] public bool isUpgraded;

    private Color originalColour;
    private Renderer nodeRenderer;

    private TurretManager turretManager;

    public Vector3 GetBuildPosition()
    {
        return transform.GetChild(0).position + positionOffset;
    }

    void Start()
    {
        nodeRenderer = GetComponentInChildren<Renderer>();
        originalColour = nodeRenderer.material.color;
        turretManager = TurretManager.singleton;
    }

    public void HoverNode()
    {
        if (turretManager.CanBuild && turretManager.HasMoney && turret == null)
        {
            nodeRenderer.material.color = hoverColour;
        }
        else
        {
            nodeRenderer.material.color = cannotPlaceColour;
        }
    }

    public void UnHoverNode()
    {
        nodeRenderer.material.color = originalColour;
    }

    public void BuildTurret()
    {
        TurretStats turretToBuild = turretManager.turretToBuild;
        turretStats = turretToBuild;

        if (!turretManager.CanBuild)
        {
            return;
        }


        if (PlayerManager.money < turretToBuild.cost) // Checks if you have no money left.
        {
            turretManager.HandleNotEnoughMoney();
            return;
        }

        turretManager.HandleTurretPurchased(turretToBuild);

        PlayerManager.money -= turretToBuild.cost;
        GameObject turret = Instantiate(turretToBuild.turretPrefab, GetBuildPosition(), Quaternion.identity);
        Turret turretComponent = turret.GetComponent<Turret>();
        turretComponent.SetNode(this);
        this.turret = turret;
        turretManager.HidePlacePath();

        TurretSelection.singleton.HideUiObject();
        ShopManager.singleton.TurretPlaced();
    }

    public void UpgradeTurret()
    {
        if (turretStats.upgradedPrefab == null) return;

        if (PlayerManager.money < turretStats.upgradeCost) // Checks if you have no money left.
        {
            turretManager.HandleNotEnoughMoney();
            return;
        }

        PlayerManager.money -= turretStats.upgradeCost;

        // Get rid of old turret
        Destroy(this.turret);

        // Build new, upgraded one.
        TurretStats newTurretStats = ShopManager.singleton.GetTurretStatsFor(turretStats.upgradedPrefab);
        GameObject turret = Instantiate(turretStats.upgradedPrefab, GetBuildPosition(), Quaternion.identity);
        Turret turretComponent = turret.GetComponent<Turret>();
        turretComponent.SetNode(this);
        this.turret = turret;
        this.turretStats = newTurretStats;
        turretManager.HidePlacePath();

        isUpgraded = true;

        TurretSelection.singleton.HideUiObject();
        ShopManager.singleton.TurretPlaced();
    }

    public void SellTurret()
    {
        PlayerManager.money += turretStats.cost;
        Destroy(this.turret);
        this.turret = null;
        TurretSelection.singleton.HideUiObject();
    }
}
