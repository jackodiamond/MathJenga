using UnityEngine;

public class OrbitControls : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float distance = 10.0f;
    [SerializeField] private float xSpeed = 250.0f;
    [SerializeField] private float ySpeed = 120.0f;
    [SerializeField] private float yMinLimit = -20f;
    [SerializeField] private float yMaxLimit = 80f;
    [SerializeField] private float scrollSpeed = 5.0f;

    private float x = 0.0f;
    private float y = 0.0f;

    private bool isOrbiting = false;
    private Vector3 lastMousePosition;
    Camera mainCamera;

    Vector3 position;
    Quaternion rotation;

    private void Start()
    {
        mainCamera = Camera.main;

        Vector3 angles = mainCamera.transform.eulerAngles;
        x = angles.y;
        y = angles.x;
    }

    private void LateUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isOrbiting = true;
            lastMousePosition = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isOrbiting = false;
        }

        if (isOrbiting)
        {
            x += Input.GetAxis("Mouse X") * xSpeed * distance * 0.02f;
            y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;

            y = ClampAngle(y, yMinLimit, yMaxLimit);

            rotation = Quaternion.Euler(y, x, 0);
            position = rotation * new Vector3(0.0f, 0.0f, -distance) + target.position;

            mainCamera.transform.rotation = rotation;
            mainCamera.transform.position = position;
        }

        float scrollAmount = Input.GetAxis("Mouse ScrollWheel") * scrollSpeed;
        distance -= scrollAmount;
        distance = Mathf.Clamp(distance, 1.0f, 100.0f);
    }

    private static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360f)
            angle += 360f;
        if (angle > 360f)
            angle -= 360f;
        return Mathf.Clamp(angle, min, max);
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
        mainCamera.transform.position = rotation * new Vector3(0.0f, 0.0f, -distance) + target.position;
    }
}
