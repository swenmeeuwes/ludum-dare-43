using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private PlayerManager _playerManager;
    [SerializeField]
    private UIManager _uiManager;

    public PlayerManager PlayerManager {
        get { return _playerManager; }
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

        _playerManager.SpawnPlayer();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Restart"))
        {
            StartCoroutine(RestartCoroutine());
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Win()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
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
