using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gacor : MonoBehaviour
{
    public GameObject canvasObject;
   public LevelScript levelScript;
    public void Gacorkang()
    {
        for (int i = 0; i < 10; i++)
        {
            levelScript.GainGold(10000);
            levelScript.GainXP(10000);
        }

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
