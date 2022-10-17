using UnityEngine;

[CreateAssetMenu(fileName = "New Dialogue", menuName = "ScriptableObjects/Dialogue")]
public class DialogueScriptableObject : ScriptableObject
{
    [Header("Dialogue content")]
    [TextArea()] public string dialogue;
    public string speakerName;

    [Header("Left character")]
    public bool hasLeftCharacter;
    public bool isLeftCharacterSpeaking;
    public Sprite leftCharacterSprite;

    [Header("Right character")]
    public bool hasRightCharacter;
    public bool isRightCharacterSpeaking;
    public Sprite rightCharacter;
}
