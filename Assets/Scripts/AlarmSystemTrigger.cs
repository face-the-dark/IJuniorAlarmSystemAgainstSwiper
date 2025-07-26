using System;
using UnityEngine;

public class AlarmSystemTrigger : MonoBehaviour
{
    public event Action SwiperIsIn;
    public event Action SwiperIsOut;
    
    private void OnTriggerEnter(Collider other)
    {
        bool isItSwiper = other.TryGetComponent(out Swiper swiper);

        if (isItSwiper) 
            SwiperIsIn?.Invoke();
    }

    private void OnTriggerExit(Collider other)
    {
        bool isItSwiper = other.TryGetComponent(out Swiper swiper);

        if (isItSwiper) 
            SwiperIsOut?.Invoke();
    }       
}
