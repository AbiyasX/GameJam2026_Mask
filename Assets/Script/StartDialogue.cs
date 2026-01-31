using UnityEngine;

public class StartDialogue : MonoBehaviour
{
    public DialogTextData data;
    private DialogueSystem dialog;

    private void Start()
    {
        dialog = GetComponent<DialogueSystem>();
        dialog.StartDialogue(data);
    }
}
