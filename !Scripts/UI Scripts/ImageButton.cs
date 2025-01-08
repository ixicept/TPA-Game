using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.UI;

public class ImageButton : MonoBehaviour
{
    public Button sourceButton;
    public Image targetImage;
    public TextMeshProUGUI title;
    public TextMeshProUGUI description;
    public TextMeshProUGUI gold;
    public string titleTxt;
    public string descriptionTxt;
    public string goldTxt;

    private void Start()
    {
        sourceButton.onClick.AddListener(CopyImage);
        sourceButton.onClick.AddListener(ChangeText);
    }

    private void CopyImage()
    {
        Sprite buttonImage = sourceButton.image.sprite;
        targetImage.sprite = buttonImage;
    }

    private void ChangeText()
    {
        title.text = titleTxt;
        description.text = descriptionTxt;
        gold.text = goldTxt;
    }
}
