using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMenuController : MonoBehaviour {
    public TMP_Text InstructionText;
    public Image FadeImage;

    private bool _isStarting;

    private void Start()
    {
        InstructionText
            .DOFade(0, 0.85f)
            .SetLoops(-1, LoopType.Yoyo);
    }

    private void Update()
    {
        if (Input.anyKeyDown && !_isStarting)
        {
            _isStarting = true;
            InstructionText.DOKill();
            FadeImage.DOFade(1, 0.65f).OnComplete(Continue);
        }
    }

    private void Continue()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
