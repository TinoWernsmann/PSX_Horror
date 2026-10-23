using UnityEngine;

public class NightOne : NightBase
{
    public override void PlayNight()
    {
        SpawnBus(this._nightData);
        OnNightBegin?.Invoke(_nightData.nightText);
    }
}
