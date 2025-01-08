using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChangeSkill : MonoBehaviour
{
    public Button sourceButton;
    public Image topSkillImage;
    public Image bottomSkillImage;
    public TextMeshProUGUI topTxt;
    public TextMeshProUGUI botTxt;

    public List<Image> skillContainers;
    public PlayerData playerData;
    public TextMeshProUGUI goldTxt;
    private int currentIndex = 0;

    void Start()
    {
        sourceButton.onClick.AddListener(SwapSkills);
    }

    void SwapSkills()
    {
        List<string> purchasedSkills = SkillManager.Instance.GetPurchasedSkills();
        string topSkillName = topTxt.text;
        string bottomSkillName = botTxt.text;
        int topSkillIndex = purchasedSkills.FindIndex(skill => skill == topSkillName);
        int botSkillIndex = purchasedSkills.FindIndex(skill => skill == bottomSkillName);

        List<string> ownedSkills = SkillManager.Instance.GetOwnedSkills();
        int topSkillIdx = ownedSkills.FindIndex(skill => skill == topSkillName);
        int botSkillIdx = ownedSkills.FindIndex(skill => skill == bottomSkillName);

        List<string> loadout = SkillManager.Instance.GetLoadoutSkills();
        int gold = Convert.ToInt32(goldTxt.text);

        if (playerData.gold > gold)
        {
            goldTxt.color = Color.yellow;
            if (SkillManager.Instance.GetOwnedSkills().Contains(bottomSkillName) && !SkillManager.Instance.GetLoadoutSkills().Contains(bottomSkillName))
            {
                int bottomSkillIndex = ownedSkills.FindIndex(skill => skill == bottomSkillName);

                string removedSkill = loadout[topSkillIndex];

                int oldBottomSkillIndex = loadout.FindIndex(skill => skill == bottomSkillName);

                SkillManager.Instance.RemoveLoadoutSkill(topSkillName);

                SkillManager.Instance.AddLoadoutSkill(bottomSkillName);

                int newBottomSkillIndex = loadout.FindIndex(skill => skill == bottomSkillName);

                loadout[oldBottomSkillIndex] = removedSkill;

                loadout[oldBottomSkillIndex] = bottomSkillName;

                string tempSkill = purchasedSkills[topSkillIndex];
                purchasedSkills[topSkillIndex] = purchasedSkills[botSkillIndex];
                purchasedSkills[botSkillIndex] = tempSkill;

                string temp = ownedSkills[topSkillIdx];
                ownedSkills[topSkillIdx] = ownedSkills[botSkillIdx];
                ownedSkills[botSkillIdx] = tempSkill;
            }
            else
            {
                Debug.Log("skill is the same or in loadout");
            }

            playerData.gold = playerData.gold - gold;
        }
        else
        {
            goldTxt.color = Color.red;
            goldTxt.text = "not enough gold!";
        }
    }

}
