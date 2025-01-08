using UnityEngine;

[CreateAssetMenu(fileName = "NPC interaction", menuName = "NPC Data", order = 1)]
public class NPCData : ScriptableObject
{
    public float lookDistanceThreshold = 10f;
    public float rotationSpeed = 5f;
    public float interactionDistance = 2f;
}
