using UnityEngine;

public class Tutorial1 : MonoBehaviour
{
    public DialogTextData data;
    private void OnTriggerEnter(Collider player)
    {
        if(player.CompareTag("Player"))
        {
            DialogueSystem.Instance.StartDialogue(data);
            
            Destroy(this);
        }
    }

}
