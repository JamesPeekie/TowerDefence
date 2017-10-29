using UnityEngine;

public class TerrainSwitcher : MonoBehaviour
{
	[SerializeField] private GameObject desert;
	[SerializeField] private GameObject grass;
	[SerializeField] private GameObject ice;

	void Start ()
    {
        grass.SetActive(true); // Activate Grass Terrain by default.
        desert.SetActive(false);
		ice.SetActive (false);
	}

	public void ActivateDesertTerrain()
    {
		desert.SetActive(true);
		grass.SetActive (false);
		ice.SetActive (false);
	}

	public void ActivateGrassTerrain()
    {
        grass.SetActive(true);
        desert.SetActive(false);
		ice.SetActive (false);
	}
	public void ActivateIceTerrain()
    {
        ice.SetActive(true);
        desert.SetActive (false);
		grass.SetActive (false);
	}
}