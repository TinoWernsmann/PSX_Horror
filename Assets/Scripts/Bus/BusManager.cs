using UnityEngine;

public class BusManager : MonoBehaviour
{
    public static BusManager Instance { get; private set; }

    [SerializeField] protected BusSpawner _busSpawner;
    [SerializeField] protected BusLogic _busLogic;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void SpawnBus(NightSO night)
    {
        _busSpawner.StartNight(night.busSpawnTimer + night.nightTimer);
        _busLogic.SetDepartTimer(night.busDepartTimer);
    }
}
