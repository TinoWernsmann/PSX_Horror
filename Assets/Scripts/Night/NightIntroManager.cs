using System.Collections;
using UnityEngine;

public class NightIntroManager : MonoBehaviour
{
    [SerializeField] private BusDestroyer _busDestroyer;
    [SerializeField] private NightSO[] _nightSOs;
    [SerializeField] private NightManager _nightManager;
    [SerializeField] private UIManager _uiManager;
    [SerializeField] private Player _player;



    private void Start()
    {
        _busDestroyer.OnBusDestroy += _busDestroyer_OnBusDestroy;
    }

    private void _busDestroyer_OnBusDestroy()
    {
        _uiManager.ShowBlackScreen();
        _nightManager.ChangeWindState();
        Invoke("ShowNight", 2f);
    }

    public void ShowNight()
    {
        if (_nightManager.NightCount >= _nightSOs.Length) return;

        StartNight(_nightSOs[_nightManager.NightCount].nightNumber);
        StartCoroutine(ShowNightScreen(_nightSOs[_nightManager.NightCount].nightTimer));
    }

    private IEnumerator ShowNightScreen(float timer)
    {
        yield return new WaitForSecondsRealtime(timer);

        DisableNightIntro();
        _player.SetAlive();
    }

    private void DisableNightIntro()
    {
        _uiManager.DisableNightUI();
        Time.timeScale = 1f;
        _nightManager.ChangeWindState();
    }

    private void StartNight(int nightNumber)
    {
        _uiManager.ShowNightUI(nightNumber);
        _nightManager.ChooseNight(_nightSOs[_nightManager.NightCount]);
        Time.timeScale = 0f;
    }

    public NightSO GetNight()
    {
        return _nightSOs[_nightManager.NightCount];
    }

    private void OnDestroy()
    {
        _busDestroyer.OnBusDestroy -= _busDestroyer_OnBusDestroy;
    }
}