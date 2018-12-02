using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField]
    private Transform _spawnPoint;

    [SerializeField]
    private Player _playerPrefab;

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

        var newPlayer = Instantiate(_playerPrefab, _spawnPoint.transform.position, Quaternion.identity);
        newPlayer.OnKilled.AddListener(OnPlayerKilled);
        ActivePlayer = newPlayer;
    }

    public void KillPlayer()
    {
        if (ActivePlayer == null)
        {
            return;
        }

        ActivePlayer.Kill();
    }

    public void OnPlayerKilled(Player player)
    {
        player.OnKilled.RemoveListener(OnPlayerKilled);

        ActivePlayer = null;

        SpawnPlayer();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireCube(_spawnPoint.position, Vector3.one);
    }
}
