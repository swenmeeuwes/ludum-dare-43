using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public TMP_Text RestartText;

    private void Awake()
    {
        ShowResetText(false);
    }

    public void SetResetText(string timeLeft)
    {
        RestartText.text = "Restarting in " + timeLeft;
    }

    public void ShowResetText(bool show)
    {
        RestartText.gameObject.SetActive(show);
    }
}
