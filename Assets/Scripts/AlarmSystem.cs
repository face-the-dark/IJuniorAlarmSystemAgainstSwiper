using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AlarmSystem : MonoBehaviour
{
    private const float MinAudioSourceVolume = 0.0f;
    private const float MaxAudioSourceVolume = 1.0f;
    
    [SerializeField] private float _volumeChangingStep = 0.1f;
    [SerializeField] private AlarmSystemTrigger _alarmTrigger;
    
    private AudioSource _audioSource;
    private Coroutine _volumeCoroutine;
    
    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.loop = true;
        _audioSource.volume = MinAudioSourceVolume;
        _audioSource.Play();
    }

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

    private void OnSwiperIsIn()
    {
        StopVolumeCoroutine();
        
        _volumeCoroutine = StartCoroutine(StartChangingVolumeTo(MaxAudioSourceVolume));
    }

    private void OnSwiperIsOut()
    {
        StopVolumeCoroutine();

        _volumeCoroutine = StartCoroutine(StartChangingVolumeTo(MinAudioSourceVolume));
    }

    private void StopVolumeCoroutine()
    {
        if (_volumeCoroutine != null)
        {
            StopCoroutine(_volumeCoroutine);
            _volumeCoroutine = null;
        }
    }

    private IEnumerator StartChangingVolumeTo(float targetVolume)
    {
        while (Mathf.Approximately(_audioSource.volume, targetVolume) == false)
        {
            _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, targetVolume, Time.deltaTime * _volumeChangingStep);
            
            yield return null;
        }
    }
}
