using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MazeButton : MonoBehaviour
{
    public GameObject canvas;
    private void Start()
    {
        Button button = GetComponent<Button>();
        button.onClick.AddListener(OnButtonClick);
    }

    private void OnButtonClick()
    {
        canvas.SetActive(false);
        LoadingScript.Instance.LoadScene("Maze");
    }

}
