using System;
using UnityEngine;

public class Detector : MonoBehaviour
{
    public event Action<Collider> Detected;
    public event Action<Collider> Lost;

    private void OnTriggerEnter(Collider other) => 
        Detected?.Invoke(other);

    private void OnTriggerExit(Collider other) => 
        Lost?.Invoke(other);
}