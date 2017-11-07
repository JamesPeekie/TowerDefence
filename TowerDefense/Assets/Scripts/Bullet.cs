using UnityEngine;

public class Bullet : MonoBehaviour
{
	private Transform target;

	[SerializeField] private float speed = 70f; //Speed variable at which the bullet travels
    [SerializeField] private int damage = 50; 
    [SerializeField] private float explosionRadius = 0f; //Optional splash damage for special turrets
    [SerializeField] private GameObject AntBlood; //Particle effect to play on enemy hit
    
    private AudioController audioController;

    void Start()
    {
        audioController = AudioController.instance;
    }

    void Update ()
    {
        if (target == null) //Destroys bullet if there is no target
		{
			Destroy (gameObject);
			return;
		}
        
		Vector3 dir = target.position - transform.position; //Sets the flypath of the bullet towards the enemy.
		float distanceThisFrame = speed * Time.deltaTime;  //Sets the distance the bullet moves this frame to the speed modfier

		if (dir.magnitude <= distanceThisFrame) //if the bullet is close enough to the enemy, run the hit target function 
		{
			HitTarget ();
			return;
		}

		transform.Translate (dir.normalized * distanceThisFrame, Space.World); //Bullet homes in on the target's position
        transform.LookAt(target);
	}

    public void Seek(Transform target)
    {
        this.target = target;
    }

    void HitTarget() //The function that runs on hitting the enemy
	{
		GameObject effectIns = Instantiate(AntBlood, target.position, target.rotation); //Spawns particle effect on hitting the target
		Destroy(effectIns, 2f); //deletes particle effect after 2 seconds for performance
        Destroy(gameObject); //Destroys Bullet
        audioController.PlaySound("AntHit"); // Plays the Ant Hit sound.

        if (explosionRadius > 0f)
        {
            Explode();
        }
        else
        {
            Damage(target);
        }
	}

    void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach(Collider collider in colliders)
        {
            if(collider.tag == "Enemy")
            {
                Damage(collider.transform);
            }
        }
    }

    void Damage(Transform enemy)
    {
        EnemyMovement e = enemy.GetComponent<EnemyMovement>();

        if (e != null)
        {
            e.TakeDamage(damage);
        }
    }
}