using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreScreenController : MonoBehaviour {
    public TMP_Text CompletionTimeText;
    public TMP_Text BodiesSacrificedText;
    public TMP_Text ContinueText;
    public Image FadeImage;

    private float _completionTimeInSeconds;
    private bool _canContinue;

    private void Awake()
    {
        CompletionTimeText.DOFade(0, 0);
        BodiesSacrificedText.DOFade(0, 0);
        ContinueText.DOFade(0, 0);

        _completionTimeInSeconds = Time.time - StatsManager.Instance.StartTime;
    }

    private void Start()
    {
        CompletionTimeText.text = "Completed in " + (_completionTimeInSeconds / 60f).ToString("0.00") + " minutes";
        BodiesSacrificedText.text = "Sacrificed " + StatsManager.Instance.BodiesLost  + " bodies";

        StartCoroutine(ShowStats());
    }

    private void Update()
    {
        if (Input.anyKey && _canContinue)
        {
            _canContinue = false;
            FadeImage.DOFade(1, 0.65f).OnComplete(Continue);
        }
    }

    private void Continue()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private IEnumerator ShowStats()
    {
        CompletionTimeText.DOFade(1, 0.65f);

        yield return new WaitForSeconds(0.75f);

        BodiesSacrificedText.DOFade(1, 0.65f);

        yield return new WaitForSeconds(2.85f);

        ContinueText
            .DOFade(1, 0.85f)
            .SetLoops(-1, LoopType.Yoyo);

        _canContinue = true;
    }
}
