using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChangeKey : MonoBehaviour
{
    public TextMeshProUGUI buttonText;
    public TextMeshProUGUI guiText;
    public PlayerSkill playerSkill; 
    public int skillIndex; 
    public Button changeKeyButton; 

    private bool isWaitingForKey = false;

    private void Start()
    {
        changeKeyButton.onClick.AddListener(OnButtonClick);
    }

    private void Update()
    {
        if (isWaitingForKey)
        {
            foreach (KeyCode keyCode in System.Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKeyDown(keyCode))
                {
                    if (keyCode >= KeyCode.Alpha0 && keyCode <= KeyCode.Alpha9)
                    {
                        KeyCode convertedKeyCode = (KeyCode)((int)KeyCode.Alpha0 + (int)(keyCode - KeyCode.Alpha1));
                        playerSkill.UpdateSkillKeyCode(skillIndex, convertedKeyCode);
                        isWaitingForKey = false;
                        buttonText.text = convertedKeyCode.ToString();
                        guiText.text = convertedKeyCode.ToString();
                        return;
                    }
                    else
                    {
                        playerSkill.UpdateSkillKeyCode(skillIndex, keyCode);
                        isWaitingForKey = false;
                        buttonText.text = keyCode.ToString();
                        guiText.text = keyCode.ToString();
                        return;
                    }
                }
            }

        }
    }

    public void OnButtonClick()
    {
        buttonText.text = "Waiting...";
        isWaitingForKey = true;
    }
}
