using UnityEngine;
using UnityEngine.UI;

public class EnemyMovement : MonoBehaviour
{
	[SerializeField] private float speed = 5f; // Speed modifier for the enemy to move at
	[SerializeField] private float turnspeed = 3.5f; // Speed at wich to turn to the next waypoint at 
    [SerializeField] private int health = 200;
    [SerializeField] private int rewardValue = 10;
    


    private Transform target; // intended waypoint to travel to
	private int waypointIndex = 0; // value of the target to travel to

    void Start()
	{
		target = Waypoints.points[0]; // lists the first waypoint as the target
    }

    public void TakeDamage(int amount)
    {
        health -= amount;
        if (health <= 0)
        {
            EnemyDeath();
        }
    }

    void EnemyDeath()
    {
        Destroy(gameObject);
        FindObjectOfType<PlayerManager>().RewardCurrency(rewardValue);
    }

	void Update ()
	{
		Vector3 dir = target.position - transform.position;
		Quaternion lookRotation = Quaternion.LookRotation (dir);
		Vector3 rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * turnspeed).eulerAngles;
		transform.rotation = Quaternion.Euler (0f, rotation.y, 0f);
		transform.Translate (dir.normalized * speed * Time.deltaTime, Space.World);
		Debug.DrawLine (transform.position, target.position);

		if (Vector3.Distance (transform.position, target.position) <= 0.4f) 
		{
			waypointIndex++; 

			if (waypointIndex >= Waypoints.points.Length - 1f) 
			{
                ReachedEnd();
                return;
            }

			target = Waypoints.points [waypointIndex];
		}
	}

    void ReachedEnd()
    {
        Destroy(gameObject);
        PlayerManager.lives--;
    }
}

