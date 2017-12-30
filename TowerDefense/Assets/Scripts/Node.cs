using UnityEngine;

public class Node : MonoBehaviour
{
	[SerializeField] private Color hoverColour;
	[SerializeField] private Color cannotPlaceColour;
	[SerializeField] private Vector3 positionOffset;
	
	public GameObject turret;

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

	public void HandleMouseEnter()
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
	
	public void HandleMouseDown()
	{
		if (turret != null)
		{
            turretManager.SelectNode(this);
			return;
		}

        if (!turretManager.CanBuild)
        {
            return;
        }

        turretManager.BuildTurretOn(this);
		ShopManager.singleton.TurretPlaced();
	}

	public void HandleMouseExit()
	{
		nodeRenderer.material.color = originalColour;
	}

    public void SelectNode()
    {
        turretManager.SelectNode(this);
    }
}
