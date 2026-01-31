using TMPro;
using UnityEngine;

public class DialogueSystem : MonoBehaviour
{
    public GameObject panel;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;

    private DialogTextData dialog;
    private int index;
    public void StartDialogue(DialogTextData data)
    {
        dialog = data;
        index = 0;

        Show();
    }
    public void Next()
    {
        index+= 1;

        Debug.Log("next");
        
        if (index >= dialog.lines.Length)
        {
            panel.SetActive(false);
            return;
        }

        Show();
    }

    void Show()
    {
        nameText.text = dialog.lines[index].characterName;
        dialogueText.text = dialog.lines[index].text;
    }
}