using UnityEngine;

public class House : MonoBehaviour
{
    [SerializeField] private AlarmSystem _alarmSystem;
    [SerializeField] private AlarmSystemTrigger _alarmTrigger;

    private void OnEnable()
    {
        _alarmTrigger.SwiperIsIn += OnSwiperIsIn;
        _alarmTrigger.SwiperIsOut += OnSwiperIsOut;
    }

    private void OnDisable()
    {
        _alarmTrigger.SwiperIsIn -= OnSwiperIsIn;
        _alarmTrigger.SwiperIsOut -= OnSwiperIsOut;
    }

    private void OnSwiperIsIn() => 
        _alarmSystem.Enable();

    private void OnSwiperIsOut() => 
        _alarmSystem.Disable();
}
