using Mono.Cecil.Cil;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CheatCode : MonoBehaviour
{
    [SerializeField]
    private string currentString = "";
    [SerializeField]
    private List<CheatCodeInstance> cheatCodeList = new List<CheatCodeInstance>();
    void Update()
    {
            foreach (var c in Input.inputString)
            {
                currentString += c;
                CheckCheat(currentString);
            }

    }

    private bool CheckCheat(string input)
    {
        foreach (CheatCodeInstance c in cheatCodeList)
        {
            if (input.Contains(c.code))
            {
                c.cheatEvent?.Invoke();
                Debug.Log("Cheat code activated: " + c.code);
                currentString = "";
                return true;
            }
        }
        return false;
    }
}

[System.Serializable]
public class CheatCodeInstance
{
    public string code;
    public UnityEvent cheatEvent;
}