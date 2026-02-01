using UnityEngine;

public class ItemChecker : MonoBehaviour
{
    private void OnTriggerEnter(Collider player)
    {
        ItemManager.Instance.CheckCollection();
        
        if (ItemManager.Instance.isValid == true)
        {
            ItemManager.Instance.itemsCollected = 0;
            ItemManager.Instance.isValid = false;
            return;
        }
        else
        {
            Debug.Log("Game over afff");
        }
    }
}
