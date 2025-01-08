using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.UI;

public class ImageBut2 : MonoBehaviour
{
    public Button sourceButton;
    public Image targetImage;
    public TextMeshProUGUI title;
    public TextMeshProUGUI gold;
    private void Start()
    {
        sourceButton.onClick.AddListener(CopyImage);
    }

    private void CopyImage()
    {
        Sprite buttonImage = sourceButton.image.sprite;
        targetImage.sprite = buttonImage;

        
        string spriteName = buttonImage.name;
        if (spriteName == "Priest13")
        {
            title.text = "Horizontal Slash";
            gold.text = "2000";
        }
        if (spriteName == "Brawler1")
        {
            title.text = "Red Energy Explosion";
            gold.text = "2000";
        }
        if (spriteName == "Geomancer2")
        {
            title.text = "Meteor Shower";
            gold.text = "2000";
        }
        if (spriteName == "Hunter10")
        {
            title.text = "Laser Rain";
            gold.text = "2000";
        }
        if (spriteName == "skill_icon_01")
        {
            title.text = "Hollow Red";
            gold.text = "2000";
        }
    }
}
