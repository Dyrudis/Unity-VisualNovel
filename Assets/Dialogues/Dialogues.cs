using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Dialogues : MonoBehaviour
{
    [SerializeField] private DialogueScriptableObject[] dialogues;

    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private TextMeshProUGUI speakerText;
    [SerializeField] private GameObject leftCharacter;
    [SerializeField] private GameObject rightCharacter;
    [SerializeField] private Button nextButton;

    private bool isTextAnimating = false;

    void Start()
    {
        // Start the first dialogue
        nextDialogue();
    }

    void Update()
    {
        // When the user presses the spacebar, the dialogue will change
        if (Input.GetKeyDown(KeyCode.Space) && dialogues.Length > 0)
        {
            nextDialogue();
        }
    }

    // Displays the text over time
    private IEnumerator animateText()
    {
        isTextAnimating = true;
        TextMeshProUGUI text = dialogueText;
        text.ForceMeshUpdate();
        int totalVisibleCharacters = text.textInfo.characterCount;
        int counter = 0;

        while (true)
        {
            text.maxVisibleCharacters = counter;
            if (counter >= totalVisibleCharacters)
            {
                isTextAnimating = false;
                yield break;
            }
            yield return new WaitForSeconds(.02f);
            counter++;
        }
    }

    public void nextDialogue()
    {
        // If there are no dialogues left, return
        if (dialogues.Length == 0)
        {
            return;
        }

        // If the text is animating, instantly display the full text
        if (isTextAnimating)
        {
            StopAllCoroutines();
            dialogueText.maxVisibleCharacters = dialogueText.textInfo.characterCount;
            isTextAnimating = false;
            return;
        }

        DialogueScriptableObject dialogue = dialogues[0];
        dialogues = dialogues[1..];

        // Change the text
        dialogueText.text = dialogue.dialogue;
        StartCoroutine(animateText());

        // Change the speaker text
        speakerText.text = dialogue.speakerName;

        // Change the left character
        if (dialogue.hasLeftCharacter)
        {
            leftCharacter.SetActive(true);
            leftCharacter.GetComponent<Image>().sprite = dialogue.leftCharacterSprite;
            if (dialogue.isLeftCharacterSpeaking)
            {
                leftCharacter.GetComponent<Image>().color = new Color(1, 1, 1, 1);
            }
            else
            {
                leftCharacter.GetComponent<Image>().color = new Color(1, 1, 1, 0.5f);
            }
        }
        else
        {
            leftCharacter.SetActive(false);
        }

        // Change the right character
        if (dialogue.hasRightCharacter)
        {
            rightCharacter.SetActive(true);
            rightCharacter.GetComponent<Image>().sprite = dialogue.rightCharacter;
            if (dialogue.isRightCharacterSpeaking)
            {
                rightCharacter.GetComponent<Image>().color = new Color(1, 1, 1, 1);
            }
            else
            {
                rightCharacter.GetComponent<Image>().color = new Color(1, 1, 1, 0.5f);
            }
        }
        else
        {
            rightCharacter.SetActive(false);
        }
    }

}
