using UnityEngine;

public class Turret : MonoBehaviour
{
	private Transform target; // Singular Enemy found to target
	private EnemyMovement enemyScript;

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
    public GameObject nodeOn;

    [Space]

	[Header("Bullet")]
	[SerializeField] private GameObject bullet; // Bullet object to spawn on firing

	[Space]

	[Header("Laser")]
	[SerializeField] private bool useLaser = false;
	[SerializeField] private bool slowsEnemy = false;
	[SerializeField] private LineRenderer laserDisplay;
	[SerializeField] private int DamageOverTime = 30;

	private AudioController audioController; // References the audio script.
	
	public Node node;

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
			enemyScript = nearestEnemy.GetComponent<EnemyMovement>();
		}
        else
        {
			target = null; // Sets the nearest enemy as blank if none are within range
        }
	}
		
	void Update () 
	{
	    fireCountdown -= Time.deltaTime; 
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
            return; 
        }
        LockOn();

        if (useLaser)
        {
            FireBeam();
        }
        else
        {
            if (fireCountdown <= 0f) 
            {
                Shoot(); 
                fireCountdown = 1f / fireRate; 
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
    
    public void SetNode(Node node)
    {
    	this.node = node;
    }

    void FireBeam()
    {
        enemyScript.TakeDamage(DamageOverTime * Time.deltaTime);
        if (slowsEnemy) { enemyScript.Slow(); }

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

    void OnMouseDown()
    {
        node.SelectNode();
    }
}

