using Common;
using Game;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : Singleton<LevelLoader>
{
    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);
    }

    public void LoadLevel(int levelIdx)
    {
        StartCoroutine(LoadLevelEx(levelIdx));
    }

    private IEnumerator LoadLevelEx(int levelIdx)
    {
        // TODO: fade to black & block UI buttons from being clicked
        var load = SceneManager.LoadSceneAsync("House_1");
        while (!load.isDone)
        {
            yield return new WaitForEndOfFrame();
        }
        yield return new WaitForEndOfFrame();
        GameManager.Instance.LoadLevelFromMainMenu(levelIdx);
    }
}
