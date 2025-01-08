using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillManager : MonoBehaviour
{
    private static SkillManager instance;
    public static SkillManager Instance { get { return instance; } }

    private List<string> purchasedSkills = new List<string>();
    private List<string> loadoutSkills = new List<string>();
    private Dictionary<string, Skill> skillDictionary = new Dictionary<string, Skill>();

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void AddPurchasedSkill(string skillName)
    {
        if (!purchasedSkills.Contains(skillName))
        {
            purchasedSkills.Add(skillName);
        }
    }
    public void AddLoadoutSkill(string skillName)
    {
        if (loadoutSkills.Count < 4 && !loadoutSkills.Contains(skillName))
        {
            loadoutSkills.Add(skillName);
        }
    }
    public string GetSkillNameFromButton(Button button)
    {
        return button.GetComponentInChildren<TextMeshProUGUI>().text;
    }

    public bool HasPurchasedSkill(string skillName)
    {
        return purchasedSkills.Contains(skillName);
    }

    public void RemoveLoadoutSkill(string skillName)
    {
        if (loadoutSkills.Contains(skillName))
        {
            loadoutSkills.Remove(skillName);
        }
    }
    public bool HasLoadoutSkill(string skillName)
    {
        return loadoutSkills.Contains(skillName);
    }
    public List<string> GetPurchasedSkills()
    {
        return purchasedSkills;
    }
    public List<string> GetLoadoutSkills()
    {
        return loadoutSkills;
    }

    public void AddSkill(string skillName, Skill skill)
    {
        if (!skillDictionary.ContainsKey(skillName))
        {
            skillDictionary.Add(skillName, skill);
        }
    }

    private List<string> ownedSkills = new List<string>();

    public void AddOwnedSkill(string skillName)
    {
        if (!ownedSkills.Contains(skillName))
        {
            ownedSkills.Add(skillName);
        }
    }

    public List<string> GetOwnedSkills()
    {
        return ownedSkills;
    }

    public List<(string, Skill)> GetSkillNames()
    {
        List<(string, Skill)> skillNames = new List<(string, Skill)>();
        foreach (var kvp in skillDictionary)
        {
            skillNames.Add((kvp.Key, kvp.Value));
        }
        return skillNames;
    }


}
