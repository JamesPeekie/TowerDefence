using UnityEngine;

public class TurretSelection : MonoBehaviour
{
    private Node target;

    private Animation anim;
    [SerializeField] private GameObject upgradeButton;

    public static TurretSelection singleton;

    void Awake()
    {
        if (singleton != null)
        {
            Debug.LogError("Multiple TurretSelections");
            return;
        }

        singleton = this;
    }

    void Start()
    {
        anim = GetComponent<Animation>();
        HideUiObject();
    }

    public void HideUiObject()
    {
        gameObject.SetActive(false);
        target = null;
    }

    public void HideUpgradeButton(bool tf)
    {
        upgradeButton.SetActive(tf);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            HideUiObject();
        }
    }

    public void SetNodeTarget(Node selectedNode)
    {
        // If the node you've clicked is already selected, unselect it.
        if (target == selectedNode)
        {
            HideUiObject();
            return;
        }

        gameObject.SetActive(true);
        target = selectedNode;

        transform.position = target.GetBuildPosition();
        if (selectedNode.turretStats.upgradedPrefab == null)
        {
            upgradeButton.SetActive(false);
        } else
        {
            upgradeButton.SetActive(true);
        }

        anim.Play("TurretUiSpawnIn");
    }
    
    public void Upgrade()
    {
        target.UpgradeTurret();
    }

    public void Sell()
    {
        target.SellTurret();
    }
}