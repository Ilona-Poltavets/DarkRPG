using UnityEngine;
using UnityEngine.InputSystem;
namespace MyProject
{
    /// <summary>
    /// 
    /// </summary>
    public class CameraController : MonoBehaviour
    {
        private Camera cam = null;

        /// <summary>
        /// The target that the camera will track
        /// </summary>
        [Header("Target")]
        public Transform target;
        /// <summary>
        /// Distance at the start of the game
        /// </summary>
        [Header("Distances")]
        [Range(1f, 7f)] public float distance = 20f;
        /// <summary>
        /// Minimum camera distance from target
        /// </summary>
        public float minDistance = 10f;
        /// <summary>
        /// Maximum camera distance from target
        /// </summary>
        public float maxDistance = 100f;
        /// <summary>
        /// Camera offset relative to target position
        /// </summary>
        public Vector3 offset;
        /// <summary>
        /// Camera zoom speed
        /// </summary>
        [Header("Zoom speed")]
        public float smoothSpeed = 5f;
        /// <summary>
        /// Mouse scroll sensitivity
        /// </summary>
        public float scrollSensitivity = 1;

        /// <summary>
        /// Camera rotation speed
        /// </summary>
        [Header("Rotate speed")]
        public float speed = 1f;

        [SerializeField] private InputAction pressRightButton = new InputAction();

        void Start()
        {
            cam = Camera.main;
        }
        private void OnEnable()
        {
            pressRightButton.Enable();
        }

        private void OnDisable()
        {
            pressRightButton.Disable();
        }
        void LateUpdate()
        {
            if (!target)
            {
                print("NO TARGET SET FOR THE CAMERA!");
                return;
            }

            Vector2 vec = Mouse.current.scroll.ReadValue();
            float num = vec.y;
            if (num != 0)
            {
                float k = num > 0 ? -Mathf.Pow(num, 0f) : Mathf.Pow(num, 0f);
                distance += k * scrollSensitivity;
            }
            distance = Mathf.Clamp(distance, minDistance, maxDistance);

            Vector3 pos = target.position + offset;
            pos -= transform.forward * distance;

            transform.position = Vector3.Lerp(transform.position, pos, smoothSpeed * Time.deltaTime);

            if (pressRightButton.ReadValue<float>() == 1)
            {
                transform.eulerAngles += speed * new Vector3(-1 * Mouse.current.delta.y.ReadValue(), Mouse.current.delta.x.ReadValue(), 0);
            }
        }
    }
}