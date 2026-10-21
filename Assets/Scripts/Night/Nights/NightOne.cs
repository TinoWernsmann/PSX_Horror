using UnityEngine;

public class NightOne : NightBase
{
    public override void PlayNight()
    {
        SpawnBus();
        OnNightBegin?.Invoke(_nightData.nightText);
    }
}
