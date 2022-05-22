using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
namespace MyProject
{
	/// <summary>
	/// This class describes the behavior of the artificial intelligence of the enemy
	/// </summary>
	public class EnemyAI : MonoBehaviour
	{
		/// <summary>
		/// The tag of the target that the enemy will react to
		/// </summary>
		public string targetTag = "Player";
		/// <summary>
		/// The number of rays that an object of the class emits to search for a target
		/// </summary>
		public int rays;
		/// <summary>
		/// Ray length
		/// </summary>
		public int distance;
		/// <summary>
		/// Scatter radius of rays around a class object
		/// </summary>
		public float angle = 360;
		/// <summary>
		/// Offset beam position from the previous one
		/// </summary>
		public Vector3 offset;
		/// <summary>
		/// Target object
		/// </summary>
		private Transform target;
		/// <summary>
		/// Animator object, to activate animations
		/// </summary>
		private Animator anim;
		/// <summary>
		/// Static animator object, for activating animations from other classes
		/// </summary>
		private static Animator animStatic;
		/// <summary>
		/// NavMesh agent to implement interleaving
		/// </summary>
		private NavMeshAgent agent;
		/// <summary>
		/// The player object to use the properties and methods of this class
		/// </summary>
		[SerializeField] private Player player;
		/// <summary>
		/// Enemy Health
		/// </summary>
		public int health;
		/// <summary>
		/// The method is called when the application starts,
		/// the enemy's health is assigned, which depends on the player's level.
		/// And class objects are initialized
		/// </summary>
		public void Start()
		{
			health = player.lvl * 100;
			target = GameObject.FindGameObjectWithTag(targetTag).transform;
			anim = GetComponent<Animator>();
			agent = GetComponent<NavMeshAgent>();
			animStatic = anim;
		}
		/// <summary>
		/// A method that is executed on every FPS frame.
		/// This method conducts beams and scans the objects hit by the beams.
		/// </summary>
		public void Update()
		{
			if (Vector3.Distance(transform.position, target.position) < distance)
			{
				if (RayToScan())
				{
					agent.SetDestination(target.position);
				}
			}
		}
		/// <summary>
		/// A method that is executed every second of real time.
		/// It monitors the health of this object
		/// </summary>
		public void FixedUpdate()
		{
			if (health < 0)
				StartCoroutine(DeathCoroutine());
		}
		/// <summary>
		/// Coroutine to run functions over time.
		/// Here, the death animation is activated,
		/// the amount of experience and gold that will drop out after his death is calculated,
		/// and the object is removed from the map.
		/// </summary>
		public IEnumerator DeathCoroutine()
		{
			anim.SetTrigger("death");
			yield return new WaitForSecondsRealtime(2f);
			GameObject.FindGameObjectWithTag(targetTag).GetComponent<Player>().AddExp(Mathf.RoundToInt(Random.Range(GameObject.FindGameObjectWithTag(targetTag).GetComponent<Player>().lvl * 100, GameObject.FindGameObjectWithTag(targetTag).GetComponent<Player>().lvl * 200)));
			int count = Mathf.RoundToInt(Random.Range(GameObject.FindGameObjectWithTag(targetTag).GetComponent<Player>().lvl * 5, GameObject.FindGameObjectWithTag(targetTag).GetComponent<Player>().lvl * 10));
			ItemWorld.DropItem(transform.position, new Item { itemType = Item.ItemType.Gold, amount = count });
			Destroy(this.gameObject);
		}
		/// <summary>
		/// Reacting to the object that got into the trigger and checking it for the presence of the required tag. If the tag is defined as a player,
		/// then the enemy starts to attack the player and deal damage
		/// </summary>
		/// <param name="other">The object that hit the trigger</param>
		public void OnTriggerEnter(Collider other)
		{
			if (other.CompareTag(targetTag))
			{
				anim.SetTrigger("attack");
				player.TakeDamage(30);
			}
		}
		/// <summary>
		/// If the item is in the trigger and has a player tag,
		/// then damage is dealt to the player with an interval of 3 seconds of real time
		/// </summary>
		/// <param name="other">The object that is in the trigger</param>
		public void OnTriggerStay(Collider other)
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
		/// <summary>
		/// Method for receiving damage from the player.
		/// The amount of health from the object is taken away
		/// </summary>
		/// <param name="points"></param>
		public void TakeDamage(int points)
		{
			animStatic.SetTrigger("damage");
			health -= points;
		}
		/// <summary>
		/// Drawing rays and checking if the object with the player tag is in their range.
		/// Depending on the status of the beam, they change the color in the debug view
		/// </summary>
		/// <param name="dir">Rays position</param>
		/// <returns>Reys status. Is the player found</returns>
		public bool GetRaycast(Vector3 dir)
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
		/// <summary>
		/// Creating Rays
		/// </summary>
		/// <returns>Rays generation process status</returns>
		public bool RayToScan()
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
}