using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class KeybindIcon : MonoBehaviour
{
    public TextMeshProUGUI titleTxt;
    public Button sourceButton;
    public List<Image> skillContainers;
    public Image skillImage;
    private int currentIndex = 0;
    void Start()
    {
        Button button = GetComponent<Button>();
        button.onClick.AddListener(BuySkill);
    }

    void BuySkill()
    {
        string skillName = titleTxt.text;
        if (SkillManager.Instance.HasPurchasedSkill(skillName))
        {
            return;
        }
        else
        {
            UpdateGUIContainers();
        }

       
    }

    void UpdateGUIContainers()
    {
        Sprite skillSprite = skillImage.sprite;

        if (currentIndex < skillContainers.Count)
        {
            skillContainers[currentIndex].sprite = skillSprite;
            currentIndex += 1;
        }
    }
}
