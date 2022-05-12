using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public GameObject player;
    private Rigidbody rb;
    private Animator anim;
    private NavMeshAgent agent;
    private float speed = 1;
    private bool hunt = false;
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (hunt)
            HuntOnPlayer();
        else
            rb.velocity = Vector3.zero;
        anim.SetBool("run", hunt);
    }
    private void HuntOnPlayer()
    {
        Vector3 vectorToPlayer = player.transform.position - transform.position;
        transform.rotation = Quaternion.FromToRotation(Vector2.right, vectorToPlayer);
        rb.velocity = transform.right * speed;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            hunt = true;
            //anim.SetBool("run", true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            hunt = false;
            //anim.SetBool("run", false);
        }
    }
}
