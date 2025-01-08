using UnityEngine;

public class Skill : MonoBehaviour
{
    public string Name { get; private set; }
    private float cooldown;
    [HideInInspector] public float currentCooldown = 0f;
    [HideInInspector] public bool onCooldown = false;
    public Sprite icon;
    public float Cooldown
    {
        get { return cooldown; }
        set { cooldown = value; }
    }

    public Skill(string skillName, float skillCooldown)
    {
        Name = skillName;
        Cooldown = skillCooldown;
    }

    public virtual void Activate()
    {
        Debug.Log("Skill activated: " + Name);
        currentCooldown = Cooldown;
        onCooldown = true;
    }

    private void Update()
    {
        if (onCooldown)
        {
            currentCooldown -= Time.deltaTime;
            if (currentCooldown <= 0)
            {
                currentCooldown = 0;
                onCooldown = false;
            }
        }
    }
}
