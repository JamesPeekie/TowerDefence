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
		return transform.position + positionOffset;
	}

	void Start()
	{
        nodeRenderer = GetComponent<Renderer>();
        originalColour = nodeRenderer.material.color;
		turretManager = TurretManager.singleton;
	}

	void OnMouseEnter()
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
	
	void OnMouseDown()
	{
		if(!turretManager.CanBuild)
		{
			return;
		}

		if (turret != null)
		{
			Debug.Log("Can't place Turret - Build spot in use!");
			turretManager.ShowInUseMessage();
			return;
		}

		turretManager.BuildTurretOn(this);
		ShopManager.singleton.TurretPlaced();
	}

	void OnMouseExit()
	{
		nodeRenderer.material.color = originalColour;
	}
}
