using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class LoadingScript : MonoBehaviour
{
    public static LoadingScript Instance;
    public GameObject loaderCanvas;
    public Image progressBar;
    public VideoPlayer videoPlayer;
    private float target;

    void Awake()
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
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        GameObject cameraObject = GameObject.FindGameObjectWithTag("MainCamera");
        target = 0;
        progressBar.fillAmount = 0;
        var scene = SceneManager.LoadSceneAsync(sceneName);
        scene.allowSceneActivation = false;

        loaderCanvas.SetActive(true);

        if (cameraObject != null && videoPlayer != null)
        {
            Camera cameraComponent = cameraObject.GetComponent<Camera>();
            if (cameraComponent != null)
            {
               
                videoPlayer.targetCamera = cameraComponent;
            }
            else
            {
                Debug.LogError("Camera component not found on the MainCamera object.");
            }
        }
        else
        {
            Debug.LogError("Camera object or VideoPlayer component is null.");
        }


        if (player != null)
        {
            player.SetActive(false);
        }

        do
        {
            await Task.Delay(300);
            target = scene.progress;
        } while (scene.progress < 0.9f);

        await Task.Delay(300);

        scene.allowSceneActivation = true;
        loaderCanvas.SetActive(false);
        if (player != null)
        {
            player.SetActive(true);
        }
    }

    private void Update()
    {
        progressBar.fillAmount = Mathf.MoveTowards(progressBar.fillAmount, target, 5 * Time.deltaTime);
    }
}
