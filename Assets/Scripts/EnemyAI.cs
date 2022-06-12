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
	private static Animator animStatic;
    private NavMeshAgent agent;
	[SerializeField] private Player player;

	[SerializeField] int health;
    void Start()
    {
		health = player.characteristics.lvl * 100;
		target = GameObject.FindGameObjectWithTag(targetTag).transform;
		anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
		animStatic = anim;
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
		yield return new WaitForSecondsRealtime(2f);
		GameObject.FindGameObjectWithTag(targetTag).GetComponent<Player>().AddExp(Mathf.RoundToInt(Random.Range(GameObject.FindGameObjectWithTag(targetTag).GetComponent<Player>().characteristics.lvl * 100, GameObject.FindGameObjectWithTag(targetTag).GetComponent<Player>().characteristics.lvl * 200)));
		int count = Mathf.RoundToInt(Random.Range(GameObject.FindGameObjectWithTag(targetTag).GetComponent<Player>().characteristics.lvl * 5, GameObject.FindGameObjectWithTag(targetTag).GetComponent<Player>().characteristics.lvl * 10));
		ItemWorld.DropItem(transform.position, new Item { itemType = Item.ItemType.Gold, amount = count });
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
				Debug.Log("Attack");
				anim.SetTrigger("attack");
				player.TakeDamage(30);
			}
		}
    }
	public void TakeDamage(int points)
    {
		animStatic.SetTrigger("damage");
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
