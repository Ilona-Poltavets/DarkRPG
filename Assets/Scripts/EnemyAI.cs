using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
	public string targetTag = "Player";
	public int rays = 6;
	public int distance = 5;
	public float angle = 360;
	public Vector3 offset;
	private Transform target;
    private Rigidbody rb;
    private Animator anim;
    private NavMeshAgent agent;
	[SerializeField] private Player player;

	static int health;
    void Start()
    {
		health = player.lvl * 100;
		target = GameObject.FindGameObjectWithTag(targetTag).transform;
		anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
    }
	void FixedUpdate()
	{
		if (health < 0)
			StartCoroutine(DeathCoroutine());
		if (Vector3.Distance(transform.position, target.position) < distance)
		{
			if (RayToScan())
			{
				agent.SetDestination(target.position);
			}
		}
	}
	IEnumerator DeathCoroutine()
	{
		anim.SetTrigger("death");
		Debug.Log(Time.deltaTime);
		yield return new WaitForSecondsRealtime(3f);
		Debug.Log(Time.deltaTime);
		Destroy(this.gameObject);
	}
	private void OnTriggerEnter(Collider other)
    {
		if (other.CompareTag(targetTag))
		{
			anim.SetTrigger("attack");
			player.TakeDamage(30);
		}
	}
    private void OnTriggerStay(Collider other)
    {
		float timeLeft = 3.0f;
		if (other.CompareTag(targetTag))
        {
			timeLeft -= Time.deltaTime;
			if (timeLeft < 0)
			{
				anim.SetTrigger("attack");
				player.TakeDamage(30);
			}
		}
    }
	public static void TakeDamage(int points)
    {
		health -= points;
    }
    bool GetRaycast(Vector3 dir)
	{
		bool result = false;
		RaycastHit hit = new RaycastHit();
		Vector3 pos = transform.position + offset;
		if (Physics.Raycast(pos, dir, out hit, distance))
		{
			if (hit.transform == target)
			{
				result = true;
				Debug.DrawLine(pos, hit.point, Color.green);
			}
			else
			{
				Debug.DrawLine(pos, hit.point, Color.blue);
			}
		}
		else
		{
			Debug.DrawRay(pos, dir * distance, Color.red);
		}
		return result;
	}
	bool RayToScan()
	{
		bool result = false;
		bool a = false;
		bool b = false;
		float j = 0;
		for (int i = 0; i < rays; i++)
		{
			var x = Mathf.Sin(j);
			var y = Mathf.Cos(j);

			j += angle * Mathf.Deg2Rad / rays;

			Vector3 dir = transform.TransformDirection(new Vector3(x, 0, y));
			if (GetRaycast(dir)) a = true;

			if (x != 0)
			{
				dir = transform.TransformDirection(new Vector3(-x, 0, y));
				if (GetRaycast(dir)) b = true;
			}
		}

		if (a || b) result = true;
		return result;
	}
}
