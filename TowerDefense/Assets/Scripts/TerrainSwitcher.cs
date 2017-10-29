using UnityEngine;

public class TerrainSwitcher : MonoBehaviour
{
	[SerializeField] private GameObject desert;
	[SerializeField] private GameObject grass;
	[SerializeField] private GameObject ice;

	void Start()
    {
        ActivateTerrain(grass);
	}

    void ActivateTerrain(GameObject terrain)
    {
        HideAllTerrainTypes();
        terrain.SetActive(true);
    }

    void HideAllTerrainTypes()
    {
        grass.SetActive(false);
        desert.SetActive(false);
        ice.SetActive(false);
    }

	public void ActivateGrassTerrain()
    {
        ActivateTerrain(grass);
	}

	public void ActivateDesertTerrain()
    {
        ActivateTerrain(desert);
	}

	public void ActivateIceTerrain()
    {
        ActivateTerrain(ice);
	}
}