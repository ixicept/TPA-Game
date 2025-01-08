using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public Transform targetTransform;

    public void tele()
    {
       targetTransform.position = new Vector3(489f, 44f, 250f);
    }
}
