using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    [SerializeField] private Door _door;
    
    private void OnTriggerEnter(Collider other)
    {
        bool isItSwiper = other.TryGetComponent(out Swiper swiper);

        if (isItSwiper)
            swiper.SetDoor(_door);
    }

    private void OnTriggerExit(Collider other)
    {
        bool isItSwiper = other.TryGetComponent(out Swiper swiper);

        if (isItSwiper)
            swiper.RemoveDoor(_door);
    }
}
