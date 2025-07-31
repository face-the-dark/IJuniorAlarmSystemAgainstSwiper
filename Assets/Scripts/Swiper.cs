using UnityEngine;

public class Swiper : MonoBehaviour
{
    private const KeyCode ForwardMoveKeyCode = KeyCode.W;
    private const KeyCode BackMoveKeyCode = KeyCode.S;
    private const KeyCode InteractKeyCode = KeyCode.E;

    [SerializeField] private float _moveSpeed = 5.0f;
    [SerializeField] private Detector _doorDetector;

    private Door _door;

    private void OnEnable()
    {
        _doorDetector.Detected += OnDoorDetected;
        _doorDetector.Lost += OnDoorLost;
    }

    private void OnDisable()
    {
        _doorDetector.Detected -= OnDoorDetected;
        _doorDetector.Lost -= OnDoorLost;
    }

    private void Update()
    {
        if (Input.GetKey(ForwardMoveKeyCode))
            transform.Translate(Vector3.forward * (_moveSpeed * Time.deltaTime));

        if (Input.GetKey(BackMoveKeyCode))
            transform.Translate(Vector3.back * (_moveSpeed * Time.deltaTime));

        if (Input.GetKey(InteractKeyCode))
            CrackDoor();
    }

    private void OnDoorDetected(Collider other)
    {
        if (other.TryGetComponent(out Door door))
            _door = door;
    }

    private void OnDoorLost(Collider other)
    {
        if (other.TryGetComponent(out Door door) && _door == door)
            _door = null;
    }

    private void CrackDoor()
    {
        if (_door != null)
            _door.Open();
    }
}