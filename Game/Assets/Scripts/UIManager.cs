using DG.Tweening;
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Color _noBodiesLeftFadeColor;

    public TMP_Text RestartText;
    public TMP_Text NoBodiesLeftText;
    public TMP_Text BodyCountText;
    public Image FadeImage;

    private void Awake()
    {
        ShowResetText(false);
        FadeImage.gameObject.SetActive(true);
        NoBodiesLeftText.gameObject.SetActive(false);
        BodyCountText.gameObject.SetActive(true);
    }

    public void SetResetText(string timeLeft)
    {
        RestartText.text = "Restarting in " + timeLeft;
    }

    public void ShowResetText(bool show)
    {
        RestartText.gameObject.SetActive(show);
    }

    public void SetBodyCountText(int bodiesLeft)
    {
        BodyCountText.text = "Body count: " + bodiesLeft;
    }

    public void FadeIn()
    {
        FadeImage.DOFade(0, 0.65f);
    }

    public void FadeOut()
    {
        FadeImage.DOFade(1, 0.45f);
    }

    public void FadeOut(Action callback)
    {
        FadeImage.DOFade(1, 0.45f).OnComplete(() => callback.Invoke());
    }

    public IEnumerator ShowNoBodiesLeft(Action callback)
    {
        NoBodiesLeftText.DOFade(0, 0f);

        yield return new WaitForSeconds(0.5f);

        NoBodiesLeftText.gameObject.SetActive(true);

        NoBodiesLeftText.DOFade(1, 0.45f);
        NoBodiesLeftText.DOColor(_noBodiesLeftFadeColor, 0.65f);

        yield return new WaitForSeconds(1.8f);

        NoBodiesLeftText.DOFade(0, 0.35f);

        yield return new WaitForSeconds(0.5f);

        callback.Invoke();
    }
}
