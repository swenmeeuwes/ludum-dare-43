using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour {
    [SerializeField]
    private GameManager _gameManager;

    private void Start()
    {
        if (_gameManager == null)
        {
            _gameManager = FindObjectOfType<GameManager>();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            var player = collision.GetComponent<Player>();
            if (player != null && _gameManager.PlayerManager.ActivePlayer == player)
            {
                _gameManager.Win();
            }
        }
    }
}
