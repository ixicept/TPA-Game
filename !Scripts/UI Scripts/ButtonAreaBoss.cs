using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonAreaBoss : MonoBehaviour
{
    public MovementScript playerMovement;
    public GameObject freeCamGameObject;
    public GameObject canvas;

    private void Start()
    {
        Button button = GetComponent<Button>();
        button.onClick.AddListener(OnButtonClick);
    }

    private void OnButtonClick()
    {
        playerMovement.enabled = true;
        freeCamGameObject.SetActive(true);
        canvas.SetActive(false);
    }
}
