using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillButton : MonoBehaviour
{
    public TextMeshProUGUI titleTxt;
    public Button sourceButton;
    public List<Image> skillContainers;
    public Image skillImage;
    public PlayerData playerData;
    public TextMeshProUGUI goldTxt;
    private int currentIndex = 0;
    void Start()
    {
        goldTxt.color = Color.yellow;
        Button button = GetComponent<Button>();
        button.onClick.AddListener(BuySkill);
    }

    void BuySkill()
    {
        string skillName = titleTxt.text;
        int gold = Convert.ToInt32(goldTxt.text);
        if (SkillManager.Instance.HasPurchasedSkill(skillName))
        {
            goldTxt.text = "already bought!";
            Debug.Log(skillName + " has already been purchased.");
            return; 
        }
        else if (playerData.gold > gold)
        {
            goldTxt.color = Color.yellow;
            Debug.Log("Bought " + skillName);
            Debug.Log("gold player now " + playerData.gold);
            SkillManager.Instance.AddLoadoutSkill(skillName);
            SkillManager.Instance.AddPurchasedSkill(skillName);
            SkillManager.Instance.AddOwnedSkill(skillName);
            playerData.gold = playerData.gold - gold;
            UpdateGUIContainers();
        }
        else
        {
            goldTxt.color = Color.red;
            goldTxt.text = "not enough gold!";
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
