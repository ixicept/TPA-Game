using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Threading.Tasks;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    public GameObject loaderCanvas;
    public Image progressBar;

    private float target;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public async void LoadScene(string sceneName)
    {
        target = 0;
        progressBar.fillAmount = 0;
        var scene = SceneManager.LoadSceneAsync(sceneName);
        scene.allowSceneActivation = false;

        loaderCanvas.SetActive(true);
        do
        {
            await Task.Delay(100);
            target = scene.progress;
            progressBar.fillAmount = scene.progress;
        } while (scene.progress < 0.9f);

        await Task.Delay(100);

        scene.allowSceneActivation = true;
        loaderCanvas.SetActive(false);
    }

    private void Update()
    {
        progressBar.fillAmount = Mathf.MoveTowards(progressBar.fillAmount, target, 2 * Time.deltaTime);
    }
}
