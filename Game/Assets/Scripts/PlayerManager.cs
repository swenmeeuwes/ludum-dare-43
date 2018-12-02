using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField]
    private Transform _spawnPoint;
    [SerializeField]
    private Player _playerPrefab;
    [SerializeField]
    private GameManager _gameManager;

    private Player _activePlayer;
    public Player ActivePlayer {
        get {
            return _activePlayer;
        }
        set {
            _activePlayer = value;
            OnActivePlayerChanged.Invoke(_activePlayer);
        }
    }

    public PlayerEvent OnActivePlayerChanged = new PlayerEvent();
    public PlayerEvent OnPlayerSpawned = new PlayerEvent();
    public PlayerEvent OnPlayerKilled = new PlayerEvent();

    private void Awake()
    {
        if (_gameManager == null)
        {
            _gameManager = FindObjectOfType<GameManager>();
        }
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            KillPlayer();
        }
    }

    public void SpawnPlayer()
    {
        if (ActivePlayer != null)
        {
            KillPlayer();
        }

        if (_gameManager.LevelSettings.AvailableBodies <= 0)
        {
            return;
        }

        var newPlayer = Instantiate(_playerPrefab, _spawnPoint.transform.position, Quaternion.identity);
        newPlayer.OnKilled.AddListener(OnPlayerKilledListener);
        ActivePlayer = newPlayer;

        OnPlayerSpawned.Invoke(newPlayer);
    }

    public void KillPlayer()
    {
        if (ActivePlayer == null)
        {
            return;
        }

        ActivePlayer.Kill();
    }

    public void OnPlayerKilledListener(Player player)
    {
        player.OnKilled.RemoveListener(OnPlayerKilledListener);
        OnPlayerKilled.Invoke(player);

        ActivePlayer = null;

        SpawnPlayer();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireCube(_spawnPoint.position, Vector3.one);
    }
}
