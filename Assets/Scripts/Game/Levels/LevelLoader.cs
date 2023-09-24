using Common;
using Game;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class LevelLoader : Singleton<LevelLoader>
{
    [SerializeField]
    private CanvasGroup fadeImage;

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);
        fadeImage.gameObject.SetActive(false);
    }

    public void LoadLevel(int levelIdx)
    {
        StartCoroutine(LoadLevelEx(levelIdx));
    }

    private IEnumerator LoadLevelEx(int levelIdx)
    {
        fadeImage.gameObject.SetActive(true);
        fadeImage.DOFade(1, 1f);
        yield return new WaitForSeconds(1.1f);

        var load = SceneManager.LoadSceneAsync("House_1");
        while (!load.isDone)
        {
            yield return new WaitForEndOfFrame();
        }
        yield return new WaitForEndOfFrame();
        GameManager.Instance.LoadLevelFromMainMenu(levelIdx);

        fadeImage.DOFade(0, 1f);
        yield return new WaitForSeconds(1.1f);
        fadeImage.gameObject.SetActive(false);
    }
}
