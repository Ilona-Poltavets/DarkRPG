using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    private Camera cam = null;

    [Header("Target")]
    public Transform target;
    [Header("Distances")]
    [Range(1f,7f)] public float distance = 5f;
    public float minDistance = 1f;
    public float maxDistance = 7f;
    public Vector3 offset;
    [Header("Zoom speed")]
    public float smoothSpeed = 5f;
    public float scrollSensitivity = 1;

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
            transform.eulerAngles += speed * new Vector3(0, Mouse.current.delta.x.ReadValue(), 0);
        }
    }
}
