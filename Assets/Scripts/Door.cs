using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Door : MonoBehaviour
{
    private static readonly int OpenKey = Animator.StringToHash("Open");
    private static readonly int CloseKey = Animator.StringToHash("Close");

    [SerializeField] private AlarmSystemTrigger _alarmTrigger;

    private Animator _animator;
    private bool _isOpen;

    private void Awake() =>
        _animator = GetComponent<Animator>();

    private void OnEnable() =>
        _alarmTrigger.SwiperIsOut += OnSwiperIsOut;

    private void OnDisable() =>
        _alarmTrigger.SwiperIsOut -= OnSwiperIsOut;

    private void OnSwiperIsOut() =>
        Close();

    public void Open()
    {
        if (_isOpen == false)
        {
            _animator.SetTrigger(OpenKey);
            _isOpen = true;
        }
    }

    private void Close()
    {
        if (_isOpen)
        {
            _animator.SetTrigger(CloseKey);
            _isOpen = false;
        }
    }
}
