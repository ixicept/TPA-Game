using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoadoutButton : MonoBehaviour
{
    public TextMeshProUGUI titleTxt;
    public Button sourceButton;
    public List<Button> skillContainers;
    public Image skillImage; 
    private int currentIndex = 0;

    void Start()
    {
        string skillName = titleTxt.text;
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
        if (currentIndex < skillContainers.Count && skillImage != null && skillImage.sprite != null)
        {
            Image buttonImage = skillContainers[currentIndex].GetComponent<Image>();
            if (buttonImage != null)
            {
                buttonImage.sprite = skillImage.sprite;
                currentIndex += 1;
            }
        }
    }
}
