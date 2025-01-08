using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pinginlulusinisial : MonoBehaviour
{
    public GameObject canvasObject;
    public PlayerData playerData;
    public void maululus()
    {
        playerData.baseSpeed = 30f;
        canvasObject.SetActive(false);
        StartCoroutine(ShowCanvasForDuration(3f));
    }

    IEnumerator ShowCanvasForDuration(float duration)
    {
        canvasObject.SetActive(true);
        yield return new WaitForSeconds(duration);
        canvasObject.SetActive(false);
    }

}
