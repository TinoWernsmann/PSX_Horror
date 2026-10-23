using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NightManager : MonoBehaviour
{
    public event Action OnNewNight;

    [SerializeField] private NightBase[] _nights;
    [SerializeField] private UIManager _uiManager;
    [SerializeField] private GameObject _moonBlocker;
    [SerializeField] private GameObject _moon; // per night, remove
    [SerializeField] private Player _player;
    [SerializeField] private Transform _playerStartPos;
    [SerializeField] private GameObject _canTrash; // per night, remove
    [SerializeField] private NightIntroManager _nightIntroManager;
    [SerializeField] private TrashCanLogic _trashCanLogic; // per night, remove
    [SerializeField] private AudioManager _audioManager;
    [SerializeField] private MonsterSpawner _monsterSpawner; // per night, remove
    [SerializeField] private ChaseMonsterSpawner _chaseSpawner; // per night, remove
    [SerializeField] private GameObject _hangedPerson; // per night, remove
    [SerializeField] private CreatureScript _creature; // per night, remove
    [SerializeField] private BusDestroyer _busDestroyer;

    private const float TEXT_TIMER = 3f;

    private int _nightCount = 0;

    public int NightCount => _nightCount;

    private void Start()
    {
        _trashCanLogic.OnAllTrashCollected += _trashCanLogic_OnAllTrashCollected;
        NightBase.OnNightBegin += ShowBeginNightUI;
        _busDestroyer.OnBusDestroy += _busDestroyer_OnBusDestroy;
        _nightIntroManager.OnIntroDone += _nightIntroManager_OnIntroDone;

        ChooseNight();
    }

    private void _nightIntroManager_OnIntroDone()
    {
        Time.timeScale = 1f;
        ChangeWindState();
    }

    private void _busDestroyer_OnBusDestroy()
    {
        _uiManager.ShowBlackScreen();
        ChangeWindState();
        _nightCount++;

        if (CheckGameDone())
        {
            return;
        }

        _nightIntroManager.ShowNight(_nights[_nightCount].NightData);
    }

    public void ChooseNight()
    {
        ResetPlayerPosition();
        OnNewNight?.Invoke();

        StartNight();
    }

    private void StartNight()
    {
        _nightIntroManager.ShowNight(_nights[_nightCount].NightData);
        _nights[_nightCount].PlayNight();
    }

    private bool CheckGameDone()
    {
        if (_nightCount >= _nights.Length)
        {
            SceneManager.LoadScene("MainMenuScene");
            return true;
        }
        return false;
    }

    private void PlayNightTwo(NightSO night)
    {
        MoveMoonBlocker(night.moonBlockerOffset);
    }

    private void PlayNightThree(NightSO night)
    {
        MoveMoonBlocker(night.moonBlockerOffset);
        ActivateTrash();
    }

    private void PlayNightFour(NightSO night)
    {
        MoveMoonBlocker(night.moonBlockerOffset);
        Invoke("PlayScream", 10f);
        StartCoroutine(SpawnMonster(night));
    }

    private void PlayNightFive(NightSO night)
    {
        DespawnMonster();
        SpawnDeadPerson();
        MoveMoonBlocker(night.moonBlockerOffset);
    }

    private void PlayNightSix(NightSO night)
    {
        ChangeMoonToRed();
    }

    private void PlayNightSeven(NightSO night)
    {
        StartCoroutine(SpawnChasingMonster(night));
    }

    private void _trashCanLogic_OnAllTrashCollected()
    {
        //SpawnBus();
    }

    private void ShowBeginNightUI(string nightText)
    {
        StartCoroutine(ShowNightText(nightText));
    }

    private IEnumerator ShowNightText(string nightText)
    {
        _uiManager.ShowBusText(nightText);

        yield return new WaitForSeconds(TEXT_TIMER);

        _uiManager.HideBusText();
    }

    private IEnumerator SpawnMonster(NightSO night)
    {
        if (night.monsterSpawnPoint != null)
        {
            yield return new WaitForSeconds(night.monsterSpawnTime);

            _monsterSpawner.SpawnMonster(night.monsterSpawnPoint);
        }
    }

    private IEnumerator SpawnChasingMonster(NightSO night)
    {
        if (night.monsterSpawnPoint != null)
        {
            yield return new WaitForSeconds(night.monsterSpawnTime);

            _chaseSpawner.SpawnMonster(night.monsterSpawnPoint);

            _creature.StartChase();
        }
    }

    private void MoveMoonBlocker(float offset)
    {
        _moonBlocker.transform.position += new Vector3(0, offset, 0);
    }

    private void ResetPlayerPosition()
    {
        _player.gameObject.transform.position = _playerStartPos.position;
        _player.gameObject.transform.rotation = Quaternion.identity;
    }

    private void ActivateTrash()
    {
        _canTrash.SetActive(true);
        _trashCanLogic.ActivateTrashCan();
    }

    private void OnDestroy()
    {
        _trashCanLogic.OnAllTrashCollected -= _trashCanLogic_OnAllTrashCollected;
        NightBase.OnNightBegin -= ShowBeginNightUI;
        _nightIntroManager.OnIntroDone -= _nightIntroManager_OnIntroDone;
        _busDestroyer.OnBusDestroy -= _busDestroyer_OnBusDestroy;
    }

    private void SpawnDeadPerson()
    {
        _hangedPerson.SetActive(true);
    }

    private void ChangeMoonToRed()
    {
        Renderer rend = _moon.GetComponent<Renderer>();

        rend.material.EnableKeyword("_EMISSION");

        rend.material.SetColor("_EmissionColor", Color.red);
    }

    public void ChangeWindState()
    {
        _audioManager.ChangeWind();
    }

    private void DespawnMonster()
    {
        _monsterSpawner.gameObject.SetActive(false);
    }
}