using UnityEngine;

public class House : MonoBehaviour
{
    [SerializeField] private AlarmSystem _alarmSystem;
    [SerializeField] private Detector _swiperDetector;

    private void OnEnable()
    {
        _swiperDetector.Detected += OnSwiperDetected;
        _swiperDetector.Lost += OnSwiperLost;
    }

    private void OnDisable()
    {
        _swiperDetector.Detected -= OnSwiperDetected;
        _swiperDetector.Lost -= OnSwiperLost;
    }

    private void OnSwiperDetected(Collider other)
    {
        if (other.TryGetComponent(out Swiper swiper))
            _alarmSystem.Enable();
    }

    private void OnSwiperLost(Collider other)
    {
        if (other.TryGetComponent(out Swiper swiper))
            _alarmSystem.Disable();
    }
}