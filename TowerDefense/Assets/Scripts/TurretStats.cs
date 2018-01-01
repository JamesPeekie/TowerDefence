using UnityEngine;

[System.Serializable] // sets the class to show each turret as a seperate class
public class TurretStats
{
    public string displayName; // The name of the turret that will be displayed.
    public GameObject turretPrefab; // The prefab that is instantiated when the turret it chosen and placed.
    public GameObject turretDisplay; // Shows up when the turret is clicked in the shop.
    public int cost; // The cost of the turret
    public float damage; // The damage the turret does
    public float range; // The range the turret has.
    public float rateOfFire; // The rate of fire of the turret.
    public float turnSpeed; // The turn speed of the turret.
}