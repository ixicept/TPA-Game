using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Area: AreaScript
{
    public GameObject warningObject;
    public override void GiveWarning()
    {
        warningObject.SetActive(true);
        StartCoroutine(HideWarningAfterDuration());
    }

    private IEnumerator HideWarningAfterDuration()
    {
        yield return new WaitForSeconds(3f);
        warningObject.SetActive(false);
    }
}
