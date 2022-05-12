using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.AI;
using System.Collections;

namespace MyProject
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private InputAction movement = new InputAction();
        private Animator animator;
        [SerializeField] private LayerMask layerMask = new LayerMask();
        private NavMeshAgent agent = null;
        private Camera cam = null;
        private void Start()
        {
            animator = GetComponent<Animator>();
            cam = Camera.main;
            agent = GetComponent<NavMeshAgent>();
        }

        private void OnEnable()
        {
            movement.Enable();
        }

        private void OnDisable()
        {
            movement.Disable();
        }

        private void HandleInput()
        {
            if (movement.ReadValue<float>() == 1)
            {
                Ray ray = cam.ScreenPointToRay(Mouse.current.position.ReadValue());
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, 100, layerMask))
                {
                    PlayerMove(hit.point);
                }
            }
        }

        private void Update()
        {
            animator.SetFloat("speed", agent.velocity.magnitude);
            HandleInput();
        }

        private void PlayerMove(Vector3 location)
        {
            agent.SetDestination(location);
        }
        //private void OnTriggerEnter(Collider other)
        //{
        //    if (other.tag == "Enemy")
        //    {
        //        EnemyAI.hunt = true;
        //    }
        //}
        //private void OnTriggerExit(Collider other)
        //{
        //    EnemyAI.hunt = false;
        //}
    }
}