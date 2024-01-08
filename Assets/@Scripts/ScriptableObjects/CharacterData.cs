using UnityEngine;
using UnityEngine.Windows;

[CreateAssetMenu(fileName = "CharacterData", menuName = "Scriptable Object/CharacterData", order = 0)]
public class CharacterData : ScriptableObject
{
    [SerializeField] private float moveSpeedBase = 2f;
    [SerializeField] private float moveSpeedModifierSit = 0.5f;
    [SerializeField] private float moveSpeedModifierFastRun = 2f;
    [SerializeField] private float moveSpeedModifierWalk = 0.5f;
    [SerializeField] private float sitHeight = 0.5f;
    [SerializeField] private float sitStandDuration = 0.1f;
    [SerializeField] private Vector3 defaultHeight = new Vector3(0, 1.5f, 0);

    public float MoveSpeedBase => moveSpeedBase;
    public float MoveSpeedModifierSit => moveSpeedModifierSit;
    public float MoveSpeedModifierFastRun => moveSpeedModifierFastRun;
    public float MoveSpeedModifierWalk => moveSpeedModifierWalk;
    public float SitHeight => sitHeight;
    public float SitStandDuration => sitStandDuration;
    public Vector3 DefaultHeight => defaultHeight;
}