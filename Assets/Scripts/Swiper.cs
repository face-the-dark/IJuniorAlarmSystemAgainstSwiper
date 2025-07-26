using UnityEngine;

public class Swiper : MonoBehaviour
{
    private const KeyCode ForwardMoveKeyCode = KeyCode.W;
    private const KeyCode BackMoveKeyCode = KeyCode.S;
    private const KeyCode InteractKeyCode = KeyCode.E;

    [SerializeField] private float _moveSpeed = 5.0f;

    private Door _door;

    private void Update()
    {
        if (Input.GetKey(ForwardMoveKeyCode))
            transform.Translate(Vector3.forward * (_moveSpeed * Time.deltaTime));
        
        if (Input.GetKey(BackMoveKeyCode))
            transform.Translate(Vector3.back * (_moveSpeed * Time.deltaTime));

        if (Input.GetKey(InteractKeyCode))
            CrackDoor();
    }

    private void CrackDoor()
    {
        if (_door != null)
            _door.Open();
    }

    public void SetDoor(Door door) =>
        _door = door;

    public void RemoveDoor(Door door)
    {
        if (_door == door)
            _door = null;
    }
}
