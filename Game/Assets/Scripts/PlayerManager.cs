using System;
using UnityEngine;
using UnityEngine.Events;

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

    [Serializable]
    public class PlayerEvent : UnityEvent<Player> { }
    public PlayerEvent OnActivePlayerChanged = new PlayerEvent();

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            SpawnPlayer();
        }
    }

    public void SpawnPlayer()
    {
        if (ActivePlayer != null)
        {
            KillPlayer();
        }

        var newPlayer = Instantiate(_playerPrefab);
        newPlayer.transform.position = _spawnPoint.position;

        ActivePlayer = newPlayer;
    }

    public void KillPlayer()
    {
        if (ActivePlayer == null)
        {
            return;
        }

        ActivePlayer.Kill();
        ActivePlayer = null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireCube(_spawnPoint.transform.position, Vector3.one);
    }
}
