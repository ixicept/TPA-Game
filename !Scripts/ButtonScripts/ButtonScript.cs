using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript : MonoBehaviour
{
    public void ChangeScene(string sceneName)
    {
        LoadingScript.Instance.LoadScene(sceneName);
    }

}
