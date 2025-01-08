using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordState : MonoBehaviour
{
    public GameObject activeSword;
    public GameObject nonActiveSword;

    public void ShowSword()
    {
        activeSword.SetActive(true);
        nonActiveSword.SetActive(false);
    }
    public void HideSword()
    {
        activeSword.SetActive(false);
        nonActiveSword.SetActive(true);
    }

}
