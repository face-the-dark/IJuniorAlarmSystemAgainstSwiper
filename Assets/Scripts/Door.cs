using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Door : MonoBehaviour
{
    [SerializeField] private Detector _swiperDetector;

    private static readonly int s_openKey = Animator.StringToHash("Open");
    private static readonly int s_closeKey = Animator.StringToHash("Close");

    private Animator _animator;
    private bool _isOpen;

    private void Awake() =>
        _animator = GetComponent<Animator>();

    private void OnEnable() =>
        _swiperDetector.Lost += OnSwiperLost;

    private void OnDisable() =>
        _swiperDetector.Lost -= OnSwiperLost;

    public void Open()
    {
        if (_isOpen == false)
        {
            _animator.SetTrigger(s_openKey);
            _isOpen = true;
        }
    }

    private void OnSwiperLost(Collider other)
    {
        if (other.TryGetComponent(out Swiper swiper))
            Close();
    }

    private void Close()
    {
        if (_isOpen)
        {
            _animator.SetTrigger(s_closeKey);
            _isOpen = false;
        }
    }
}