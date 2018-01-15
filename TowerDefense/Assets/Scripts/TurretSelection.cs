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
        gameObject.SetActive(true);
        target = selectedNode;

        transform.position = target.GetBuildPosition();
        anim.Play("TurretUiSpawnIn");
    }
    
    public void Upgrade()
    {
        Debug.Log("Upgrade");
        target.UpgradeTurret();
    }

    public void Sell()
    {
        Debug.Log("Sell!");
        target.SellTurret();
    }
}