using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private PlayerManager _playerManager;
    [SerializeField]
    private UIManager _uiManager;
    [SerializeField]
    private LevelSettings _levelSettings;

    public PlayerManager PlayerManager {
        get { return _playerManager; }
    }

    public LevelSettings LevelSettings { get; private set; }
    
    public bool GameStarted { get; private set; }

    private void Awake()
    {
        LevelSettings = Instantiate(_levelSettings);
    }

    private void Start()
    {
        if (_playerManager == null)
        {
            _playerManager = FindObjectOfType<PlayerManager>();
        }

        if (_uiManager == null)
        {
            _uiManager = FindObjectOfType<UIManager>();
        }

        _uiManager.BodyCountText.gameObject.SetActive(LevelSettings.LimitedAvailableBodies);
        _uiManager.SetBodyCountText(LevelSettings.AvailableBodies);

        _playerManager.OnPlayerSpawned.AddListener(OnPlayerSpawned);
        _playerManager.OnPlayerKilled.AddListener(OnPlayerKilled);

        _uiManager.FadeIn();
        Invoke("StartGame", 0.8f);
    }

    private void Update()
    {
        if (GameStarted && Input.GetButtonDown("Restart"))
        {
            StartCoroutine(RestartCoroutine());
        }
    }

    private void OnDestroy()
    {
        if (_playerManager != null)
        {
            _playerManager.OnPlayerSpawned.RemoveListener(OnPlayerSpawned);
            _playerManager.OnPlayerKilled.RemoveListener(OnPlayerKilled);
        }
    }

    public void Restart(bool fadeOut = true)
    {
        Action restartCallback = () => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        if (fadeOut)
        {
            _uiManager.FadeOut(restartCallback);
        }
        else
        {
            restartCallback.Invoke();
        }
    }

    public void Win()
    {
        _uiManager.FadeOut(() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1));
    }

    public void Lose()
    {
        Restart();
    }

    private void StartGame()
    {
        _playerManager.SpawnPlayer();
        GameStarted = true;
    }

    private void OnPlayerSpawned(Player player)
    {
        if (LevelSettings.LimitedAvailableBodies)
        {
            LevelSettings.AvailableBodies--;
            _uiManager.SetBodyCountText(LevelSettings.AvailableBodies);
        }

        Debug.Log("Available bodies: " + LevelSettings.AvailableBodies);
    }

    private void OnPlayerKilled(Player player)
    {
        if (LevelSettings.AvailableBodies <= 0)
        {
            NoBodiesLeft();
        }
    }

    private void NoBodiesLeft()
    {
        _uiManager.FadeOut();

        Action callback = () => Lose();
        StartCoroutine(_uiManager.ShowNoBodiesLeft(callback));
    }

    private IEnumerator RestartCoroutine()
    {
        _uiManager.ShowResetText(true);

        var neededHoldTime = Time.time + 3;
        while (Input.GetButton("Restart"))
        {
            var holdTimeLeft = neededHoldTime - Time.time;
            _uiManager.SetResetText(Mathf.RoundToInt(holdTimeLeft).ToString());

            if (holdTimeLeft <= 0)
            {
                Restart();
            }
            yield return null;
        }

        _uiManager.ShowResetText(false);
    }
}
