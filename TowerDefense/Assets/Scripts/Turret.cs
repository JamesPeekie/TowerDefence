using UnityEngine;

public class Turret : MonoBehaviour
{
	private Transform target; // Singular Enemy found to target

	[Header("Attributes")]
	[SerializeField] private float range = 15f; // Area in which the enemy is detected
	[SerializeField] private float turnSpeed = 10f; // Rate at which the turret turns twords target
	[SerializeField] private float fireRate = 1f; // Delay factor between shots
	[SerializeField] private AudioSource shootSound;
	private float fireCountdown = 0f; // Counts the delay between shots

    [Space]

	[Header("Fixed Assets")]
	[SerializeField] private string enemyTag = "Enemy"; // Game object that has a tag with this name, ensuring only enemy objects are targeted
	[SerializeField] private Transform partToRotate; // Transform that holds the part of the turret that moves.
	[SerializeField] private Transform gunBarrel; // Transform that animates on firing and bullets spawn at.
    [SerializeField] private ParticleSystem impactEffect;
    [SerializeField] private ParticleSystem shootEffect;

    [Space]

    [Header("Bullet")]
	[SerializeField] private GameObject bullet; // Bullet object to spawn on firing

    [Space]

    [Header("Laser")]
    [SerializeField] private bool useLaser = false;
    [SerializeField] private LineRenderer laserDisplay;

	private AudioController audioController; // References the audio script.

	void Start () 
	{
		InvokeRepeating("UpdateTarget", 0f, 0.5f); // Checks for a target on a delay, rather than every frame, for performance
		audioController = AudioController.instance; // Creates a copy of the sound control script to reference to when playing a turret sound
	}

	void UpdateTarget() // Function for finding target
	{
		GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag); // Sets all gameobjects in the scene that have the tag "enemy"
		float shortestDistance = Mathf.Infinity;
		GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies) // Within the list of enemy tagged objects, checks for the closest within the turret range and sets it to nearestenemy
		{
			float distanceToEnemy = Vector3.Distance (transform.position, enemy.transform.position);
			if (distanceToEnemy < shortestDistance)
			{
				shortestDistance = distanceToEnemy;
				nearestEnemy = enemy;
			}
		}

		if (nearestEnemy != null && shortestDistance <= range) // Sets the nearest enemy as the target
        { 
			target = nearestEnemy.transform;
		}
        else
        {
			target = null; // Sets the nearest enemy as blank if none are within range
        }
	}
		
	void Update () 
	{
	    fireCountdown -= Time.deltaTime; // Reduces countdown
        if (target == null)
        {
            if (useLaser)
            {
                if(laserDisplay.enabled)
                {
                    laserDisplay.enabled = false;
                    impactEffect.Stop();
                    shootEffect.Stop();
                }
            }
            return; // Do nothing this frame if no target is found 
        }
        LockOn();

        if (useLaser)
        {
            FireBeam();
        }
        else
        {
            if (fireCountdown <= 0f) // Checks if firecoundown has dropped far enough and allows it to shoot if low enough
            {
                Shoot(); // Fires the shoot function
                fireCountdown = 1f / fireRate; // Resets countdown and applies firerate
            }
        }
	}

    void LockOn()
    {
        // Rotates Gun barrel towards enemy.
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

    void FireBeam()
    {
        if (!laserDisplay.enabled)
        {
            laserDisplay.enabled = true;

            impactEffect.Play();
            shootEffect.Play();
        }
        laserDisplay.SetPosition(0, gunBarrel.position);
        laserDisplay.SetPosition(1, target.position);

        impactEffect.transform.position = target.position;
        shootEffect.transform.position = gunBarrel.position;
        shootEffect.transform.rotation = gunBarrel.rotation;
    }

	void Shoot() // Function that runs on shooting the enemy
	{
		Animation anim = gunBarrel.GetComponent<Animation>(); // Gathers the shooting animation 
		GameObject bulletGO = Instantiate(this.bullet, gunBarrel.position, gunBarrel.rotation); // Spawns bullet at barrel
		Bullet bullet = bulletGO.GetComponent<Bullet>();

		if (bullet != null) 
		{
			bullet.Seek(target);
		}

		anim.Stop(); // Stops any old animations that were playing
		anim.Play("recoil"); // Plays the shooting animation

		audioController.PlaySound("Shoot"); // Play the shoot sound.
	}
}