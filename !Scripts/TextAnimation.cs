using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextAnimation : MonoBehaviour
{
    public AnimationCurve opacity;
    public AnimationCurve scale;
    public TextMeshProUGUI text;
    public Color initialColor;
    private float time = 0;

    private void Awake()
    {
        text = transform.GetComponentInChildren<TextMeshProUGUI>();
        initialColor = text.color; 
    }

    private void Update()
    {
        text.color = new Color(initialColor.r, initialColor.g, initialColor.b, opacity.Evaluate(time));
        transform.localScale = (Vector3.one * scale.Evaluate(time));
        time += Time.deltaTime;
    }
}
