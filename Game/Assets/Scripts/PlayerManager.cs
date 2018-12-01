using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField]
    private Transform _spawnPoint;

    [SerializeField]
    private Player _playerPrefab;

    public Player ActivePlayer { get; set; }

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
}
