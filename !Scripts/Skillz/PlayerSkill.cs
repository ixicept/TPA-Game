using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkill : MonoBehaviour
{
    public MovementScript movementScript;
    public SkillManager skillManager;
    private List<Skill> skills = new List<Skill>();
    private Dictionary<string, Skill> skillNameToSkill = new Dictionary<string, Skill>();
    public Skill skill1;
    public Skill skill2;
    public Skill skill3;
    public Skill skill4;
    public Skill skill5;
    public KeyCode Skill1Key { get; private set; } = KeyCode.Alpha1;
    public KeyCode Skill2Key { get; private set; } = KeyCode.Alpha2;
    public KeyCode Skill3Key { get; private set; } = KeyCode.Alpha3;
    public KeyCode Skill4Key { get; private set; } = KeyCode.Alpha4;
    public KeyCode Skill5Key { get; private set; } = KeyCode.Alpha5;

    private bool isSkillActivationInProgress = false;

    private void Start()
    {
        skillManager = SkillManager.Instance;
        skillNameToSkill["Horizontal Slash"] = skill1;
        skillNameToSkill["Red Energy Explosion"] = skill2;
        skillNameToSkill["Meteor Shower"] = skill3;
        skillNameToSkill["Laser Rain"] = skill4;
        skillNameToSkill["Hollow Red"] = skill5;
    }
        
    private void Update()
    {
        if (!isSkillActivationInProgress)
        {
            CheckSkillInput();
        }
    }

    private void CheckSkillInput()
    {
        if (!movementScript.isCombat) return;
        if (Input.GetKeyDown(Skill1Key))
        {
            ActivateSkill(0);
        }
        else if (Input.GetKeyDown(Skill2Key))
        {
            ActivateSkill(1);
        }
        else if (Input.GetKeyDown(Skill3Key))
        {
            ActivateSkill(2);
        }
        else if (Input.GetKeyDown(Skill4Key))
        {
            ActivateSkill(3);
        }
        else if (Input.GetKeyDown(Skill5Key))
        {
            ActivateSkill(4);
        }
    }

    public void ActivateSkill(int index)
    {
        if (!movementScript.isCombat) return;
        List<string> ownedSkills = skillManager.GetLoadoutSkills();
        if (index < ownedSkills.Count)
        {
            string skillName = ownedSkills[index];
            if (skillNameToSkill.ContainsKey(skillName))
            {
                Skill skill = skillNameToSkill[skillName];
                if (skill != null && !skill.onCooldown)
                {
                    isSkillActivationInProgress = true;
                    ActivateSkillWithDelay(skill);
                    return;
                }
                else if (skill != null && skill.onCooldown)
                {
                    Debug.Log("Skill " + skillName + " is on cooldown.");
                }
                else
                {
                    Debug.Log("Skill " + skillName + " not found or is null.");
                }
            }
            else
            {
                Debug.Log("Skill " + skillName + " is not mapped to any skill object.");
            }
        }
        else
        {
            Debug.Log("Skill index " + index + " is not in the HUD.");
        }
    }
    public void UpdateSkillKeyCode(int skillIndex, KeyCode newKeyCode)
    {
        switch (skillIndex)
        {
            case 0:
                Skill1Key = newKeyCode;
                break;
            case 1:
                Skill2Key = newKeyCode;
                break;
            case 2:
                Skill3Key = newKeyCode;
                break;
            case 3:
                Skill4Key = newKeyCode;
                break;
            case 4:
                Skill5Key = newKeyCode;
                break;
            default:
                Debug.LogError("Invalid skill index: " + skillIndex);
                break;
        }
    }

    private void ActivateSkillWithDelay(Skill skill)
    {

        if (skill != null)
        {
            skill.Activate();
            skill.currentCooldown = skill.Cooldown;
            skill.onCooldown = true;
        }

        isSkillActivationInProgress = false;
    }
}
