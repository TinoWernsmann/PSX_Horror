using System;
using UnityEngine;

public abstract class NightBase : MonoBehaviour
{
    public static Action<string> OnNightBegin;

    [SerializeField] protected NightSO _nightData;
    [SerializeField] protected BusSpawner _busSpawner;
    [SerializeField] protected BusLogic _busLogic;
    [SerializeField] protected NightIntroManager _nightIntroManager;

    public virtual void PlayNight() { }

    protected void SpawnBus()
    {
        _busSpawner.StartNight(_nightIntroManager.GetNight().busSpawnTimer + _nightIntroManager.GetNight().nightTimer);
        _busLogic.SetDepartTimer(_nightIntroManager.GetNight().busDepartTimer);
    }
}
