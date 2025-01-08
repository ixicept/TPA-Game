using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour
{
    public GameObject loaderCanvas;
    public GameObject player;
    public Image progressBar;

    private float target;
    public void ChangeScene(string sceneName)
    {
        LoadScene(sceneName);
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
            await Task.Delay(300);
            target = scene.progress;
        } while (scene.progress < 0.9f);

        await Task.Delay(300);

        scene.allowSceneActivation = true;
        loaderCanvas.SetActive(false);

    }
    private void Update()
    {
        progressBar.fillAmount = Mathf.MoveTowards(progressBar.fillAmount, target, 5 * Time.deltaTime);
    }
}
