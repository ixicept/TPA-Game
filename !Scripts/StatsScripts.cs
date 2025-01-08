using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsScripts : MonoBehaviour
{
    public GameObject canvasObject; 

    private void Start()
    {
        if (canvasObject != null)
        {
            canvasObject.SetActive(false); 
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            Debug.Log("C pressed");
            ToggleCanvas();
        }
    }

    private void ToggleCanvas()
    {
        if (canvasObject != null)
        {
            canvasObject.SetActive(!canvasObject.activeSelf); 
        }
    }
}
