using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Target & Follow")]
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset = new Vector3(0f, 12f, -12f);
    [SerializeField] private float followLerpSpeed = 10f;
    [SerializeField] private bool lookAtTarget = true;

    [Header("Zoom")] 
    [SerializeField] private float zoomSpeed = 10f; // Mouse wheel sensitivity
    [SerializeField] private float minFieldOfView = 20f; // Perspective camera
    [SerializeField] private float maxFieldOfView = 60f;
    [SerializeField] private float minOrthographicSize = 3f; // Orthographic camera
    [SerializeField] private float maxOrthographicSize = 20f;

    private Camera attachedCamera;

    private void Awake()
    {
        attachedCamera = GetComponent<Camera>();
        if (attachedCamera == null)
        {
            attachedCamera = Camera.main;
        }
    }

    private void LateUpdate()
    {
        if (target == null)
        {
            // Fallback: find an object tagged as "Player"
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                target = player.transform;
            }

            if (target == null)
            {
                return;
            }
        }

        Vector3 desiredPosition = target.position + offset;
        transform.position = Vector3.Lerp(
            transform.position,
            desiredPosition,
            1f - Mathf.Exp(-followLerpSpeed * Time.deltaTime)
        );

        if (lookAtTarget)
        {
            Quaternion desiredRotation = Quaternion.LookRotation(target.position - transform.position);
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                desiredRotation,
                1f - Mathf.Exp(-followLerpSpeed * Time.deltaTime)
            );
        }
    }

    private void Update()
    {
        if (attachedCamera == null)
        {
            return;
        }

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (Mathf.Abs(scroll) < 0.0001f)
        {
            return;
        }

        if (attachedCamera.orthographic)
        {
            float size = attachedCamera.orthographicSize - scroll * zoomSpeed;
            attachedCamera.orthographicSize = Mathf.Clamp(size, minOrthographicSize, maxOrthographicSize);
        }
        else
        {
            float fov = attachedCamera.fieldOfView - scroll * zoomSpeed;
            attachedCamera.fieldOfView = Mathf.Clamp(fov, minFieldOfView, maxFieldOfView);
        }
    }
}
