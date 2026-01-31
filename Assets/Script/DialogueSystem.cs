using TMPro;
using UnityEngine;
using static UnityEditor.Rendering.MaterialUpgrader;

public class DialogueSystem : MonoBehaviour
{
    [Header("UI")]
    public GameObject dialoguePanel;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;

    private DialogTextData currentDialog;
    private int currentIndex;
    public bool isActive;

    public void StartDialogue(DialogTextData dialog)
    {
        currentDialog = dialog;
        currentIndex = 0;
        isActive = true;

        dialoguePanel.SetActive(true);
        ShowLine();
    }

    void ShowLine()
    {
        if (currentIndex >= currentDialog.lines.Length)
        {
            EndDialogue();
            return;
        }

        DialogLine line = currentDialog.lines[currentIndex];
        nameText.text = line.characterName;
        dialogueText.text = line.text;
    }

    public void NextLine()
    {  
        currentIndex++;
        ShowLine();
    }

    void EndDialogue()
    {
        isActive = false;
        dialoguePanel.SetActive(false);
        currentDialog = null;
    }

    public bool IsDialogueActive()
    {
        return isActive;
    }
}
