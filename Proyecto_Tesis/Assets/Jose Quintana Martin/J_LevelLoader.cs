using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class J_LevelLoader : MonoBehaviour
{
    public Slider progressBar;
    public TextMeshProUGUI text;

    public void LoadLevel(string sceneName)
    {
        StartCoroutine(LoadAsynchronusly(sceneName));
    }

    IEnumerator LoadAsynchronusly (string sceneName)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);

            progressBar.value = progress;
            text.SetText(progress * 100f + "%");

            yield return null;
        }
    }
}
