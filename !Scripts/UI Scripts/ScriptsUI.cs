using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptsUI : MonoBehaviour
{
    public GameObject canvasStats;
    public GameObject canvasKeybind;
    public MovementScript playerMovement;
    public GameObject freeCamGameObject;

    private void Start()
    {
        if (canvasStats != null)
        {
            canvasStats.SetActive(false);
        }

        if (canvasKeybind != null)
        {
            canvasKeybind.SetActive(false);
        }

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            Debug.Log("C pressed");
            ToggleCanvas();
        }

        if (Input.GetKeyDown(KeyCode.Equals))
        {
            ToggleKeybind();
        }
    }

    private void ToggleCanvas()
    {
        if (canvasStats != null)
        {
            canvasStats.SetActive(!canvasStats.activeSelf);
        }
    }

    private void ToggleKeybind()
    {
        if (canvasStats != null)
        {
            canvasKeybind.SetActive(!canvasStats.activeSelf);
            playerMovement.enabled = false;
            freeCamGameObject.SetActive(false);
        }
    }
}
