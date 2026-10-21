using System;
using UnityEngine;

public class BusDestroyer : MonoBehaviour
{
    public event Action OnBusDestroy;

    [SerializeField] private NightManager _nightManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bus"))
        {
            _nightManager.IncreaseNight();
            OnBusDestroy?.Invoke();
        }
    }
}