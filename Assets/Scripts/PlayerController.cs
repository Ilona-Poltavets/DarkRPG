using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.AI;
using System.Collections;
namespace MyProject
{
    /// <summary>
    /// Main character control class
    /// </summary>
    [RequireComponent(typeof(NavMeshAgent))]
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private InputAction movement = new InputAction();
        [SerializeField] private LayerMask layerMask = new LayerMask();
        private NavMeshAgent agent = null;
        private Animator animator;
        private Camera cam = null;
        public enum ProjectMode { Project3D = 0, Project2D = 1 };
        /// <summary>
        /// Game display type
        /// </summary>
        public ProjectMode mode = ProjectMode.Project3D;
        private Texture2D cursor;
        /// <summary>
        /// Cursor sprite in normal state
        /// </summary>
        public Texture2D cursorNormal;
        /// <summary>
        /// Cursor sprite when cursor is on enemy
        /// </summary>
        public Texture2D cursorEnemy;
        /// <summary>
        /// Cursor sprite when looted
        /// </summary>
        public Texture2D cursorInfo;
        private Vector2 offset;
        /// <summary>
        /// Cursor size
        /// </summary>
        public int size = 30;

        private float timeAttack = 0f;
        private void Start()
        {
            animator = GetComponent<Animator>();
            cam = Camera.main;
            agent = GetComponent<NavMeshAgent>();
        }
        private void Update()
        {
            if (mode == ProjectMode.Project3D)
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
                if (Physics.Raycast(ray, out hit))
                {
                    MainCursor(hit.transform.tag);
                }
                else
                {
                    offset = Vector2.zero;
                    cursor = cursorNormal;
                }
            }
            animator.SetFloat("speed", agent.velocity.magnitude);
            HandleInput();
        }
        private void FixedUpdate()
        {
            if (timeAttack != 0f)
            {
                timeAttack -= 1f;
            }
        }
        void MainCursor(string tags)
        {
            if (tags == "Enemy")
            {
                offset = new Vector2(-size / 2, -size / 2);
                cursor = cursorEnemy;
            }
            else if (tags == "Info")
            {
                offset = new Vector2(-size / 2, -size / 1.2f);
                cursor = cursorInfo;
            }
            else
            {
                offset = Vector2.zero;
                cursor = cursorNormal;
            }
        }
        void OnGUI()
        {
            Vector2 mousePos = Event.current.mousePosition;
            GUI.depth = 999;
            GUI.Label(new Rect(mousePos.x + offset.x, mousePos.y + offset.y, size, size), cursor);
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
        private void PlayerMove(Vector3 location)
        {
            agent.SetDestination(location);
        }
        private void OnTriggerStay(Collider other)
        {
            if (Mouse.current.leftButton.isPressed && other.CompareTag("Enemy") && timeAttack == 0f)
            {
                animator.SetTrigger("attack");
                other.GetComponent<EnemyAI>().TakeDamage(GetComponent<Player>().GetDamage());
                timeAttack = 3f;
            }
        }
    }
}