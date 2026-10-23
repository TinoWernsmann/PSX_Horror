using System;
using System.Collections;
using UnityEngine;

public class NightIntroManager : MonoBehaviour
{
    public event Action OnIntroDone;

    [SerializeField] private NightSO[] _nightSOs;
    [SerializeField] private NightManager _nightManager;
    [SerializeField] private UIManager _uiManager;
    [SerializeField] private Player _player;

    public void ShowNight(NightSO night)
    {
        StartCoroutine(ShowNightScreen(night.nightTimer, night));
    }

    private IEnumerator ShowNightScreen(float timer, NightSO night)
    {
        StartNight(night.nightNumber);

        yield return new WaitForSecondsRealtime(timer);

        _uiManager.DisableNightUI();
        OnIntroDone?.Invoke();
    }

    private void StartNight(int nightNumber)
    {
        _uiManager.ShowNightUI(nightNumber);
    }

    public NightSO GetNight()
    {
        return _nightSOs[_nightManager.NightCount];
    }
}