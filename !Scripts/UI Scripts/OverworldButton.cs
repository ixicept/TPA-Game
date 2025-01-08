using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OverworldButton : MonoBehaviour
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
        LoadingScript.Instance.LoadScene("OverWorld");
    }

}
