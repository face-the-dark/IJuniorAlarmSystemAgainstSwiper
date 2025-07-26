using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AlarmSystem : MonoBehaviour
{
    private const float MinAudioSourceVolume = 0.0f;
    private const float MaxAudioSourceVolume = 1.0f;
    
    [SerializeField] private float _volumeIncreaseStep = 0.1f;
    
    private AudioSource _audioSource;
    private bool _isSwiperInHouse;
    
    private Coroutine _increaseVolumeCoroutine;
    private Coroutine _decreaseVolumeCoroutine;

    public event Action SwiperIsOut;
    
    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.loop = true;
        _audioSource.volume = MinAudioSourceVolume;
        _audioSource.Play();
    }

    private void OnTriggerEnter(Collider other)
    {
        bool isItSwiper = other.TryGetComponent(out Swiper swiper);

        if (isItSwiper)
        {
            _isSwiperInHouse = true;

            if (_decreaseVolumeCoroutine != null)
            {
                StopCoroutine(_decreaseVolumeCoroutine);
                _decreaseVolumeCoroutine = null;
            }
        
            _increaseVolumeCoroutine = StartCoroutine(StartIncreaseVolume());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        bool isItSwiper = other.TryGetComponent(out Swiper swiper);

        if (isItSwiper)
        {
            _isSwiperInHouse = false;

            if (_increaseVolumeCoroutine != null)
            {
                StopCoroutine(_increaseVolumeCoroutine);
                _increaseVolumeCoroutine = null;
            }

            _decreaseVolumeCoroutine = StartCoroutine(StartDecreaseVolume());
            
            SwiperIsOut?.Invoke();
        }
    }

    private IEnumerator StartIncreaseVolume()
    {
        while (Mathf.Approximately(_audioSource.volume, MaxAudioSourceVolume) == false && _isSwiperInHouse)
        {
            _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, MaxAudioSourceVolume, Time.deltaTime * _volumeIncreaseStep);
            
            yield return null;
        }
    }

    private IEnumerator StartDecreaseVolume()
    {
        while (Mathf.Approximately(_audioSource.volume, MinAudioSourceVolume) == false && _isSwiperInHouse == false)
        {
            _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, MinAudioSourceVolume, Time.deltaTime * _volumeIncreaseStep);
            
            yield return null;
        }
    }
}
