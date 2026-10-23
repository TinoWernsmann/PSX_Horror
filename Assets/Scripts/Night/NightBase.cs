using System;
using UnityEngine;

public abstract class NightBase : MonoBehaviour
{
    public static Action<string> OnNightBegin;

    [SerializeField] protected NightSO _nightData;

    public virtual void PlayNight() { }

    public NightSO NightData => _nightData;

    protected void SpawnBus(NightSO night)
    {
        BusManager.Instance.SpawnBus(night);
    }
}
